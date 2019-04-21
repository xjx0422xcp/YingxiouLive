//-----------------------------------------------------------------------
// <copyright file=" InvestConfig.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: InvestConfig.cs
// * history : Created by T4 04/21/2019 10:59:44 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.InvestConfig
{
    /// <summary>
    /// InvestConfig Entity Model
    /// </summary>
    public class InvestConfigEntity : IEntity<InvestConfigEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public Decimal? F_Money { get; set; }
        public Int32? F_VCoin { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public String F_LastModifyUserId { get; set; }
    }
}