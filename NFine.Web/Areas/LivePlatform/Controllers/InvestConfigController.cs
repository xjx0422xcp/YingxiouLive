using NFine.Application.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Application.InvestConfig;
using NFine.Domain.Entity.InvestConfig;
using NFine.Web.Controllers;

namespace NFine.Web.Areas.LivePlatform.Controllers
{
    public class InvestConfigController : ControllerBase
    {
        private InvestConfigApp investConfigApp = new InvestConfigApp();
       // private GetDataController getDataController = new GetDataController();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = investConfigApp.GetList(pagination, keyword),
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
            var data = investConfigApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(InvestConfigEntity investConfigEntity, string keyValue)
        {
            investConfigApp.SubmitForm(investConfigEntity, keyValue);
            return Success("操作成功。");
        }
        //删除
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            investConfigApp.Delete(investConfigApp.GetForm(keyValue));
            return Success("删除成功。");
        }
        //新增、修改
        [HttpGet]
        [HandlerAuthorize]
        public  ActionResult Form1()
        {
            return View();
        }
    }
}
