//-----------------------------------------------------------------------
// <copyright file=" SysUserVipRanking.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: SysUserVipRanking.cs
// * history : Created by T4 03/02/2019 22:08:30 
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
    /// SysUserVipRanking Entity Model
    /// </summary>
    public class UserVipRankingEntity : IEntity<UserVipRankingEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public Int32? F_UserID { get; set; }//
        public Int32? F_LeftUserID { get; set; }//左
        public Int32? F_RightUserID { get; set; }//右
        public Int32? F_ParentID { get; set; }//推荐人
        public Int32? F_SuperiorID { get; set; }//上级
        public String F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public String F_LastModifyUserId { get; set; }

        public Decimal? F_Integral { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        /// <summary>
        /// 0：未达标
        /// 1：已达标
        /// </summary>
        public Int32? F_Status { get; set; }
        /// <summary>
        /// 0:58
        /// 1:580
        /// </summary>
        public Int32? F_Type { get; set; }//0,1,2

        [NotMapped]
        public string UserGarden
        {
            get
            {
                string result = "";
                switch ((int)F_Type)
                {
                    case 0:
                        result = "白银会员";
                        break;
                    case 1:
                        result = "白金会员";
                        break;
                    default:
                        result = "白银会员";
                        break;
                }
                return result;
            }
        }
    }
}