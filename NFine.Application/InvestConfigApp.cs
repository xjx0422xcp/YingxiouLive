//-----------------------------------------------------------------------
// <copyright file=" InvestConfig.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: InvestConfig.cs
// * history : Created by T4 04/21/2019 10:59:44 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Code;
using NFine.Domain.Entity.InvestConfig;
using NFine.Domain.IRepository.InvestConfig;
using NFine.Repository.InvestConfig;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.InvestConfig
{
    public class InvestConfigApp
    {
		private IInvestConfigRepository service = new InvestConfigRepository();

		public List<InvestConfigEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<InvestConfigEntity>();
            var queryParam = queryJson.ToJObject();
            //if (!queryParam["keyword"].IsEmpty())
            //{
            //    string keyword = queryParam["keyword"].ToString();
            //    expression = expression.And(t => t.Title.Contains(keyword));
            //}
            return service.FindList(expression, pagination);
        }

	    public InvestConfigEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(InvestConfigEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(InvestConfigEntity entity, string keyValue)
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