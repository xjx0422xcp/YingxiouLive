//-----------------------------------------------------------------------
// <copyright file=" Withdraw.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Withdraw.cs
// * history : Created by T4 01/29/2019 15:51:58 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using NFine.Domain.IRepository.Withdraw;
using NFine.Repository.Withdraw;
using System;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.Withdraw
{
    public class WithdrawApp
    {
        private IWithdrawRepository service = new WithdrawRepository();

        public List<WithdrawEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<WithdrawEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public List<WithdrawEntity> GetList(string userID)
        {
            var expression = ExtLinq.True<WithdrawEntity>();
            if (!string.IsNullOrEmpty(userID))
            {
                expression = expression.And(x => x.F_UserID == userID);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }


        public List<WithdrawEntity> GetWithdrawList(string userID)
        {
            var expression = ExtLinq.True<WithdrawEntity>();
            expression = expression.And(x => x.F_Surplus > 0);
            expression = expression.And(x => x.F_Status == 0);
            if (!string.IsNullOrEmpty(userID))
            {
                expression = expression.And(x => x.F_UserID != userID);
            }
            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();
        }

        public WithdrawEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(WithdrawEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(WithdrawEntity entity, string keyValue)
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