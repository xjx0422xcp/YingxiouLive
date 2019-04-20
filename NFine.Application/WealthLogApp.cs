//-----------------------------------------------------------------------
// <copyright file=" WealthLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: WealthLog.cs
// * history : Created by T4 01/29/2019 15:51:36 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using NFine.Domain.IRepository.WealthLog;
using NFine.Repository.WealthLog;
using System;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.WealthLog
{
    public class WealthLogApp
    {
        private IWealthLogRepository service = new WealthLogRepository();

        public List<WealthLogEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<WealthLogEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public WealthLogEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public List<WealthLogEntity> GetWealthLogList(string userID, int coinType)
        {
            var expression = ExtLinq.True<WealthLogEntity>();
            expression = expression.And(x => x.F_UserID == userID);
            expression = expression.And(x => x.F_CoinType == coinType);

            return service.IQueryable(expression).OrderByDescending(x=>x.F_CreatorTime).ToList();
        }



        public List<WealthLogEntity> GetWealthLogList(string userID, int coinType, int type)
        {
            var expression = ExtLinq.True<WealthLogEntity>();
            expression = expression.And(x => x.F_UserID == userID);
            expression = expression.And(x => x.F_CoinType == coinType);
            expression = expression.And(x => x.F_Type == type);
            return service.IQueryable(expression).ToList();
        }
        public void Delete(WealthLogEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(WealthLogEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }
    }
}