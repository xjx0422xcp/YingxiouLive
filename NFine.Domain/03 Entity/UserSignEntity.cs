//-----------------------------------------------------------------------
// <copyright file=" UserSign.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserSign.cs
// * history : Created by T4 03/13/2019 01:19:04 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.UserSign
{
    /// <summary>
    /// UserSign Entity Model
    /// </summary>
    public class UserSignEntity : IEntity<UserSignEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public Int32? UserID { get; set; }
        public DateTime? SignData { get; set; }
        public Decimal? Coin { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }
}