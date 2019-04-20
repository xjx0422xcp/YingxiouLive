//-----------------------------------------------------------------------
// <copyright file=" UserVip.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserVip.cs
// * history : Created by T4 01/29/2019 15:51:01 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Domain.Entity;
using NFine.Domain.IRepository.UserVip;
using NFine.Repository.UserVip;
using System;
using NFine.Code;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace NFine.Application.UserVip
{
    public class UserVipApp
    {
        private IUserVipRepository service = new UserVipRepository();

        public List<UserVipEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<UserVipEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_UserName.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public List<UserVipEntity> GetListChildren(int parentID)
        {
            var expression = ExtLinq.True<UserVipEntity>();
            expression = expression.And(t=>t.F_ParentID==parentID);
            return service.IQueryable(expression).OrderByDescending(x=>x.F_UserID).ToList();
        }


        public UserVipEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public UserVipEntity GetUserVipEntity(string mobile)
        {
            return service.FindEntity(x => x.F_UserName == mobile);
        }
        public void Delete(UserVipEntity entity)
        {
            service.Delete(entity);
        }

        public UserVipEntity GetUserVipEntityOrderID(string orderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select v.* from Sys_Order o inner join Sys_UserVip v on o.F_UserID =v.F_Id where o.F_Id=@orderID");
            DbParameter[] parameter =
            {
                 new SqlParameter("@orderID",orderID)
            };
            return service.FindList(strSql.ToString(), parameter).FirstOrDefault();
        }

        public bool IsExistsSysUser(string mobile)
        {
            UserVipEntity userVipEntity = service.FindEntity(x => x.F_UserName== mobile);
            return userVipEntity == null ? true : false;
        }

        public UserVipEntity GetUserVipEntityByUserID(int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select v.* from Sys_UserVip v where v.F_UserID=@userID");
            DbParameter[] parameter =
            {
                 new SqlParameter("@userID",userID)
            };
            return service.FindList(strSql.ToString(), parameter).FirstOrDefault();
        }


        public List<UserVipEntity> GetUserVipList()
        {
            var expression = ExtLinq.True<UserVipEntity>();
            return service.IQueryable(expression).ToList();
        }


        public UserVipEntity CheckLogin(string username, string password)
        {
            UserVipEntity userEntity = service.FindEntity(t => t.F_UserName == username);
            if (userEntity != null)
            {
                string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower()).ToLower(), 32).ToLower();
                if (dbPassword != userEntity.F_UserPass)
                {
                   userEntity=null;
                }
            }
            return userEntity;
        }


        public void SubmitForm(UserVipEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }
    }
}