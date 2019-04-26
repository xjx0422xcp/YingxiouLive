//-----------------------------------------------------------------------
// <copyright file=" UserFans.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserFans.cs
// * history : Created by T4 04/24/2019 18:36:10 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.UserFans
{
    /// <summary>
    /// UserFans Entity Model
    /// </summary>
    public class UserFansEntity : IEntity<UserFansEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public Int32? F_UserID { get; set; }
        public Int32? F_ReferID { get; set; }
        public Int32? F_Status { get; set; }
        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }
}