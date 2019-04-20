//-----------------------------------------------------------------------
// <copyright file=" UserVip.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: UserVip.cs
// * history : Created by T4 01/29/2019 15:51:01 
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
    /// UserVip Entity Model
    /// </summary>
    public class UserVipEntity : IEntity<UserVipEntity>, ICreationAudited, IModificationAudited
    {
        public String F_Id { get; set; }
        public String F_UserName { get; set; }
        public String F_UserPass { get; set; }
        public String F_Account { get; set; }
        public String F_Mobile { get; set; }
        public String F_NickName { get; set; }
        public Int32? F_ParentID { get; set; }
        public Int16? F_Position { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32? F_UserID { get; set; }
        public Decimal? F_Coin { get; set; }
        public Decimal? F_LockCoin { get; set; }
        public Decimal? F_Integral { get; set; }
        public Decimal? F_Integral1 { get; set; }
        public DateTime? F_CreateTime { get; set; }
        public Boolean? F_Enable { get; set; }
        public DateTime? F_ActiveTime { get; set; }

        public string F_CreatorUserId { get; set; }

        public DateTime? F_CreatorTime { get; set; }

        public string F_PurseAddress { get; set; }

        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

        public String F_TureName { get; set; }
        public String F_CardNo { get; set; }
        public String F_BankName { get; set; }
        public String F_Apliay { get; set; }
        public String F_WeiXin { get; set; }
        public Int32? F_Status { get; set; }
        public Int32? F_Type { get; set; }
        public Int32? F_VipType { get; set; }
        public String F_HeadImg { get; set; }
        public Decimal? F_PrivateCoin { get; set; }

        public Decimal? F_TodayRelaseCoin { get; set; }//今日释放

        public Decimal? F_TodayExpediteCoin { get; set; }//今日加速


    }
}