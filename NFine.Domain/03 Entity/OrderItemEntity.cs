//-----------------------------------------------------------------------
// <copyright file=" OrderItem.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: OrderItem.cs
// * history : Created by T4 01/29/2019 15:53:01 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    /// <summary>
    /// OrderItem Entity Model
    /// </summary>
    public class OrderItemEntity : IEntity<OrderItemEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_OrderID { get; set; }
        public Decimal? F_Amount { get; set; }
        public String F_Certificate { get; set; }
        public String F_TureName { get; set; }
        public String F_CardNo { get; set; }
        public String F_BankName { get; set; }
        public String F_Apliay { get; set; }
        public String F_WeiXin { get; set; }
        public String F_WithdrawId { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

        public Int32? F_Status { get; set; }


        [NotMapped]
        public string OrderStatus
        {
            get
            {
                string result = "";
                switch (F_Status)
                {
                    case 0: result = "Î´ÉóºË"; break;
                    case 1: result = "ÒÑ¾Ü¾ø"; break;
                    case 2: result = "ÒÑÍê³É"; break;
                    default: result = "Î´ÉóºË"; break;
                }
                return result;
            }
        }


    }
}