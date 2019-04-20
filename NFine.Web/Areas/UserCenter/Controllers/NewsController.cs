using NFine.Application.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Code;
using NFine.Domain.Entity;

namespace NFine.Web.Areas.UserCenter.Controllers
{
    public class NewsController : ControllerBase
    {
        private NewsApp newsApp = new NewsApp();

        //
        // GET: /UserCenter/News/


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = newsApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = newsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SubmitForm(NewsEntity newsEntity, string keyValue)
        {
            newsEntity.F_Content = Request.Form["ueditor_textarea_F_Content"];
            newsApp.SubmitForm(newsEntity, keyValue);
            return Success("操作成功。");
        }
    }
}
