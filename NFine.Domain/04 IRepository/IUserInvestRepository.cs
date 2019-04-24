//-----------------------------------------------------------------------
// <copyright file=" UserInvest.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserInvest.cs
// * history : Created by T4 04/24/2019 19:21:23 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.UserInvest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.IRepository.UserInvest
{
    public interface IUserInvestRepository : IRepositoryBase<UserInvestEntity>
    {
    }
}