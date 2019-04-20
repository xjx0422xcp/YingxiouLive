//-----------------------------------------------------------------------
// <copyright file=" WealthLog.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: WealthLog.cs
// * history : Created by T4 01/29/2019 15:51:36 
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
    /// WealthLog Entity Model
    /// </summary>
    public class WealthLogEntity : IEntity<WealthLogEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }

       /// <summary>
       /// 1 增加，2 减少
       /// </summary>
        public Int16? F_Type { get; set; }
        public Decimal? F_Coin { get; set; }
        public Decimal? F_Integral { get; set; }
        public Decimal? F_OldCoin { get; set; }
        public Decimal? F_OldIntegral { get; set; }
        public String F_UserID { get; set; }

        /// <summary>
        /// 1积分 2空气币
        /// </summary>
        public Int32? F_CoinType { get; set; }
        public DateTime? F_CreateTime { get; set; }


        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

        public String F_Note{ get; set; }
    }
}