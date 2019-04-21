//-----------------------------------------------------------------------
// <copyright file=" Gift.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Gift.cs
// * history : Created by T4 04/21/2019 10:43:28 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Code;
using NFine.Domain.Entity.Gift;
using NFine.Domain.IRepository.Gift;
using NFine.Repository.Gift;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.Gift
{
    public class GiftApp
    {
		private IGiftRepository service = new GiftRepository();

		public List<GiftEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<GiftEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

	    public GiftEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(GiftEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(GiftEntity entity, string keyValue)
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