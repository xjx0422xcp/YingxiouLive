using NFine.Application.UserVip;
using NFine.Code;
using NFine.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.UserCenter.Controllers
{
    public class UserVipController : ControllerBase
    {

        private UserVipApp userVipApp = new UserVipApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = userVipApp.GetList(pagination, keyword),
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
            var data = userVipApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(UserVipEntity userEntity, string keyValue)
        {
            userVipApp.SubmitForm(userEntity, keyValue);
            return Success("操作成功。");
        }
        //[HttpPost]
        //[HandlerAuthorize]
        //[HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteForm(string keyValue)
        //{
        //    userApp.DeleteForm(keyValue);
        //    return Success("删除成功。");
        //}
        //[HttpGet]
        //public ActionResult RevisePassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        //{
        //    userLogOnApp.RevisePassword(userPassword, keyValue);
        //    return Success("重置密码成功。");
        //}
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult DisabledAccount(string keyValue)
        //{
        //    UserEntity userEntity = new UserEntity();
        //    userEntity.F_Id = keyValue;
        //    userEntity.F_EnabledMark = false;
        //    userApp.UpdateForm(userEntity);
        //    return Success("账户禁用成功。");
        //}
        //[HttpPost]
        //[HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult EnabledAccount(string keyValue)
        //{
        //    UserEntity userEntity = new UserEntity();
        //    userEntity.F_Id = keyValue;
        //    userEntity.F_EnabledMark = true;
        //    userApp.UpdateForm(userEntity);
        //    return Success("账户启用成功。");
        //}

        //[HttpGet]
        //public ActionResult Info()
        //{
        //    return View();
        //}

    }
}
