//-----------------------------------------------------------------------
// <copyright file=" Withdraw.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: Withdraw.cs
// * history : Created by T4 01/29/2019 15:51:59 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity
{
    /// <summary>
    /// Withdraw Entity Model
    /// </summary>
    public class WithdrawEntity : IEntity<WithdrawEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_UserID { get; set; }
        public Decimal? F_Amount { get; set; }
        public Decimal? F_Surplus { get; set; }
        public Int16? F_Status { get; set; }
        public Int16? F_Enable { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }
}