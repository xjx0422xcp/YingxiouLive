; (function ($) {
    $.fn.ajaxSubmit = function (options) {
        // fast fail if nothing selected (http://dev.jquery.com/ticket/2752)
        if (!this.length) {
            log('ajaxSubmit: skipping submit process - no element selected');
            return this;
        }

        if (typeof options == 'function')
            options = { success: options };

        var url = $.trim(this.attr('action'));
        if (url) {
            // clean url (don't include hash vaue)
            url = (url.match(/^([^#]+)/) || [])[1];
        }
        url = url || window.location.href || ''

        options = $.extend({
            url: url,
            type: this.attr('method') || 'GET'
        }, options || {});

        // hook for manipulating the form data before it is extracted;
        // convenient for use with rich editors like tinyMCE or FCKEditor
        var veto = {};
        this.trigger('form-pre-serialize', [this, options, veto]);
        if (veto.veto) {
            log('ajaxSubmit: submit vetoed via form-pre-serialize trigger');
            return this;
        }

        // provide opportunity to alter form data before it is serialized
        if (options.beforeSerialize && options.beforeSerialize(this, options) === false) {
            log('ajaxSubmit: submit aborted via beforeSerialize callback');
            return this;
        }

        var a = this.formToArray(options.semantic);
        if (options.data) {
            options.extraData = options.data;
            for (var n in options.data) {
                if (options.data[n] instanceof Array) {
                    for (var k in options.data[n])
                        a.push({ name: n, value: options.data[n][k] });
                }
                else
                    a.push({ name: n, value: options.data[n] });
            }
        }

        // give pre-submit callback an opportunity to abort the submit
        if (options.beforeSubmit && options.beforeSubmit(a, this, options) === false) {
            log('ajaxSubmit: submit aborted via beforeSubmit callback');
            return this;
        }

        // fire vetoable 'validate' event
        this.trigger('form-submit-validate', [a, this, options, veto]);
        if (veto.veto) {
            log('ajaxSubmit: submit vetoed via form-submit-validate trigger');
            return this;
        }

        var q = $.param(a);

        if (options.type.toUpperCase() == 'GET') {
            options.url += (options.url.indexOf('?') >= 0 ? '&' : '?') + q;
            options.data = null;  // data is null for 'get'
        }
        else
            options.data = q; // data is the query string for 'post'

        var $form = this, callbacks = [];
        if (options.resetForm) callbacks.push(function () { $form.resetForm(); });
        if (options.clearForm) callbacks.push(function () { $form.clearForm(); });

        // perform a load on the target only if dataType is not provided
        if (!options.dataType && options.target) {
            var oldSuccess = options.success || function () { };
            callbacks.push(function (data) {
                $(options.target).html(data).each(oldSuccess, arguments);
            });
        }
        else if (options.success)
            callbacks.push(options.success);

        options.success = function (data, status) {
            for (var i = 0, max = callbacks.length; i < max; i++)
                callbacks[i].apply(options, [data, status, $form]);
        };

        // are there files to upload?
        var files = $('input:file', this).fieldValue();
        var found = false;
        for (var j = 0; j < files.length; j++)
            if (files[j])
                found = true;

        var multipart = false;
        //	var mp = 'multipart/form-data';
        //	multipart = ($form.attr('enctype') == mp || $form.attr('encoding') == mp);

        // options.iframe allows user to force iframe mode
        if (options.iframe || found || multipart) {
            // hack to fix Safari hang (thanks to Tim Molendijk for this)
            // see:  http://groups.google.com/group/jquery-dev/browse_thread/thread/36395b7ab510dd5d
            if (options.closeKeepAlive)
                $.get(options.closeKeepAlive, fileUpload);
            else
                fileUpload();
        }
        else {

            $.ajax(options);

        }

        // fire 'notify' event
        this.trigger('form-submit-notify', [this, options]);
        return this;


        // private function for handling file uploads (hat tip to YAHOO!)
        function fileUpload() {
            var form = $form[0];

            if ($(':input[name=submit]', form).length) {
                alert('Error: Form elements must not be named "submit".');
                return;
            }

            var opts = $.extend({}, $.ajaxSettings, options);
            var s = $.extend(true, {}, $.extend(true, {}, $.ajaxSettings), opts);

            var id = 'jqFormIO' + (new Date().getTime());
            var $io = $('<iframe id="' + id + '" name="' + id + '" src="about:blank" />');
            var io = $io[0];

            $io.css({ position: 'absolute', top: '-1000px', left: '-1000px' });

            var xhr = { // mock object
                aborted: 0,
                responseText: null,
                responseXML: null,
                status: 0,
                statusText: 'n/a',
                getAllResponseHeaders: function () { },
                getResponseHeader: function () { },
                setRequestHeader: function () { },
                abort: function () {
                    this.aborted = 1;
                    $io.attr('src', 'about:blank'); // abort op in progress
                }
            };

            var g = opts.global;
            // trigger ajax global events so that activity/block indicators work like normal
            if (g && !$.active++) $.event.trigger("ajaxStart");
            if (g) $.event.trigger("ajaxSend", [xhr, opts]);

            if (s.beforeSend && s.beforeSend(xhr, s) === false) {
                s.global && $.active--;
                return;
            }
            if (xhr.aborted)
                return;

            var cbInvoked = 0;
            var timedOut = 0;

            // add submitting element to data if we know it
            var sub = form.clk;
            if (sub) {
                var n = sub.name;
                if (n && !sub.disabled) {
                    options.extraData = options.extraData || {};
                    options.extraData[n] = sub.value;
                    if (sub.type == "image") {
                        options.extraData[name + '.x'] = form.clk_x;
                        options.extraData[name + '.y'] = form.clk_y;
                    }
                }
            }

            // take a breath so that pending repaints get some cpu time before the upload starts
            setTimeout(function () {
                // make sure form attrs are set
                var t = $form.attr('target'), a = $form.attr('action');

                // update form attrs in IE friendly way
                form.setAttribute('target', id);
                if (form.getAttribute('method') != 'POST')
                    form.setAttribute('method', 'POST');
                if (form.getAttribute('action') != opts.url)
                    form.setAttribute('action', opts.url);

                // ie borks in some cases when setting encoding
                if (!options.skipEncodingOverride) {
                    $form.attr({
                        encoding: 'multipart/form-data',
                        enctype: 'multipart/form-data'
                    });
                }

                // support timout
                if (opts.timeout)
                    setTimeout(function () { timedOut = true; cb(); }, opts.timeout);

                // add "extra" data to form if provided in options
                var extraInputs = [];
                try {
                    if (options.extraData)
                        for (var n in options.extraData)
                            extraInputs.push(
                                $('<input type="hidden" name="' + n + '" value="' + options.extraData[n] + '" />')
                                    .appendTo(form)[0]);

                    // add iframe to doc and submit the form
                    $io.appendTo('body');
                    io.attachEvent ? io.attachEvent('onload', cb) : io.addEventListener('load', cb, false);
                    form.submit();
                }
                finally {
                    // reset attrs and remove "extra" input elements
                    form.setAttribute('action', a);
                    t ? form.setAttribute('target', t) : $form.removeAttr('target');
                    $(extraInputs).remove();
                }
            }, 10);

            var nullCheckFlag = 0;

            function cb() {
                if (cbInvoked++) return;

                io.detachEvent ? io.detachEvent('onload', cb) : io.removeEventListener('load', cb, false);

                var ok = true;
                try {
                    if (timedOut) throw 'timeout';
                    // extract the server response from the iframe
                    var data, doc;

                    doc = io.contentWindow ? io.contentWindow.document : io.contentDocument ? io.contentDocument : io.document;

                    if ((doc.body == null || doc.body.innerHTML == '') && !nullCheckFlag) {
                        // in some browsers (cough, Opera 9.2.x) the iframe DOM is not always traversable when
                        // the onload callback fires, so we give them a 2nd chance
                        nullCheckFlag = 1;
                        cbInvoked--;
                        setTimeout(cb, 100);
                        return;
                    }

                    xhr.responseText = doc.body ? doc.body.innerHTML : null;
                    xhr.responseXML = doc.XMLDocument ? doc.XMLDocument : doc;
                    xhr.getResponseHeader = function (header) {
                        var headers = { 'content-type': opts.dataType };
                        return headers[header];
                    };

                    if (opts.dataType == 'json' || opts.dataType == 'script') {
                        var ta = doc.getElementsByTagName('textarea')[0];
                        xhr.responseText = ta ? ta.value : xhr.responseText;
                    }
                    else if (opts.dataType == 'xml' && !xhr.responseXML && xhr.responseText != null) {
                        xhr.responseXML = toXml(xhr.responseText);
                    }
                    data = $.httpData(xhr, opts.dataType);
                }
                catch (e) {
                    ok = false;
                    $.handleError(opts, xhr, 'error', e);
                }

                // ordering of these callbacks/triggers is odd, but that's how $.ajax does it
                if (ok) {
                    opts.success(data, 'success');
                    if (g) $.event.trigger("ajaxSuccess", [xhr, opts]);
                }
                if (g) $.event.trigger("ajaxComplete", [xhr, opts]);
                if (g && ! --$.active) $.event.trigger("ajaxStop");
                if (opts.complete) opts.complete(xhr, ok ? 'success' : 'error');

                // clean up
                setTimeout(function () {
                    $io.remove();
                    xhr.responseXML = null;
                }, 100);
            };

            function toXml(s, doc) {
                if (window.ActiveXObject) {
                    doc = new ActiveXObject('Microsoft.XMLDOM');
                    doc.async = 'false';
                    doc.loadXML(s);
                }
                else
                    doc = (new DOMParser()).parseFromString(s, 'text/xml');
                return (doc && doc.documentElement && doc.documentElement.tagName != 'parsererror') ? doc : null;
            };
        };

    };

    /**
    * ajaxForm() provides a mechanism for fully automating form submission.
    *
    * The advantages of using this method instead of ajaxSubmit() are:
    *
    * 1: This method will include coordinates for <input type="image" /> elements (if the element
    *    is used to submit the form).
    * 2. This method will include the submit element's name/value data (for the element that was
    *    used to submit the form).
    * 3. This method binds the submit() method to the form for you.
    *
    * The options argument for ajaxForm works exactly as it does for ajaxSubmit.  ajaxForm merely
    * passes the options argument along after properly binding events for submit elements and
    * the form itself.
    */
    $.fn.ajaxForm = function (options) {

        return this.ajaxFormUnbind().bind('submit.form-plugin', function () {
            $(this).ajaxSubmit(options);
            return false;
        }).each(function () {
            // store options in hash
            $(":submit,input:image", this).bind('click.form-plugin', function (e) {
                var form = this.form;
                form.clk = this;
                if (this.type == 'image') {
                    if (e.offsetX != undefined) {
                        form.clk_x = e.offsetX;
                        form.clk_y = e.offsetY;
                    } else if (typeof $.fn.offset == 'function') { // try to use dimensions plugin
                        var offset = $(this).offset();
                        form.clk_x = e.pageX - offset.left;
                        form.clk_y = e.pageY - offset.top;
                    } else {
                        form.clk_x = e.pageX - this.offsetLeft;
                        form.clk_y = e.pageY - this.offsetTop;
                    }
                }
                // clear form vars
                setTimeout(function () { form.clk = form.clk_x = form.clk_y = null; }, 10);
            });
        });
    };

    // ajaxFormUnbind unbinds the event handlers that were bound by ajaxForm
    $.fn.ajaxFormUnbind = function () {
        this.unbind('submit.form-plugin');
        return this.each(function () {
            $(":submit,input:image", this).unbind('click.form-plugin');
        });

    };

    /**
    * formToArray() gathers form element data into an array of objects that can
    * be passed to any of the following ajax functions: $.get, $.post, or load.
    * Each object in the array has both a 'name' and 'value' property.  An example of
    * an array for a simple login form might be:
    *
    * [ { name: 'username', value: 'jresig' }, { name: 'password', value: 'secret' } ]
    *
    * It is this array that is passed to pre-submit callback functions provided to the
    * ajaxSubmit() and ajaxForm() methods.
    */
    $.fn.formToArray = function (semantic) {
        var a = [];
        if (this.length == 0) return a;

        var form = this[0];
        var els = semantic ? form.getElementsByTagName('*') : form.elements;
        if (!els) return a;
        for (var i = 0, max = els.length; i < max; i++) {
            var el = els[i];
            var n = el.name;
            if (!n) continue;

            if (semantic && form.clk && el.type == "image") {
                // handle image inputs on the fly when semantic == true
                if (!el.disabled && form.clk == el) {
                    a.push({ name: n, value: $(el).val() });
                    a.push({ name: n + '.x', value: form.clk_x }, { name: n + '.y', value: form.clk_y });
                }
                continue;
            }

            var v = $.fieldValue(el, true);
            if (v && v.constructor == Array) {
                for (var j = 0, jmax = v.length; j < jmax; j++)
                    a.push({ name: n, value: v[j] });
            }
            else if (v !== null && typeof v != 'undefined')
                a.push({ name: n, value: v });
        }

        if (!semantic && form.clk) {
            // input type=='image' are not found in elements array! handle it here
            var $input = $(form.clk), input = $input[0], n = input.name;
            if (n && !input.disabled && input.type == 'image') {
                a.push({ name: n, value: $input.val() });
                a.push({ name: n + '.x', value: form.clk_x }, { name: n + '.y', value: form.clk_y });
            }
        }
        return a;
    };

    /**
    * Serializes form data into a 'submittable' string. This method will return a string
    * in the format: name1=value1&amp;name2=value2
    */
    $.fn.formSerialize = function (semantic) {
        //hand off to jQuery.param for proper encoding
        return $.param(this.formToArray(semantic));
    };

    /**
    * Serializes all field elements in the jQuery object into a query string.
    * This method will return a string in the format: name1=value1&amp;name2=value2
    */
    $.fn.fieldSerialize = function (successful) {
        var a = [];
        this.each(function () {
            var n = this.name;
            if (!n) return;
            var v = $.fieldValue(this, successful);
            if (v && v.constructor == Array) {
                for (var i = 0, max = v.length; i < max; i++)
                    a.push({ name: n, value: v[i] });
            }
            else if (v !== null && typeof v != 'undefined')
                a.push({ name: this.name, value: v });
        });
        //hand off to jQuery.param for proper encoding
        return $.param(a);
    };

    /**
    * Returns the value(s) of the element in the matched set.  For example, consider the following form:
    *
    *  <form><fieldset>
    *      <input name="A" type="text" />
    *      <input name="A" type="text" />
    *      <input name="B" type="checkbox" value="B1" />
    *      <input name="B" type="checkbox" value="B2"/>
    *      <input name="C" type="radio" value="C1" />
    *      <input name="C" type="radio" value="C2" />
    *  </fieldset></form>
    *
    *  var v = $(':text').fieldValue();
    *  // if no values are entered into the text inputs
    *  v == ['','']
    *  // if values entered into the text inputs are 'foo' and 'bar'
    *  v == ['foo','bar']
    *
    *  var v = $(':checkbox').fieldValue();
    *  // if neither checkbox is checked
    *  v === undefined
    *  // if both checkboxes are checked
    *  v == ['B1', 'B2']
    *
    *  var v = $(':radio').fieldValue();
    *  // if neither radio is checked
    *  v === undefined
    *  // if first radio is checked
    *  v == ['C1']
    *
    * The successful argument controls whether or not the field element must be 'successful'
    * (per http://www.w3.org/TR/html4/interact/forms.html#successful-controls).
    * The default value of the successful argument is true.  If this value is false the value(s)
    * for each element is returned.
    *
    * Note: This method *always* returns an array.  If no valid value can be determined the
    *       array will be empty, otherwise it will contain one or more values.
    */
    $.fn.fieldValue = function (successful) {
        for (var val = [], i = 0, max = this.length; i < max; i++) {
            var el = this[i];
            var v = $.fieldValue(el, successful);
            if (v === null || typeof v == 'undefined' || (v.constructor == Array && !v.length))
                continue;
            v.constructor == Array ? $.merge(val, v) : val.push(v);
        }
        return val;
    };

    /**
    * Returns the value of the field element.
    */
    $.fieldValue = function (el, successful) {
        var n = el.name, t = el.type, tag = el.tagName.toLowerCase();
        if (typeof successful == 'undefined') successful = true;

        if (successful && (!n || el.disabled || t == 'reset' || t == 'button' ||
        (t == 'checkbox' || t == 'radio') && !el.checked ||
        (t == 'submit' || t == 'image') && el.form && el.form.clk != el ||
        tag == 'select' && el.selectedIndex == -1))
            return null;

        if (tag == 'select') {
            var index = el.selectedIndex;
            if (index < 0) return null;
            var a = [], ops = el.options;
            var one = (t == 'select-one');
            var max = (one ? index + 1 : ops.length);
            for (var i = (one ? index : 0) ; i < max; i++) {
                var op = ops[i];
                if (op.selected) {
                    var v = op.value;
                    if (!v) // extra pain for IE...
                        v = (op.attributes && op.attributes['value'] && !(op.attributes['value'].specified)) ? op.text : op.value;
                    if (one) return v;
                    a.push(v);
                }
            }
            return a;
        }
        return el.value;
    };

    /**
    * Clears the form data.  Takes the following actions on the form's input fields:
    *  - input text fields will have their 'value' property set to the empty string
    *  - select elements will have their 'selectedIndex' property set to -1
    *  - checkbox and radio inputs will have their 'checked' property set to false
    *  - inputs of type submit, button, reset, and hidden will *not* be effected
    *  - button elements will *not* be effected
    */
    $.fn.clearForm = function () {
        return this.each(function () {
            $('input,select,textarea', this).clearFields();
        });
    };

    /**
    * Clears the selected form elements.
    */
    $.fn.clearFields = $.fn.clearInputs = function () {
        return this.each(function () {
            var t = this.type, tag = this.tagName.toLowerCase();
            if (t == 'text' || t == 'password' || tag == 'textarea')
                this.value = '';
            else if (t == 'checkbox' || t == 'radio')
                this.checked = false;
            else if (tag == 'select')
                this.selectedIndex = -1;
        });
    };

    /**
    * Resets the form data.  Causes all form elements to be reset to their original value.
    */
    $.fn.resetForm = function () {
        return this.each(function () {
            // guard against an input with the name of 'reset'
            // note that IE reports the reset function as an 'object'
            if (typeof this.reset == 'function' || (typeof this.reset == 'object' && !this.reset.nodeType))
                this.reset();
        });
    };

    /**
    * Enables or disables any matching elements.
    */
    $.fn.enable = function (b) {
        if (b == undefined) b = true;
        return this.each(function () {
            this.disabled = !b;
        });
    };

    /**
    * Checks/unchecks any matching checkboxes or radio buttons and
    * selects/deselects and matching option elements.
    */
    $.fn.selected = function (select) {
        if (select == undefined) select = true;
        return this.each(function () {
            var t = this.type;
            if (t == 'checkbox' || t == 'radio')
                this.checked = select;
            else if (this.tagName.toLowerCase() == 'option') {
                var $sel = $(this).parent('select');
                if (select && $sel[0] && $sel[0].type == 'select-one') {
                    // deselect all other options
                    $sel.find('option').selected(false);
                }
                this.selected = select;
            }
        });
    };

    // helper fn for console logging
    // set $.fn.ajaxSubmit.debug to true to enable debug logging
    function log() {
        if ($.fn.ajaxSubmit.debug && window.console && window.console.log)
            window.console.log('[jquery.form] ' + Array.prototype.join.call(arguments, ''));
    };

    //注册form的ajaxForm 此方法需要调用jquery.ajaxwindow.js的方法
    $.fn.submitForm = function (args) {
        var url, id, callback, before, callerror, beforeSerialize, dataType, checkInput;
        id = this.attr("id");
        checkInput = true;
        if (typeof (args) == "string") {
            url = args;
            before = undefined;
            callback = undefined;
            callerror = undefined;
            beforeSerialize = undefined;
            dataType = undefined;
        }
        else {
            args = args || new Object();
            url = args.url || this.attr("action");
            dataType = args.dataType || "json";
            if (typeof (args) == "function") {
                callback = args;
            }
            else {
                before = args.before;
                callback = args.callback;
                callerror = args.callerror;
                beforeSerialize = args.beforeSerialize;
                checkInput = args.checkInput == undefined ? checkInput : args.checkInput;
            }
        }


        //输入验证
        if (checkInput)
            this.inputValidate();
        //form没有url 则是伪提交
        if (url == undefined || url == "") {
            $("#" + id).submit(function () {
                if ($("#" + id).submitValidate() == false)
                    return false;
                //验证成功就执行callback
                callback();
            });
        }
        else {

            this.ajaxForm({
                url: url,
                beforeSerialize: beforeSerialize,
                beforeSubmit: function (a, f, o) {
                    //提交验证
                    var _this = this;
                    $("#SubmitMask").appendTo(document.body);
                    $("#SubmitInfo").appendTo(document.body).ajaxStart(function () {
                        $(":submit,input:image", _this).attr("disabled", "disabled");
                        $(this).show().css({
                            "left": (document.body.clientWidth - $(this).width()) / 2,
                            "top": (document.documentElement.clientHeight - $(this).height()) / 2 + document.documentElement.scrollTop
                        });
                        $("#SubmitMask").show().css({
                            "opacity": .3,
                            "height": Math.max(document.documentElement.offsetHeight, document.body.scrollHeight)
                        })
                    });

                    $("#SubmitInfo").ajaxStop(function () {
                        $(this).fadeOut(300);
                        $("#SubmitMask").fadeOut(300);
                        $(this).unbind();
                        $(":submit,input:image", _this).removeAttr("disabled");
                    });
                    if (checkInput) {
                        if ($("#" + id).submitValidate() == false)
                            return false;
                    }
                    if (before != undefined && before() == false) {
                        return false;
                    }

                    o.dataType = dataType;
                },

                success: function (data) {

                    if (data == "" || data == null) {
                        return false;
                    }

                    var msg = new ajaxMsg(data);

                    //confirm
                    if (msg.result == -1) {
                        if (callback != undefined) {
                            callerror(data);
                        }
                        else {
                            if (confirm(msg.content)) {
                                url = url || this.redirect;
                                if (url.indexOf("?") == -1)
                                    url += "?";
                                //$("#" + id).attr("action", url);
                                $("#" + id).submitForm({ url: url + "&skip=1", callback: callback }).submit();
                            }
                            else
                                return false;
                        }


                    }
                    else
                        callback(data);

                    return false;
                }
            });
        }
        return this;
    }
    //输入验证
    $.fn.inputValidate = function () {

        $("input,select,textarea", this).each(function () {
            var isnull = $(this).attr("isnull");
            var regexValue = $(this).attr("regex");
            var defautValue = $(this).attr("dvalue");

            //①非空注册焦点事件
            if (isnull == "0") {
                $(this).blur(function () {
                    if (this.value == "" || this.value == defautValue)
                        $(this).addClass("focus");
                    else
                        $(this).removeClass("focus");
                });
            }

            //②正则注册onchange事件
            if (regexValue != undefined) {
                var thisValue = this.value;
                //检查类型绑定不同事件
                if ($(this).attr("type") == "text") {
                    $(this).bind("keyup", function (e) {
                        switch (e.keyCode) {
                            case 8:
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                                return;
                        }
                        if ($(this).val() == "")
                            return;
                        var re = new RegExp(regexValue, "ig");
                        var newValue = this.value;
                        if (!re.test(newValue)) {
                            $(this).val(thisValue);
                        }
                        else {
                            thisValue = newValue;
                            $(this).val(newValue);
                        }
                    });
                }
            }

            function checkRegex(value, re) {

            }
            //③最小长度

            //④其他

        });
    }

    //提交验证
    $.fn.submitValidate = function () {
        var result = true;
        var innerResult = true;
        var wholeResult = true;
        $("input:visible,select:visible,textarea:visible", this).each(function () {
            innerResult = true;
            var thisValue = "";
            if ($(this).attr("type") == "radio" || $(this).attr("type") == "checkbox") {
                thisValue = $("input[name='" + this.name + "']:checked").val();
            }
            else
                thisValue = $(this).val();
            //判断是否违法


            if ($(this).attr("isnull") == "0") {//① 是否必填 且不能为空或缺省值
                innerResult = (thisValue != "" && thisValue != $(this).attr("dvalue"));
                if (!innerResult)
                    wholeResult = false;
            }

            if ($(this).attr("type") == "file") {
                $tmpAttr = $(this).attr("filerequired");
                if (typeof ($tmpAttr) != "undefined") {
                    if ($(this).attr("filerequired") == "1" || $(this).attr("filerequired") == "filerequired") {
                        innerResult = thisValue != "";
                    }
                }
            }

            if (typeof ($(this).attr("maxnumber")) != "undefined") {

                if ($(this).val() == "")
                    innerResult = false;
                else {
                    if (parseInt($(this).val()) > parseInt($(this).attr("maxnumber"))) {
                        innerResult = false;
                    }
                }

            }

            if (typeof ($(this).attr("minnumber")) != "undefined") {
                if ($(this).val() == "")
                    innerResult = false;
                else {
                    if (parseInt($(this).val()) < parseInt($(this).attr("minnumber"))) {
                        innerResult = false;
                    }
                }
            }


            //把else删了，如果2个属性都设置了值，就判断错误了 update
            if (thisValue != "") {//② 是否符合格式 属性为 regex 正则
                var reValue = $(this).attr("regex");
                if (reValue != undefined) {
                    re = new RegExp(reValue, "ig");
                    innerResult = re.test(thisValue);
                }
            }

            //        //③ 是否符合最大长度
            //        var maxLength = $(this).attr("maxLen");
            //        if (maxLength != undefined && maxLength != "-1") {
            //            if (thisValue.length > parseInt(maxLength))
            //                result = false;
            //        }
            //        //④ 是否符合最小长度

            //返回false
            if (result && !innerResult) {
                result = innerResult;
            }
            if (innerResult == false) {

                $(this).addClass("focus");

            }

        });
        if (!wholeResult)
            alert("有必填项未输入！");
        var alterOne = 0;
        $("input[type='hidden']", this).each(function () {
            if ($(this).attr("dyfilerequired") == "1") {
                var itemid = $(this).attr("dyrechid");
                if ($(".dycheck_" + itemid) == null || $(".dycheck_" + itemid).length == 0) {
                    if (alterOne == 0) {
                        alert("请选择上传文件。");
                        alterOne = 1;
                    }
                    result = false;
                }
            }
        });
        if (!result) {
            if ($("#msgbox_error")) {
                $("#msgbox_error").show();
                location.hash = "error_a";
            }

        }

        return result;
    }

    //绑定checkbox
    $.fn.bindData = function (args) {

        if ($(this).attr("type") == "radio" || $(this).attr("type") == "checkbox") {
            var argType = typeof (args);

            if (argType == "string") {
                if (args == "true" || args == "True")
                    this.checked = true;
                else
                    this.checked = false;
            }
            else if (argType == "boolean") {
                if (args)
                    this.checked = true;
                else
                    this.checked = false;

            }
            else if (argType == "number") {
                if (args == 1)
                    this.checked = true;
                else
                    this.checked = false;
            }
        }
        else if ($(this).attr("type") == "select-one") {
            var isSelected = false;
            this.children().each(function () {
                if (this.value == args) {
                    isSelected = true;
                    this.selected = true;
                }
            });
            if (!isSelected)
                this[0].selectedIndex = 0;
        }
    },
     jQuery.extend({
         handleError: function (s, xhr, status, e) {
             if (s.error) {
                 s.error.call(s.context || s, xhr, status, e);
             }
             if (s.global) {
                 (s.context ? jQuery(s.context) : jQuery.event).trigger("ajaxError", [xhr, s, e]);
             }
         },
         httpData: function (xhr, type, s) {
             var ct = xhr.getResponseHeader("content-type"),
     xml = type == "xml" || !type && ct && ct.indexOf("xml") >= 0,
     data = xml ? xhr.responseXML : xhr.responseText;
             if (xml && data.documentElement.tagName == "parsererror")
                 throw "parsererror";
             if (s && s.dataFilter)
                 data = s.dataFilter(data, type);
             if (typeof data === "string") {
                 if (type == "script")
                     jQuery.globalEval(data);
                 if (type == "json")
                     data = window["eval"]("(" + data + ")");
             }
             return data;
         }
     });
})(jQuery);
