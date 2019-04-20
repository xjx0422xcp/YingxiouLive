//-----------------------------------------------------------------------
// <copyright file=" Order.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Order.cs
// * history : Created by T4 01/29/2019 15:52:35 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    /// <summary>
    /// Order Entity Model
    /// </summary>
    public class OrderEntity : IEntity<OrderEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_OrderNO { get; set; }
        public String F_UserID { get; set; }
        public Decimal? F_Amount { get; set; }
        public Int16? F_Status { get; set; }
        public String F_Title { get; set; }

        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

        [NotMapped]
        public string OrderStatus
        {
            get
            {
                string result = "";
                switch (F_Status)
                {
                    case 0:
                        result = "已提交";
                        break;
                    case 1:
                        result = "未审核";
                        break;
                    case 2:
                        result = "已退回";
                        break;
                    case 3:
                        result = "已完成";
                        break;
                    default:
                        result = "已提交";
                        break;
                }
                return result;
            }
        }
    }
}