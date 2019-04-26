//-----------------------------------------------------------------------
// <copyright file=" UserInvest.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserInvest.cs
// * history : Created by T4 04/24/2019 19:21:22 
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.UserInvest
{
    /// <summary>
    /// UserInvest Entity Model
    /// </summary>
    public class UserInvestEntity : IEntity<UserInvestEntity>, ICreationAudited,  IModificationAudited
    {
						public  String  F_Id { get; set; }
					public  String  F_OrderNo { get; set; }
					public  Int32?  F_UserID { get; set; }
					public  Decimal?  F_Money { get; set; }
					public  Decimal?  F_VCoin { get; set; }
					public  Int32?  F_Status { get; set; }
					public  String  F_CreatorUserId { get; set; }
					public  DateTime?  F_CreatorTime { get; set; }
					public  String  F_LastModifyUserId { get; set; }
					public  DateTime?  F_LastModifyTime { get; set; }
		    }
}