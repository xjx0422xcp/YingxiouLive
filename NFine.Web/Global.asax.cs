using NFine.Code;
using NFine.Data;
using System.Web.Mvc;
using System;
using System.Web.Routing;
using NFine.Application.Order;
using NFine.Application.OrderItem;
using NFine.Application.UserSign;
using NFine.Application.UserVip;
using NFine.Application.UserVipRanking;
using NFine.Application.WealthLog;
using NFine.Application.Withdraw;
using NFine.Domain.Entity;
using NFine.Domain.Entity.UserSign;
using NFine.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Threading;
using System.IO;
using log4net.Config;

namespace NFine.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private UserVipApp userVipApp = new UserVipApp();
       
        private log4net.ILog log = log4net.LogManager.GetLogger("Global.asax");

        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            PreApplicationStartMethod();


            Thread LoadServiceData = new Thread(new ThreadStart(LoadFromWebservice));
            LoadServiceData.Start();
        }

        void Application_End(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            string RequestURL = "http://103.205.6.57:8090";
            //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
            System.Net.HttpWebRequest __HttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(RequestURL);
            System.Net.HttpWebResponse __HttpWebResponse = (System.Net.HttpWebResponse)__HttpWebRequest.GetResponse();
            System.IO.Stream __rStream = __HttpWebResponse.GetResponseStream();//得到回写的字节流  
                                                                               //当不再需要计时器时，请使用 Dispose 方法释放计时器持有的资源。
            __rStream.Close();
            __rStream.Dispose();
        }

        private void LoadFromWebservice()
        {
            //定义一个定时器，并开启和配置相关属性
            System.Timers.Timer Wtimer = new System.Timers.Timer(1000);//执行任务的周期
            Wtimer.Elapsed += new System.Timers.ElapsedEventHandler(Wtimer_Elapsed);
            Wtimer.Enabled = true;
            Wtimer.AutoReset = true;
        }


        void Wtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int intHour = e.SignalTime.Hour, intMinute = e.SignalTime.Minute, intSecond = e.SignalTime.Second;
            int iHour = 0, iMinute = 00, iSecond = 00;
            if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
            {
                log.Info("定时任务开始 StartTime:" + DateTime.Now);   
                List<UserVipEntity> listUserVip = userVipApp.GetUserVipList();
                listUserVip = listUserVip.Where(x => x.F_PrivateCoin > 0).ToList();
                foreach (var item in listUserVip)
                {
                    int todayExpediteCoin = 100 + (int)item.F_TodayExpediteCoin;
                    if (item.F_PrivateCoin >= todayExpediteCoin)
                    {
                        item.F_PrivateCoin = item.F_PrivateCoin - todayExpediteCoin;
                        item.F_TodayRelaseCoin = todayExpediteCoin;
                        item.F_TodayExpediteCoin = 0;
                        item.F_Coin = item.F_Coin + todayExpediteCoin;
                    }
                    else
                    {
                        item.F_TodayRelaseCoin = item.F_PrivateCoin;
                        item.F_TodayExpediteCoin = 0;
                        item.F_Coin = item.F_Coin + item.F_PrivateCoin;
                        item.F_PrivateCoin = 0;
                    }
                    item.F_PrivateCoin = item.F_PrivateCoin - item.F_TodayRelaseCoin;
                    userVipApp.SubmitForm(item, item.F_Id);
                }

                log.Info("定时任务结束 EndTime:" + DateTime.Now);
            }
        }

        private void PreApplicationStartMethod()
        {
            using (var dbcontext = new NFineDbContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
    }

}