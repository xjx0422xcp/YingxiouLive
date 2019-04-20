//-----------------------------------------------------------------------
// <copyright file=" OrderItem.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: OrderItem.cs
// * history : Created by T4 01/29/2019 15:53:00 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using NFine.Domain.IRepository.OrderItem;
using NFine.Repository.OrderItem;
using System;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace NFine.Application.OrderItem
{
    public class OrderItemApp
    {
        private IOrderItemRepository service = new OrderItemRepository();

        public List<OrderItemEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<OrderItemEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                //expression = expression.And(t => t.Title.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public OrderItemEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(OrderItemEntity entity)
        {
            service.Delete(entity);
        }

        public int GetOrderItemCount(string orderID, int status)
        {
            var expression = ExtLinq.True<OrderItemEntity>();
            expression = expression.And(x => x.F_OrderID == orderID);
            expression = expression.And(x => x.F_Status != status);
            return service.IQueryable(expression).Count();
        }

        public List<OrderItemEntity> GetOrderItemListByOrderID(string orderID)
        {
            var expression = ExtLinq.True<OrderItemEntity>();
            expression = expression.And(x => x.F_OrderID == orderID);
            return service.IQueryable(expression).ToList();
        }


        public List<OrderItemEntity> GetOrderItemList(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select item.* from Sys_OrderItem item inner join Sys_Withdraw draw on item.F_WithdrawId=draw.F_Id inner join Sys_UserVip vip on draw.F_UserID =vip.F_Id where vip.F_Account=@mobile order by item.F_CreatorTime desc");
            DbParameter[] parameter =
            {
                 new SqlParameter("@mobile",mobile)
            };
            return service.FindList(strSql.ToString(), parameter);
        }

        public void SubmitForm(OrderItemEntity entity, string keyValue)
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