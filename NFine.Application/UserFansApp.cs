//-----------------------------------------------------------------------
// <copyright file=" UserFans.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserFans.cs
// * history : Created by T4 04/24/2019 18:36:09 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Code;
using NFine.Domain.Entity.UserFans;
using NFine.Domain.IRepository.UserFans;
using NFine.Repository.UserFans;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.UserFans
{
    public class UserFansApp
    {
		private IUserFansRepository service = new UserFansRepository();

		public List<UserFansEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<UserFansEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
               // expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

	    public UserFansEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(UserFansEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(UserFansEntity entity, string keyValue)
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