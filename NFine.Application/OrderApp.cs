//-----------------------------------------------------------------------
// <copyright file=" Order.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Order.cs
// * history : Created by T4 01/29/2019 15:52:34 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using NFine.Domain.IRepository.Order;
using NFine.Repository.Order;
using System;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.Order
{
    public class OrderApp
    {
		private IOrderRepository service = new OrderRepository();

		public List<OrderEntity> GetList(Pagination pagination, string queryJson)
        {
		    var expression = ExtLinq.True<OrderEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }


        public List<OrderEntity> GetList(string userID)
        {
            var expression = ExtLinq.True<OrderEntity>();
            if (!string.IsNullOrEmpty(userID))
            {
                expression = expression.And(x => x.F_UserID == userID);
            }
            return service.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime).ToList();
        }

        public OrderEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OrderEntity entity)
        {
            service.Delete(entity);
        }

		public void SubmitForm(OrderEntity entity, string keyValue)
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