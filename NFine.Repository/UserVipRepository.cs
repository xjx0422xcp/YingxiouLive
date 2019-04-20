//-----------------------------------------------------------------------
// <copyright file=" UserVip.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserVip.cs
// * history : Created by T4 01/29/2019 15:51:03 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.IRepository.UserVip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.UserVip
{
    public class UserVipRepository : RepositoryBase<UserVipEntity>, IUserVipRepository
    {
    }
}