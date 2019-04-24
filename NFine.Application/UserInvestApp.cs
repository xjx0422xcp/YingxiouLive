//-----------------------------------------------------------------------
// <copyright file=" UserInvest.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserInvest.cs
// * history : Created by T4 04/24/2019 19:21:22 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Code;
using NFine.Domain.Entity.UserInvest;
using NFine.Domain.IRepository.UserInvest;
using NFine.Repository.UserInvest;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.UserInvest
{
    public class UserInvestApp
    {
		private IUserInvestRepository service = new UserInvestRepository();

		public List<UserInvestEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<UserInvestEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_OrderNo.Contains(keyword));//F_OrderNo ∂©µ•±‡∫≈≤È’“
            }
            return service.FindList(expression, pagination);
        }

	    public UserInvestEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(UserInvestEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(UserInvestEntity entity, string keyValue)
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