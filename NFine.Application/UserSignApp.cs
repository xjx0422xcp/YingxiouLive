//-----------------------------------------------------------------------
// <copyright file=" UserSign.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserSign.cs
// * history : Created by T4 03/13/2019 01:19:04 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Code;
using NFine.Domain.Entity.UserSign;
using NFine.Domain.IRepository.UserSign;
using NFine.Repository.UserSign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;

namespace NFine.Application.UserSign
{
    public class UserSignApp
    {
        private IUserSignRepository service = new UserSignRepository();

        public List<UserSignEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<UserSignEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public UserSignEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(UserSignEntity entity)
        {
            service.Delete(entity);
        }


        public List<UserSignEntity> GetUserSignList(int userID, int type = 0)
        {
            var expression = ExtLinq.True<UserSignEntity>();
            expression = expression.And(x => x.UserID == userID);
            if (type > 0)
            {
                string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                DateTime dt = Convert.ToDateTime(date);
                expression = expression.And(x => x.SignData >=dt);
            }
            return service.IQueryable(expression).ToList();
        }

        public void SubmitForm(UserSignEntity entity, string keyValue)
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