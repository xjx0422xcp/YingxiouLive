using NFine.Application.InvestConfig;
using NFine.Application.News;
using NFine.Application.Order;
using NFine.Application.OrderItem;
using NFine.Application.UserSign;
using NFine.Application.UserVip;
using NFine.Application.UserVipRanking;
using NFine.Application.WealthLog;
using NFine.Application.Withdraw;
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.Entity.InvestConfig;
using NFine.Domain.Entity.UserSign;
using NFine.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Controllers
{
    public class AppController : Controller
    {

        private UserVipApp userVipApp = new UserVipApp();
        private WithdrawApp withdrawApp = new WithdrawApp();
        private WealthLogApp wealthLogApp = new WealthLogApp();
        private OrderApp orderApp = new OrderApp();
        private OrderItemApp orderItemApp = new OrderItemApp();
        private UserVipRankingApp userVipRankingApp = new UserVipRankingApp();
        private UserSignApp usApp = new UserSignApp();
        private NewsApp newsApp = new NewsApp();
        private log4net.ILog log = log4net.LogManager.GetLogger("AppController");

        private InvestConfigApp investConfigApp = new InvestConfigApp();
        //
        // GET: /App/
        #region index
        public JsonResult Index(string orderItemID)
        {

            return SubmitOrderItemFrom("2ffb7fc2-3cf4-4b73-ad08-f83dd1b39851", 2);
            return CheckLogin("13868837171", "ra330326");

            //return SubmitOrderItemFrom("034bac1a-eb9a-4607-aba7-3fcd6464f9ba");
            //    UserVipEntity entity = userVipApp.GetUserVipEntity("15901923941");
            //    //    return SubmitWithdrawCash(entity.F_UserName, 400);
            //    // return SubmitWithdrawCash(entity.F_Mobile, 580);
            //    int count = orderItemApp.GetOrderItemCount("879472f3-7ef8-443b-8f52-16d851fd57b7");
            //    Stack<UvrActionModel> stackUvrActionModel = new Stack<UvrActionModel>();
            //    userVipRankingApp.GetSuperiorList(0, 6, 0, ref stackUvrActionModel);

            //    List<UvrActionModel> listUvrActionModel = new List<UvrActionModel>();
            //    foreach (var item in stackUvrActionModel)
            //    {
            //        listUvrActionModel.Add(item);
            //    }
            //    userVipRankingApp.GetChildrenList(0, "6", 0, ref listUvrActionModel);

            //return Json(new
            //{
            //    data = stackUvrActionModel.ToArray()
            //}, JsonRequestBehavior.AllowGet);
            //string strjson = "{\"ItemList\":[{\"F_Id\":null,\"F_OrderID\":null,\"F_Amount\":58,\"F_TureName\":\"空白2019\",\"F_CardNo\":\"1111111111111111\",\"F_BankName\":\"中国银行\",\"F_Apliay\":\"15901923941\",\"F_WeiXin\":\"15901923941\",\"F_WithdrawId\":\"fb1dca35-81a1-4792-9b00-be76deb6e2d4\",\"F_CreatorUserId\":null,\"F_CreatorTime\":null,\"F_LastModifyUserId\":null,\"F_LastModifyTime\":null,\"F_Status\":null,\"OrderStatus\":\"未审核\"}],\"orderEntity\":{\"F_Id\":null,\"F_OrderNO\":null,\"F_UserID\":\"d25abab2-130e-465d-acc0-0fac6dbb08da\",\"F_Amount\":58,\"F_Status\":null,\"F_Title\":\"白银会员卡\",\"F_CreatorUserId\":null,\"F_CreatorTime\":null,\"F_LastModifyUserId\":null,\"F_LastModifyTime\":null,\"OrderStatus\":\"已提交\"}}";
            //// string orderItemID = "a79049ed-f13d-4d4e-ac29-6d5418692055";
            //UserVipRankingEntity t = new UserVipRankingEntity();
            //UserVipRankingEntity parentUserVipRankingEntity = userVipRankingApp.GetUvrEntity(23, 0);

            //UserVipRankingEntity tmpUvrEntity = new UserVipRankingEntity();
            //userVipRankingApp.GetSuperiorRanking(parentUserVipRankingEntity.F_SuperiorID + "", 0, ref tmpUvrEntity);
            ////if (tmpUvrEntity != null)
            ////{
            ////    UserVipRankingEntity tmpUserVipRankingEntity = userVipRankingApp.GetUvrEntity((int)tmpUvrEntity.F_SuperiorID, 0);
            ////    log.Info("tmpUvrEntity:" + tmpUvrEntity.ToJson());
            ////    userVipRankingApp.GetSuperiorRanking((int)tmpUserVipRankingEntity.F_SuperiorID + "", 0, ref t);

            ////    log.Info("tmpUvrEntity:" + tmpUvrEntity.ToJson());
            ////}

            //int maxUserID = userVipApp.GetUserVipList().Max(x => x.F_UserID).ToInt();
            //// return SubmitOrderItemFrom(orderItemID, 2);

            // return GetUserVipGraden("15901923941");
            // return GetOrderItemList("15901923941");
            //return GetOrderViewList("13138856513");
            //UserVipEntity entity = new UserVipEntity()
            //{
            //    F_UserID = 0,
            //    F_Account = "15901923941",
            //    F_ActiveTime = DateTime.Now,
            //    F_Coin = 1500,
            //    F_CreateTime = DateTime.Now,
            //    F_Enable = false,
            //    F_Integral = 15000,
            //    F_Mobile = "15901923941",
            //    F_NickName = "空白2018",
            //    F_ParentID = 0,
            //    F_UserName = "15901923941",
            //    F_UserPass = "123456",
            //    F_Position = 0,
            //    F_CreatorUserId = "",
            //    F_CreatorTime = DateTime.Now,
            //    F_Id = ""
            //};

            ////s
            //userVipApp.SubmitForm(entity, entity.F_Id);

            // return SendSmsCode("13138856513", 1);


            // bool isExists=   userVipApp.IsExistsSysUser("13138856513");
            var result = new
            {
                code = 1,
                msg = "成功",
                data = "",
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 用户登录，发送手机验证码，忘记密码
        [HttpPost]
        public JsonResult CheckLogin(string username, string password)
        {
            log.Info(string.Format("CheckLogin StartTime:{0}", DateTime.Now));
            int code = 0;
            string msg = "";

            UserVipEntity entity = userVipApp.CheckLogin(username, password);
            if (entity == null)
            {
                code = 1;
                msg = "账号或密码错误";
            }
            log.Info(string.Format("CheckLogin EndTime:{0}", DateTime.Now));
            return Json(new
            {
                code = code,
                msg = msg,
                data = entity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendSmsCode(string mobile, int stype = 0)
        {
            log.Info(string.Format("SendSmsCode StartTime:{0}", DateTime.Now));

            log.Info(string.Format("SendSmsCode Values mobile:{0},type:{1}", mobile, stype));

            int code = 0;
            string msg = "";
            string data = "";
            if (stype > 0)
            {
                if (userVipApp.IsExistsSysUser(mobile))
                {
                    code = 1;
                    msg = "账号不存在";
                }
            }
            if (code == 0)
            {
                data = Common.RndNum(6);
                TempData["smsCode"] = data;

                System.Net.WebClient client = new System.Net.WebClient();//System.Net.Webclient类从特定的URL请求文件
                client.Credentials = System.Net.CredentialCache.DefaultCredentials;//获取或设置发送到主机并用于对请求进行身份验证的网 络凭据。
                string userName = System.Configuration.ConfigurationManager.AppSettings["smsUser"].ToString();//获取用户名即短信宝帐户
                string pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(System.Configuration.ConfigurationManager.AppSettings["smsPass"].ToString(), "MD5");//短信宝帐户密码经md5加密
                string content = "【58区块链学院】您的验证码为 " + data + "，在10分钟内有效。";
                content = System.Web.HttpUtility.UrlEncode(content, System.Text.Encoding.UTF8);//短信发送内容并进行 urlencode编码
                byte[] result = client.DownloadData("http://api.smsbao.com/sms?u=" + userName + "&p=" + pass + "&m=" + mobile + "&c=" + content);// 向远程网址发送请求并获取返回值保存至字节数组
                string sres = Encoding.UTF8.GetString(result);

                if (sres != "0")
                {
                    code = 1;
                    data = "";
                    msg = "发送失败，错误码为：" + sres;
                }
            }

            log.Info(string.Format("SendSmsCode EndTime:{0}", DateTime.Now));
            return Json(new
            {
                code = code,
                msg = msg,
                data = data
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ForgotPassword(string mobile, string smsCode, string password, string surePass)
        {

            int code = 0;
            string msg = "";
            string data = "";
            UserVipEntity entity = userVipApp.GetUserVipEntity(mobile);
            if (entity != null)
            {
                entity.F_UserPass = Md5.md5(DESEncrypt.Encrypt(password.ToLower()).ToLower(), 32).ToLower();
                userVipApp.SubmitForm(entity, entity.F_Id);
            }
            else
            {
                code = 1;
                msg = "账号不存在";
            }



            return Json(new
            {
                code = code,
                msg = msg,
                data = data
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 提现
        [HttpPost]
        public JsonResult SubmitWithdrawCash(string mobile, int point, int type)
        {
            int code = 0;
            string msg = "";
            string data = "";
            try
            {
                UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
                UserVipRankingEntity uvrEntity = userVipRankingApp.GetUvrEntity((int)userVipEntity.F_UserID, type);
                if (uvrEntity == null)
                {
                    #region 提现处理
                    int[] arrayNum = new int[] { 100, 400, 1000, 1600, 4000 };
                    if (point % 580 == 0 || point % 5800 == 0)
                    {
                        if (userVipEntity != null)
                        {
                            int integral = type == 0 ? (int)userVipEntity.F_Integral : (int)userVipEntity.F_Integral1;

                            if (point <= integral)
                            {
                                #region 执行
                                using (var tranScope = new TransactionScope())
                                {
                                    WithdrawEntity withdrawEntity = new WithdrawEntity()
                                    {
                                        F_Surplus = point,
                                        F_Amount = point,
                                        F_UserID = userVipEntity.F_Id,
                                        F_CreatorTime = DateTime.Now,
                                        F_Status = 0
                                    };
                                    withdrawApp.SubmitForm(withdrawEntity, withdrawEntity.F_Id);
                                    WealthLogEntity wealthLogEntity = new WealthLogEntity()
                                    {
                                        F_UserID = userVipEntity.F_Id,
                                        F_Integral = point,
                                        F_OldIntegral = integral,
                                        F_CoinType = 1,
                                        F_Type = 2,
                                        F_Note = "提现"
                                    };
                                    wealthLogApp.SubmitForm(wealthLogEntity, wealthLogEntity.F_Id);
                                    if (type == 0)
                                    {
                                        userVipEntity.F_Integral = userVipEntity.F_Integral - point;
                                    }
                                    else
                                    {
                                        userVipEntity.F_Integral1 = userVipEntity.F_Integral1 - point;
                                    }
                                    userVipApp.SubmitForm(userVipEntity, userVipEntity.F_Id);
                                    tranScope.Complete();
                                }
                                #endregion
                            }
                            else
                            {
                                code = 1;
                                msg = "可提积分不足";
                            }
                        }
                    }
                    else
                    {
                        code = 1;
                        msg = "提现积分必须是580或5800的整数倍";
                    }
                    #endregion 
                }
                else
                {
                    code = 1;
                    msg = "您还没有出局，不能提现";
                }
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                msg = "程序异常，error:" + ex.Message;
                code = 1;
            }
            finally { }



            return Json(new
            {
                code = code,
                msg = msg,
                data = data
            });
        }

        public JsonResult GetWithdrawRecord(string mobile)
        {
            List<WithdrawEntity> listWithdraw = null;
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            if (userVipEntity != null)
            {
                listWithdraw = withdrawApp.GetList(userVipEntity.F_Id);
            }
            return Json(new
            {
                code = 0,
                msg = "",
                data = listWithdraw.Select(x => new
                {
                    x.F_Amount,
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yyyy年MM月dd日"),
                    F_Status = x.F_Status == 0 ? "提款中" : "已完成"
                })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 申请激活
        public JsonResult getAlipayList(string mobile, decimal amount)
        {
            string msg = ""; int code = 0;
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            OrderActionModel model = new OrderActionModel();
            try
            {
                int vtype = amount > 580 ? 1 : 0;
                UserVipRankingEntity userVipRankingEntity = userVipRankingApp.GetUserVipRankingEntity((int)userVipEntity.F_UserID, vtype);
                if (userVipRankingEntity == null)
                {
                    OrderEntity orderEntity = new OrderEntity()
                    {
                        F_Amount = amount,
                        F_UserID = userVipEntity.F_Id,
                        F_Title = amount > 580 ? "白金会员卡" : "白银会员卡"
                    };
                    model.orderEntity = orderEntity;
                    if (userVipEntity != null)
                    {
                        List<WithdrawEntity> listWithdraw = withdrawApp.GetWithdrawList(userVipEntity.F_Id);//所有未完成提款的记录

                        if (listWithdraw != null && listWithdraw.Count > 0)
                        {
                            int index = 0;
                            if (listWithdraw.Count >= 4)
                            {
                                index = 3;
                            }

                            UserVipEntity firstUserVipEntity = userVipApp.GetForm(listWithdraw[index].F_UserID);
                            decimal fAmount = amount < listWithdraw[index].F_Surplus ? amount : (decimal)listWithdraw[index].F_Surplus;//58<42 取42 取小
                            model.ItemList.Add(new OrderItemEntity
                            {
                                F_Amount = fAmount,
                                F_Apliay = firstUserVipEntity.F_Apliay,
                                F_CardNo = firstUserVipEntity.F_CardNo,
                                F_BankName = firstUserVipEntity.F_BankName,
                                F_Certificate = "",
                                F_TureName = firstUserVipEntity.F_TureName,
                                F_WeiXin = firstUserVipEntity.F_WeiXin,
                                F_WithdrawId = listWithdraw[index].F_Id
                            });
                            if (fAmount != amount)
                            {
                                decimal? secondFAmount = amount - fAmount;
                                Random r = new Random();
                                int tmp = r.Next(0, 2);
                                WithdrawEntity tmpWithdrawEntity = listWithdraw[tmp];

                                UserVipEntity secondUserVipEntity = userVipApp.GetForm(tmpWithdrawEntity.F_UserID);
                                model.ItemList.Add(new OrderItemEntity
                                {
                                    F_Amount = secondFAmount,
                                    F_Apliay = secondUserVipEntity.F_Apliay,
                                    F_CardNo = secondUserVipEntity.F_CardNo,
                                    F_BankName = secondUserVipEntity.F_BankName,
                                    F_Certificate = "",
                                    F_TureName = secondUserVipEntity.F_TureName,
                                    F_WeiXin = secondUserVipEntity.F_WeiXin,
                                    F_WithdrawId = tmpWithdrawEntity.F_Id
                                });
                            }
                        }
                        else
                        {
                            code = 1;
                            msg = "没有收款人信息了！！！";
                        }
                    }
                }
                else
                {
                    code = 1;
                    msg = "已经是Vip了，不可重复！！！";
                }
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                code = 1;
                msg = "程序出错了";
            }
            finally
            {

            }
            return Json(new
            {
                code = code,
                msg = msg,
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitOrderForm(string mobile, string orderActionModelJson)
        {
            log.Info(orderActionModelJson);
            int code = 0;
            string msg = "";
            OrderActionModel model = orderActionModelJson.ToObject<OrderActionModel>();
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            List<OrderEntity> listOrder = orderApp.GetList(userVipEntity.F_Id);
            int count = listOrder.Where(x => x.F_Amount == model.orderEntity.F_Amount && x.F_Status != 3).Count();
            if (count > 0)
            {
                code = 1;
                msg = "不能重复提交订单";
            }
            else
            {
                foreach (var item in model.ItemList)
                {
                    WithdrawEntity tmpWithdrawEntity = withdrawApp.GetForm(item.F_WithdrawId);
                    if (tmpWithdrawEntity.F_Surplus < item.F_Amount)
                    {
                        code = 1;
                        msg = "数据失效，请刷新";
                        break;
                    }
                }
                try
                {
                    int uvrType = model.orderEntity.F_Amount == 580 ? 0 : 1;
                    UserVipRankingEntity userVipRankingEntity = userVipRankingApp.GetUvrEntity((int)userVipEntity.F_UserID, uvrType);
                    if (userVipRankingEntity != null)
                    {
                        code = 1;
                        msg = "您已经成为了Vip，不必重新申请";
                    }
                    if (code == 0)
                    {

                        if (userVipEntity != null)
                        {
                            using (var tranScope = new TransactionScope())
                            {
                                model.orderEntity.F_Status = 0;
                                model.orderEntity.F_UserID = userVipEntity.F_Id;
                                orderApp.SubmitForm(model.orderEntity, model.orderEntity.F_Id);
                                foreach (var item in model.ItemList)
                                {
                                    item.F_OrderID = model.orderEntity.F_Id;
                                    item.F_Status = 0;
                                    orderItemApp.SubmitForm(item, item.F_Id);

                                    WithdrawEntity withdrawEntity = withdrawApp.GetForm(item.F_WithdrawId);
                                    withdrawEntity.F_Surplus = (decimal)withdrawEntity.F_Surplus - (decimal)item.F_Amount;
                                    if (withdrawEntity.F_Surplus == 0)
                                    {
                                        withdrawEntity.F_Status = 1;
                                    }
                                    withdrawApp.SubmitForm(withdrawEntity, withdrawEntity.F_Id);
                                }
                                tranScope.Complete();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Info("SubmitOrderFormException:" + ex.Message);
                    code = 1;
                    msg = "发生异常";
                }
                finally
                {

                }
            }
            return Json(new
            {
                code = code,
                msg = msg,
                data = ""
            });
        }

        public JsonResult getOrderRecord(string mobile)
        {
            List<OrderEntity> listOrder = null;
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            if (userVipEntity != null)
            {
                listOrder = orderApp.GetList(userVipEntity.F_Id);
            }
            return Json(new
            {
                code = 0,
                msg = "",
                data = listOrder.Select(x => new
                {
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yyyy年MM月dd日"),
                    x.F_Amount,
                    x.F_Title,
                    x.F_Status,
                    x.OrderStatus,
                    //F_Status = x.F_Status == 0 ? "审核中" : (x.F_Status == 1 ? "已完成" : "审核失败")
                })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 上传文件
        [HttpPost]
        public JsonResult SavaUploadFile(HttpPostedFileBase[] fileToUpload)
        {
            int code = 0;
            string msg = "";
            List<string> strList = new List<string>();
            foreach (HttpPostedFileBase imageName in fileToUpload)
            {
                string file = imageName.FileName;
                string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
                Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
                if (!(string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat))) // 验证后缀，判断文件是否是所要上传的格式
                {
                    string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                    string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                    string imageStr = string.Format("/Upload/{0}", DateTime.Now.ToString("yy-MM-dd")); // 获取保存图片的项目文件夹
                    string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
                    string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                    string saveFile = uploadPath + "/" + fileName;//文件路径
                    imageName.SaveAs(saveFile);// 保存文件如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                    strList.Add(imageStr + "/" + fileName);
                }
            }
            return Json(new
            {
                code = code,
                msg = msg,
                data = strList
            }, "text/html", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 注册新用户
        [HttpPost]
        public JsonResult register(UserVipActionModel model)
        {
            int code = 0;
            string msg = "";
            string data = "";
            string smsValidateCode = Request.Form["smsValidateCode"];

            if (smsValidateCode.Equals(TempData["smsCode"]))
            {
                if (userVipApp.IsExistsSysUser(model.mobile))
                {
                    if (!userVipApp.IsExistsSysUser(model.ReferMobile))
                    {
                        UserVipEntity parentUserVipEntity = userVipApp.GetUserVipEntity(model.ReferMobile);
                        int maxUserID = userVipApp.GetUserVipList().Max(x => x.F_UserID).ToInt() + 1;
                        string result = (maxUserID + "").PadLeft(8, '0');
                        UserVipEntity entity = new UserVipEntity()
                        {
                            F_Account = "BX" + result,
                            F_ActiveTime = DateTime.Now,
                            F_Status = 0,
                            F_Coin = 0,
                            F_CreateTime = DateTime.Now,
                            F_Enable = false,
                            F_Integral = 0,
                            F_Mobile = model.mobile,
                            F_NickName = model.mobile,
                            F_ParentID = parentUserVipEntity.F_UserID,
                            F_UserName = model.mobile,
                            F_UserPass = Md5.md5(DESEncrypt.Encrypt(model.UserPass.ToLower()).ToLower(), 32).ToLower(),
                            F_Position = 0,
                            F_CreatorUserId = "",
                            F_LockCoin = 10000,
                            F_PrivateCoin = 0,
                            F_Integral1 = 0,
                            F_CreatorTime = DateTime.Now,
                            F_TodayExpediteCoin = 0,
                            F_TodayRelaseCoin = 0,
                            F_Id = ""
                        };
                        userVipApp.SubmitForm(entity, entity.F_Id);
                    }
                    else
                    {
                        code = 1;
                        msg = "邀请人不存在";
                    }
                }
                else
                {
                    code = 1;
                    msg = "手机号已注册";
                }
            }
            else
            {
                code = 1;
                msg = "验证码错误";
            }

            return Json(new
            {
                code = code,
                msg = msg,
                data = data
            });
        }

        [HttpPost]
        public JsonResult RegisterApi(string strUserVipActionModel)
        {
            log.Info("RegisterApi:" + strUserVipActionModel);
            int code = 0;
            string msg = "";
            string data = "";

            UserVipActionModel model = strUserVipActionModel.ToObject<UserVipActionModel>();
            if (userVipApp.IsExistsSysUser(model.mobile))
            {
                if (!userVipApp.IsExistsSysUser(model.ReferMobile))
                {
                    UserVipEntity parentUserVipEntity = userVipApp.GetUserVipEntity(model.ReferMobile);
                    int maxUserID = userVipApp.GetUserVipList().Max(x => x.F_UserID).ToInt() + 1;
                    string result = (maxUserID + "").PadLeft(8, '0');
                    UserVipEntity entity = new UserVipEntity()
                    {
                        F_Account = "BX" + result,
                        F_ActiveTime = DateTime.Now,
                        F_Status = 0,
                        F_Coin = 0,
                        F_CreateTime = DateTime.Now,
                        F_Enable = false,
                        F_Integral = 0,
                        F_Mobile = model.mobile,
                        F_NickName = model.mobile,
                        F_ParentID = parentUserVipEntity.F_UserID,
                        F_UserName = model.mobile,
                        F_UserPass = Md5.md5(DESEncrypt.Encrypt(model.UserPass.ToLower()).ToLower(), 32).ToLower(),
                        F_Position = 0,
                        F_CreatorUserId = "",
                        F_LockCoin = 10000,
                        F_PrivateCoin = 0,
                        F_Integral1 = 0,
                        F_CreatorTime = DateTime.Now,
                        F_TodayExpediteCoin = 0,
                        F_TodayRelaseCoin = 0,
                        F_Id = ""
                    };
                    userVipApp.SubmitForm(entity, entity.F_Id);
                }
                else
                {
                    code = 1;
                    msg = "邀请人不存在";
                }
            }
            else
            {
                code = 1;
                msg = "手机号已注册";
            }


            return Json(new
            {
                code = code,
                msg = msg,
                data = data
            });
        }
        #endregion

        #region 个人信息
        public JsonResult GetUserVipEntity(string mobile)
        {
            log.Info(string.Format("GetUserVipEntity StartTime:{0}", DateTime.Now));
            int code = 0;
            string msg = "";
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);

            log.Info(string.Format("GetUserVipEntity EndTime:{0}", DateTime.Now));
            return Json(new
            {
                code = code,
                msg = msg,
                data = userVipEntity
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserVipEntityExtend()
        {
            int code = 0;
            string msg = "";
            List<UserVipRankingEntity> listUserVipRanking = userVipRankingApp.GetUserVipRankingList();
            List<UserVipEntity> listUserVip = userVipApp.GetUserVipList();
            return Json(new
            {
                code = code,
                msg = msg,
                data = new
                {
                    vipCount = 5200 + listUserVipRanking.GroupBy(x => x.F_UserID).Count(),
                    userCount = 10000 + listUserVip.Count,
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserVipGraden(string mobile)
        {
            int code = 0;
            string msg = "";
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            List<UserVipRankingEntity> listUserVipRanking = userVipRankingApp.GetUserVipRankingEntityList((int)userVipEntity.F_UserID);
            return Json(new
            {
                code = code,
                msg = msg,
                data = listUserVipRanking.Select(x => new
                {
                    x.UserGarden
                })
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SubmitUserVipEntity(string mobile, string entity)
        {
            int code = 0;
            string msg = "";
            UserVipEntity model = entity.ToObject<UserVipEntity>();
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            userVipEntity.F_TureName = model.F_TureName;
            userVipEntity.F_NickName = model.F_NickName;
            userVipEntity.F_WeiXin = model.F_WeiXin;
            userVipEntity.F_Apliay = model.F_Apliay;
            userVipEntity.F_BankName = model.F_BankName;
            userVipEntity.F_CardNo = model.F_CardNo;
            userVipEntity.F_PurseAddress = model.F_PurseAddress;
            userVipEntity.F_HeadImg = model.F_HeadImg;
            userVipApp.SubmitForm(userVipEntity, userVipEntity.F_Id);
            return Json(new
            {
                code = code,
                msg = msg,
                data = ""
            });
        }


        [HttpPost]
        public JsonResult UpdateUserVipPassword(string mobile, string password, string newpassword)
        {
            int code = 0;
            string msg = "";
            UserVipEntity userVipEntity = userVipApp.GetUserVipEntity(mobile);
            string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower()).ToLower(), 32).ToLower();
            if (dbPassword == userVipEntity.F_UserPass)
            {
                userVipEntity.F_UserPass = Md5.md5(DESEncrypt.Encrypt(newpassword.ToLower()).ToLower(), 32).ToLower();
                userVipApp.SubmitForm(userVipEntity, userVipEntity.F_Id);
            }
            else
            { 
                code = 1; 
                msg = "原密码不对";
            }
            return Json(new
            {
                code = code,
                msg = msg,
                data = ""
            });
        }

        public JsonResult GetUserVipChildren(string mobile)
        {

            int code = 0;
            string msg = "";
            UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
            List<UserVipEntity> userList = userVipApp.GetListChildren((int)uvEntity.F_UserID);
            var result = new
            {
                code = code,
                msg = msg,
                data = userList.Select(x => new
                {
                    x.F_Account,
                    x.F_UserName,
                    x.F_Type,//类型，1  增加，2  减少
                    x.F_Integral,//数量
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yy-MM-dd"),//日期
                    //展示信息
                    // 类型       数量         备注        日期
                    //类型为增加，字颜色为红色
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 排位双轨
        public JsonResult GetRankingData(string mobile, int type)
        {

            int code = 0;
            string msg = "";
            List<UvrActionModel> listUvrActionModel = new List<UvrActionModel>();
            try
            {
                UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
                UserVipRankingEntity uvrEntity = userVipRankingApp.GetUvrEntity((int)uvEntity.F_UserID, type);
                if (uvrEntity != null)
                {
                    Stack<UvrActionModel> stackUvrActionModel = new Stack<UvrActionModel>();
                    userVipRankingApp.GetSuperiorList(0, (int)uvEntity.F_UserID, type, ref stackUvrActionModel);
                    int index = 4;
                    foreach (var item in stackUvrActionModel)
                    {
                        item.UvrModelList[0].layer = index;
                        listUvrActionModel.Add(item);
                        index--;
                    }
                    userVipRankingApp.GetChildrenList(0, (int)uvEntity.F_UserID + "", type, ref listUvrActionModel);
                }
            }
            catch (Exception ex)
            {
                code = 1;
                msg = ex.Message;
            }
            finally
            {

            }
            var result = new
            {
                code = code,
                msg = msg,
                data = listUvrActionModel,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 收款确认
        public JsonResult GetOrderItemList(string mobile)
        {
            int code = 0;
            string msg = "";

            List<OrderItemEntity> listOrderItem = orderItemApp.GetOrderItemList(mobile);
            List<OrderItemActionModel> listOrderItemAction = new List<OrderItemActionModel>();
            foreach (var item in listOrderItem)
            {
                UserVipEntity userVipEntity = userVipApp.GetUserVipEntityOrderID(item.F_OrderID);
                listOrderItemAction.Add(new OrderItemActionModel()
                {
                    OrderStatus = item.OrderStatus,
                    UserName = userVipEntity == null ? "" : userVipEntity.F_UserName,
                    FAmount = (decimal)item.F_Amount,
                    FID = item.F_Id,
                    NickName = userVipEntity == null ? "" : userVipEntity.F_NickName,
                    CreateTime = ((DateTime)item.F_CreatorTime).ToString("MM-dd")
                });
            }

            var result = new
            {
                code = code,
                msg = msg,
                data = listOrderItemAction
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetOrderItemInfo(string orderItemID)
        {
            int code = 0;
            string msg = "";

            OrderItemEntity entity = orderItemApp.GetForm(orderItemID);
            var result = new
            {
                code = code,
                msg = msg,
                data = new
                {
                    entity,
                    userInfo = userVipApp.GetUserVipEntityOrderID(entity.F_OrderID)
                },
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收款提交
        /// </summary>
        /// <param name="orderItemID">FID</param>
        /// <param name="status">1：拒绝 2 通过</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmitOrderItemFrom(string orderItemID, int status)
        {
            int code = 0;
            string msg = "";
            try
            {
                if (status == 2)
                {
                    #region 通过
                    OrderItemEntity orderItemEntity = orderItemApp.GetForm(orderItemID);
                    if (orderItemEntity.F_Status != status)
                    {
                        using (var tranScope = new TransactionScope())
                        {
                            orderItemEntity.F_Status = status;
                            orderItemApp.SubmitForm(orderItemEntity, orderItemEntity.F_Id);

                            int count = orderItemApp.GetOrderItemCount(orderItemEntity.F_OrderID, status);
                            if (count == 0)
                            {
                                OrderEntity orderEntity = orderApp.GetForm(orderItemEntity.F_OrderID);
                                orderEntity.F_Status = 3;
                                orderApp.SubmitForm(orderEntity, orderEntity.F_Id);
                                int amount = (int)orderEntity.F_Amount;
                                int point = 500;
                                int type = 0;
                                if (amount == 5800)
                                {
                                    point = 5000;
                                    type = 1;
                                }
                                UserVipEntity userVipEntity = userVipApp.GetForm(orderEntity.F_UserID);
                                userVipEntity.F_Coin = userVipEntity.F_Coin + orderEntity.F_Amount;
                                userVipEntity.F_Status = 1;
                                userVipEntity.F_VipType = 1;
                                userVipEntity.F_Type = type;
                                userVipApp.SubmitForm(userVipEntity, userVipEntity.F_Id);

                                UserVipEntity parentUserVipEntity = userVipApp.GetUserVipEntityByUserID((int)userVipEntity.F_ParentID);
                                parentUserVipEntity.F_TodayExpediteCoin = parentUserVipEntity.F_TodayExpediteCoin + amount;
                                userVipApp.SubmitForm(parentUserVipEntity, parentUserVipEntity.F_Id);


                                wealthLogApp.SubmitForm(new WealthLogEntity()
                                {
                                    F_UserID = userVipEntity.F_Id,
                                    F_Coin = orderEntity.F_Amount,
                                    F_OldCoin = userVipEntity.F_Coin,
                                    F_CoinType = 2,
                                    F_Type = 1,
                                    F_Note = ""
                                }, "");
                                #region 开始排位

                                #region 找上级
                                UserVipRankingEntity uvrEntity = new UserVipRankingEntity();
                                uvrEntity = userVipRankingApp.GetUserVipRankingEntity((int)userVipEntity.F_ParentID, type);
                                if (uvrEntity == null)
                                {
                                    UserVipRankingEntity tmpUserVipRankingEntity = new UserVipRankingEntity();
                                    GetUserVipRankingEntity((int)userVipEntity.F_ParentID, type, ref tmpUserVipRankingEntity);
                                    if (tmpUserVipRankingEntity != null)
                                    {
                                        userVipRankingApp.GetSuperiorRanking((int)tmpUserVipRankingEntity.F_SuperiorID + "", type, ref uvrEntity);
                                    }
                                }
                                #endregion 找上级
                                if (uvrEntity != null)
                                {
                                    uvrEntity.F_Integral = uvrEntity.F_Integral + point;
                                    if (uvrEntity.F_LeftUserID > 0)
                                    {
                                        uvrEntity.F_RightUserID = userVipEntity.F_UserID;
                                        uvrEntity.F_Status = 1;
                                    }
                                    else
                                    {
                                        uvrEntity.F_LeftUserID = userVipEntity.F_UserID;
                                    }
                                    userVipRankingApp.SubmitForm(uvrEntity, uvrEntity.F_Id);

                                    #region =======================================给上级加积分=======================================
                                    UserVipEntity superiorEntity = userVipApp.GetUserVipEntityByUserID((int)uvrEntity.F_UserID);//上级
                                    if (point == 50)
                                    {
                                        superiorEntity.F_Integral = superiorEntity.F_Integral + point;
                                    }
                                    else
                                    {
                                        superiorEntity.F_Integral1 = superiorEntity.F_Integral1 + point;
                                    }
                                    userVipApp.SubmitForm(superiorEntity, superiorEntity.F_Id);
                                    wealthLogApp.SubmitForm(new WealthLogEntity()
                                    {
                                        F_UserID = superiorEntity.F_Id,
                                        F_Integral = point,
                                        F_OldIntegral = superiorEntity.F_Integral,
                                        F_CoinType = 1,
                                        F_Type = 1,
                                    }, "");
                                    #endregion

                                    userVipRankingApp.SubmitForm(new UserVipRankingEntity()
                                    {
                                        F_RightUserID = 0,
                                        F_LeftUserID = 0,
                                        F_SuperiorID = uvrEntity.F_UserID,
                                        F_ParentID = userVipEntity.F_ParentID,
                                        F_UserID = userVipEntity.F_UserID,
                                        F_Status = 0,
                                        F_Type = type,
                                    }, "");
                                    if (uvrEntity.F_Status == 1)
                                    {
                                        superiorEntity.F_Coin = superiorEntity.F_Coin - orderEntity.F_Amount;
                                        userVipApp.SubmitForm(superiorEntity, superiorEntity.F_Id);

                                        wealthLogApp.SubmitForm(new WealthLogEntity()
                                        {
                                            F_UserID = userVipEntity.F_Id,
                                            F_Coin = orderEntity.F_Amount,
                                            F_OldCoin = userVipEntity.F_Coin,
                                            F_CoinType = 2,
                                            F_Type = 2,
                                            F_Note = ""
                                        }, "");
                                        userVipRankingApp.Delete(uvrEntity);
                                    }
                                    //
                                    if (1 == 2)
                                    {
                                        #region =======================================会员自动升级========================================
                                        UserVipRankingEntity superUserVipRankingEntity = userVipRankingApp.GetUvrEntity((int)uvrEntity.F_SuperiorID, type);//
                                        if (superUserVipRankingEntity != null)
                                        {
                                            UserVipRankingEntity parentUserVipRankingEntity = userVipRankingApp.GetUvrEntity((int)superUserVipRankingEntity.F_SuperiorID, type);
                                            UserVipEntity superUserVipEntity = userVipApp.GetUserVipEntityByUserID((int)superUserVipRankingEntity.F_SuperiorID);//上上级
                                            if (superUserVipEntity != null)
                                            {
                                                #region =======================================================操作上上级===============================================
                                                if (point == 50 && uvrEntity.F_Integral == 100)
                                                {
                                                    superiorEntity.F_Integral = superiorEntity.F_Integral - 100;
                                                    userVipApp.SubmitForm(superiorEntity, superiorEntity.F_Id);

                                                    uvrEntity.F_Integral = uvrEntity.F_Integral - 100;
                                                    userVipRankingApp.SubmitForm(uvrEntity, uvrEntity.F_Id);
                                                    wealthLogApp.SubmitForm(new WealthLogEntity()
                                                    {
                                                        F_UserID = superiorEntity.F_Id,
                                                        F_Integral = 100,
                                                        F_OldIntegral = superiorEntity.F_Integral,
                                                        F_CoinType = 1,
                                                        F_Type = 2,
                                                    }, "");
                                                    superUserVipEntity.F_Integral = superUserVipEntity.F_Integral + 100;
                                                    userVipApp.SubmitForm(superUserVipEntity, superUserVipEntity.F_Id);

                                                    parentUserVipRankingEntity.F_Integral = parentUserVipRankingEntity.F_Integral + 100;
                                                    userVipRankingApp.SubmitForm(parentUserVipRankingEntity, parentUserVipRankingEntity.F_Id);
                                                    wealthLogApp.SubmitForm(new WealthLogEntity()
                                                    {
                                                        F_UserID = superUserVipEntity.F_Id,
                                                        F_Integral = 100,
                                                        F_OldIntegral = superUserVipEntity.F_Integral,
                                                        F_CoinType = 1,
                                                        F_Type = 1,
                                                    }, "");
                                                    if (parentUserVipRankingEntity.F_Integral == 400)
                                                    {
                                                        UserVipRankingEntity delUserVipRankingEntity = userVipRankingApp.GetUvrEntity((int)superUserVipEntity.F_UserID, type);
                                                        if (delUserVipRankingEntity.F_UserID > 8)
                                                        {
                                                            userVipRankingApp.Delete(delUserVipRankingEntity);
                                                        }
                                                    }

                                                }
                                                else if (point == 500 && uvrEntity.F_Integral == 1000)
                                                {
                                                    superiorEntity.F_Integral1 = superiorEntity.F_Integral1 - 1000;
                                                    userVipApp.SubmitForm(superiorEntity, superiorEntity.F_Id);

                                                    uvrEntity.F_Integral = uvrEntity.F_Integral - 1000;
                                                    userVipRankingApp.SubmitForm(uvrEntity, uvrEntity.F_Id);

                                                    wealthLogApp.SubmitForm(new WealthLogEntity()
                                                    {
                                                        F_UserID = superiorEntity.F_Id,
                                                        F_Integral = 1000,
                                                        F_OldIntegral = superiorEntity.F_Integral1,
                                                        F_CoinType = 1,
                                                        F_Type = 2,
                                                    }, "");

                                                    superUserVipEntity.F_Integral1 = superUserVipEntity.F_Integral1 + 1000;
                                                    userVipApp.SubmitForm(superUserVipEntity, superUserVipEntity.F_Id);

                                                    parentUserVipRankingEntity.F_Integral = parentUserVipRankingEntity.F_Integral + 1000;
                                                    userVipRankingApp.SubmitForm(parentUserVipRankingEntity, parentUserVipRankingEntity.F_Id);

                                                    wealthLogApp.SubmitForm(new WealthLogEntity()
                                                    {
                                                        F_UserID = superUserVipEntity.F_Id,
                                                        F_Integral = 1000,
                                                        F_OldIntegral = superUserVipEntity.F_Integral1,
                                                        F_CoinType = 1,
                                                        F_Type = 1,
                                                    }, "");

                                                    if (superUserVipEntity.F_Integral1 == 4000)
                                                    {
                                                        UserVipRankingEntity delUserVipRankingEntity = userVipRankingApp.GetUvrEntity((int)superUserVipEntity.F_UserID, type);
                                                        userVipRankingApp.Delete(delUserVipRankingEntity);
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    userVipRankingApp.SubmitForm(new UserVipRankingEntity()
                                    {
                                        F_RightUserID = 0,
                                        F_LeftUserID = 0,
                                        F_SuperiorID = 0,
                                        F_Integral = 0,
                                        F_ParentID = userVipEntity.F_ParentID,
                                        F_UserID = userVipEntity.F_UserID,
                                        F_Status = 0,
                                        F_Type = type,
                                    }, "");
                                }
                                #endregion 结束排位
                            }
                            tranScope.Complete();
                        }
                    }
                    else
                    {
                        code = 1;
                        msg = "已经确认，不必在点击";
                    }
                    #endregion
                }
                else
                {
                    #region 拒绝
                    OrderItemEntity orderItemEntity = orderItemApp.GetForm(orderItemID);
                    if (orderItemEntity.F_Status != status)
                    {
                        using (var tranScope = new TransactionScope())
                        {
                            orderItemEntity.F_Status = status;
                            orderItemApp.SubmitForm(orderItemEntity, orderItemEntity.F_Id);

                            int count = orderItemApp.GetOrderItemCount(orderItemEntity.F_OrderID, status);
                            if (count == 0)
                            {
                                OrderEntity orderEntity = orderApp.GetForm(orderItemEntity.F_OrderID);
                                orderEntity.F_Status = 2;
                                orderApp.SubmitForm(orderEntity, orderEntity.F_Id);
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                code = 1;
                msg = "程序异常：error:" + ex.Message;
            }
            finally
            {

            }

            var result = new
            {
                code = code,
                msg = msg,
                data = ""
            };
            return Json(result);
        }
        /// <summary>
        /// 获得推荐人的上级，如果没有找到，则递归出上上级。
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="type"></param>
        /// <param name="userVipRankingEntity"></param>
        public void GetUserVipRankingEntity(int UserID, int type, ref UserVipRankingEntity userVipRankingEntity)
        {
            UserVipRankingEntity tmpUserVipRankingEntity = userVipRankingApp.GetUvrEntity(UserID, type);
            if (tmpUserVipRankingEntity == null)
            {
                UserVipEntity userVipEntity = userVipApp.GetUserVipEntityByUserID(UserID);
                if (userVipEntity != null)
                {
                    GetUserVipRankingEntity((int)userVipEntity.F_ParentID, type, ref userVipRankingEntity);
                }
                else
                {
                    userVipRankingEntity = null;
                }
            }
            else
            {
                userVipRankingEntity = tmpUserVipRankingEntity;
            }
            //userVipRankingApp.GetSuperiorRanking((int)tmpUserVipRankingEntity.F_SuperiorID + "", type, ref uvrEntity);
        }

        #endregion

        #region 签到
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmitUserVipSign(string mobile)
        {
            int code = 0;
            string msg = "";
            int data = 0;
            try
            {
                int coin = 0;
                UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
                List<UserVipRankingEntity> listUserVipRanking = userVipRankingApp.GetUserVipRankingEntityList((int)uvEntity.F_UserID);
                if (listUserVipRanking.Count > 0)
                {
                    if (listUserVipRanking.Count == 1)
                    {
                        UserVipRankingEntity uvrEntity = listUserVipRanking.FirstOrDefault();
                        if (uvEntity.F_VipType == 0)
                        {
                            coin = 10;
                        }
                        else
                        {
                            coin = 100;
                        }
                    }
                    else
                    {
                        coin = 100;
                    }
                }
                else
                {
                    code = 1;
                    msg = "您不是Vip，不能签到";
                }
                List<UserSignEntity> listUserSign = usApp.GetUserSignList((int)uvEntity.F_UserID, 1);
                if (listUserSign.Count > 0)
                {
                    code = 1;
                    msg = "今日已签到，明日再来";
                }

                if (code == 0)
                {
                    if (uvEntity.F_LockCoin > 0)
                    {
                        using (var tranScope = new TransactionScope())
                        {
                            coin = uvEntity.F_LockCoin > coin ? coin : (int)uvEntity.F_LockCoin;
                            data = coin;
                            uvEntity.F_Coin = uvEntity.F_Coin + coin;
                            uvEntity.F_LockCoin = uvEntity.F_LockCoin - coin;
                            userVipApp.SubmitForm(uvEntity, uvEntity.F_Id);

                            usApp.SubmitForm(new UserSignEntity
                            {
                                UserID = uvEntity.F_UserID,
                                SignData = DateTime.Now.Date,
                                Coin = coin,
                            }, "");

                            wealthLogApp.SubmitForm(new WealthLogEntity
                            {
                                F_UserID = uvEntity.F_Id,
                                F_Coin = coin,
                                F_OldCoin = uvEntity.F_Coin,
                                F_CoinType = 2,
                                F_Type = 1,
                                F_Note = "签到所得"
                            }, "");
                            tranScope.Complete();
                        }
                    }
                    else
                    {
                        code = 1;
                        msg = "该业务已停止";
                    }
                }
            }
            catch (Exception ex)
            {
                code = 1;
                msg = ex.Message;
            }
            finally
            {

            }
            var result = new
            {
                code = code,
                msg = msg,
                data = data
            };
            return Json(result);
        }

        public JsonResult GetUserSignList(string mobile)
        {
            int code = 0;
            string msg = "";
            int data = 0;
            UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
            List<UserSignEntity> listUserSign = usApp.GetUserSignList((int)uvEntity.F_UserID);
            var result = new
            {
                code = code,
                msg = msg,
                data = listUserSign.Select(x => new
                {
                    x.Coin,
                    SignData = ((DateTime)x.SignData).ToString("yyyy-MM-dd"),
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 升级
        public JsonResult GetUserVipUpgradeData(string mobile, int type = 0)
        {
            int code = 0;
            string msg = "";
            int point = 0;
            int userID = 0;
            decimal Integral = 0;
            string NickName = "";
            try
            {
                UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
                List<UserVipRankingEntity> listUserVipRanking = userVipRankingApp.GetUserVipRankingEntityList((int)uvEntity.F_UserID);
                if (listUserVipRanking.Count > 0)
                {
                    Stack<UvrActionModel> stackUvrActionModel = new Stack<UvrActionModel>();
                    UvrModel model = new UvrModel();
                    userVipRankingApp.GetSuperiorList(0, (int)uvEntity.F_UserID, type, ref stackUvrActionModel);

                    List<UvrActionModel> listUvrActionModel = new List<UvrActionModel>();
                    foreach (var item in stackUvrActionModel)
                    {
                        listUvrActionModel.Add(item);
                    }

                    if (uvEntity.F_Type == 0) { }
                    //    switch ((int)uvEntity.F_VipType)
                    //    {
                    //        case 1:
                    //            point = 100;
                    //            model = listUvrActionModel[1].UvrModelList[0];
                    //            break;
                    //        case 2:
                    //            point = 200;
                    //            model = listUvrActionModel[0].UvrModelList[0];
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    switch ((int)uvEntity.F_VipType)
                    //    {
                    //        case 1:
                    //            point = 1000;
                    //            model = listUvrActionModel[1].UvrModelList[0];
                    //            break;
                    //    }
                    //}

                    Integral = (decimal)uvEntity.F_Integral;
                    NickName = model.NickName;
                    userID = model.UserID;
                }
                else
                {
                    code = 1;
                    msg = "您不是Vip，无法升级";
                }
            }
            catch (Exception ex)
            {
                code = 1;
                msg = ex.Message;
            }
            finally
            {

            }
            var data = new
            {
                point = point,//所需积分
                Integral = Integral,//现有积分
                NickName = NickName,//收款方
                UserID = userID
            };


            var result = new
            {
                code = code,
                msg = msg,
                data = data
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SubmitUserVipUpgradeData(string mobile, int point, int userID)
        {

            int code = 0;
            string msg = "";

            UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
            List<UserVipRankingEntity> listUserVipRanking = userVipRankingApp.GetUserVipRankingEntityList((int)uvEntity.F_UserID);
            if (listUserVipRanking.Count > 0)
            {
                using (var tranScope = new TransactionScope())
                {
                    uvEntity.F_Integral = uvEntity.F_Integral - point;
                    userVipApp.SubmitForm(uvEntity, uvEntity.F_Id);
                    wealthLogApp.SubmitForm(new WealthLogEntity()
                    {
                        F_UserID = uvEntity.F_Id,
                        F_Integral = point,
                        F_OldIntegral = uvEntity.F_Integral,
                        F_CoinType = 1,
                        F_Type = 2,
                        F_Note = "升级"
                    }, "");

                    UserVipEntity userVipEntity = userVipApp.GetUserVipEntityByUserID(userID);
                    userVipEntity.F_Integral = userVipEntity.F_Integral + point;
                    userVipApp.SubmitForm(userVipEntity, userVipEntity.F_Id);
                    wealthLogApp.SubmitForm(new WealthLogEntity()
                    {
                        F_UserID = userVipEntity.F_Id,
                        F_Integral = point,
                        F_OldIntegral = userVipEntity.F_Integral,
                        F_CoinType = 1,
                        F_Type = 1,
                        F_Note = "下线升级"
                    }, "");
                    tranScope.Complete();
                }

            }
            else
            {
                code = 1;
                msg = "您不是Vip，无法升级";
            }
            var result = new
            {
                code = code,
                msg = msg,
                data = ""
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 流水记录
        public JsonResult GetIntegralLogList(string mobile, int type = 1)
        {

            int code = 0;
            string msg = "";
            UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
            List<WealthLogEntity> wealthLogList = wealthLogApp.GetWealthLogList(uvEntity.F_Id, type);
            var result = new
            {
                code = code,
                msg = msg,
                data = wealthLogList.Select(x => new
                {
                    x.F_Type,//类型，1  增加，2  减少
                    x.F_Integral,//数量
                    x.F_Note,//备注
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yy-MM-dd"),//日期
                    //展示信息
                    // 类型       数量         备注        日期
                    //类型为增加，字颜色为红色
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 我的订单
        public JsonResult GetOrderViewList(string mobile)
        {
            int code = 0;
            string msg = "";
            UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
            List<OrderEntity> listOrder = orderApp.GetList(uvEntity.F_Id);
            var result = new
            {
                code = code,
                msg = msg,
                data = listOrder.Select(x => new
                {
                    x.F_Id,
                    x.OrderStatus,
                    x.F_OrderNO,
                    x.F_Amount,
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yy-MM-dd")
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrderViewInfo(string orderID)
        {
            int code = 0;
            string msg = "";
            OrderEntity orderEntity = orderApp.GetForm(orderID);
            List<OrderItemEntity> listOrderItem = orderItemApp.GetOrderItemListByOrderID(orderID);
            OrderActionModel orderActionModel = new OrderActionModel()
            {
                orderEntity = orderEntity,
                ItemList = listOrderItem
            };
            var result = new
            {
                code = code,
                msg = msg,
                data = orderActionModel
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitOrderViewInfo(string orderActionModelJson)
        {
            OrderActionModel model = orderActionModelJson.ToObject<OrderActionModel>();
            using (var tranScope = new TransactionScope())
            {
                model.orderEntity.F_Status = 1;
                orderApp.SubmitForm(model.orderEntity, model.orderEntity.F_Id);
                foreach (var item in model.ItemList)
                {
                    item.F_OrderID = model.orderEntity.F_Id;
                    item.F_Status = 0;
                    orderItemApp.SubmitForm(item, item.F_Id);
                }
                tranScope.Complete();
            }

            return Json(new
            {
                code = 0,
                msg = "",
                data = ""
            });
        }
        #endregion

        #region 转出
        /// <summary>
        /// 转出
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="intoMobile"></param>
        /// <param name="coinNum"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubmitInterturnCoin(string mobile, string intoMobile, int coinNum, int type)
        {
            int code = 0;
            string msg = "";
            int data = 0;

            try
            {
                UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
                UserVipEntity intoUvEntity = userVipApp.GetUserVipEntity(intoMobile);
                if (intoUvEntity != null)
                {
                    using (var tranScope = new TransactionScope())
                    {
                        if (type == 3)
                        {
                            uvEntity.F_PrivateCoin = uvEntity.F_PrivateCoin - coinNum;
                            if (uvEntity.F_PrivateCoin >= 0)
                            {
                                userVipApp.SubmitForm(uvEntity, uvEntity.F_Id);
                                intoUvEntity.F_PrivateCoin = intoUvEntity.F_PrivateCoin + coinNum;
                                userVipApp.SubmitForm(intoUvEntity, intoUvEntity.F_Id);
                            }
                            else
                            {
                                code = 1;
                                msg = "数量不足";
                            }
                        }
                        else if (type == 4)
                        {
                            uvEntity.F_Coin = uvEntity.F_Coin - coinNum;
                            if (uvEntity.F_Coin >= 0)
                            {
                                userVipApp.SubmitForm(uvEntity, uvEntity.F_Id);
                                intoUvEntity.F_Coin = intoUvEntity.F_Coin + coinNum;
                                userVipApp.SubmitForm(intoUvEntity, intoUvEntity.F_Id);
                            }
                            else
                            {
                                code = 1;
                                msg = "数量不足";
                            }
                        }
                        if (code == 0)
                        {
                            wealthLogApp.SubmitForm(new WealthLogEntity() { F_UserID = uvEntity.F_Id, F_Coin = coinNum, F_CoinType = type, F_Type = 2, F_Note = intoMobile }, "");
                            wealthLogApp.SubmitForm(new WealthLogEntity() { F_UserID = intoUvEntity.F_Id, F_Coin = coinNum, F_CoinType = type, F_Type = 1, F_Note = mobile }, "");
                        }

                        tranScope.Complete();
                    }
                }
                else
                {
                    code = 1;
                    msg = "接收者不存在";
                }

            }
            catch (Exception ex)
            {
                code = 1;
                msg = ex.Message;
            }

            var result = new
            {
                code = code,
                msg = msg,
                data = ""
            };
            return Json(result);
        }

        public JsonResult GetInterturnCoinList(string mobile, int coinType, int type = 1)
        {
            int code = 0;
            string msg = "";
            UserVipEntity uvEntity = userVipApp.GetUserVipEntity(mobile);
            List<WealthLogEntity> wealthLogList = wealthLogApp.GetWealthLogList(uvEntity.F_Id, coinType, type);
            var result = new
            {
                code = code,
                msg = msg,
                data = wealthLogList.Select(x => new
                {
                    x.F_Coin,//数量
                    x.F_Note,//
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yy-MM-dd"),//日期
                    //展示信息
                    // 类型       数量         备注        日期
                    //类型为增加，字颜色为红色
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 公告资讯
        public JsonResult GetNewsList()
        {
            int code = 0;
            string msg = "";
            List<NewsEntity> newsList = newsApp.GetListByCount(100);//.GetWealthLogList(uvEntity.F_Id, coinType, type);
            var result = new
            {
                code = code,
                msg = msg,
                data = newsList.Select(x => new
                {
                    x.F_Id,//数量
                    x.F_Title,//
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yyyy-MM-dd HH:mm"),
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetNewsInfo(string fid)
        {
            int code = 0;
            string msg = "";
            NewsEntity newsEntity = newsApp.GetForm(fid);//.GetWealthLogList(uvEntity.F_Id, coinType, type);
            var result = new
            {
                code = code,
                msg = msg,
                data = new
                {
                    newsEntity.F_Id,//数量
                    newsEntity.F_Title,//
                    newsEntity.F_Content,
                    F_CreatorTime = ((DateTime)newsEntity.F_CreatorTime).ToString("yyyy-MM-dd HH:mm"),
                }
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public JsonResult GetInvestConfigList()
        {
            int code = 0;
            string msg = "";
            List<InvestConfigEntity> investConfigList = investConfigApp.GetInvestConfigList(8);
            var result = new
            {
                code = code,
                msg = msg,
                data = investConfigList.Select(x => new
                {
                    x.F_Id,//数量
                    x.F_VCoin,//
                    x.F_Money,
                    F_CreatorTime = ((DateTime)x.F_CreatorTime).ToString("yyyy-MM-dd HH:mm"),
                })
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
