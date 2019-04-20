//-----------------------------------------------------------------------
// <copyright file=" SysUserVipRanking.cs" company="NFine">
// * Copyright (C) NFine.Framework  All Rights Reserved
// * version : 1.0
// * author  : NFine.Framework
// * FileName: SysUserVipRanking.cs
// * history : Created by T4 03/02/2019 22:08:30 
// </copyright>
//-----------------------------------------------------------------------
using NFine.Application.UserVip;
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.IRepository.UserVipRanking;
using NFine.Domain.ViewModel;
using NFine.Repository.UserVipRanking;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NFine.Application.UserVipRanking
{
    public class UserVipRankingApp
    {
        private IUserVipRankingRepository service = new UserVipRankingRepository();
        private UserVipApp uvApp = new UserVipApp();
        public List<UserVipRankingEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<UserVipRankingEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Id.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }

        public UserVipRankingEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void Delete(UserVipRankingEntity entity)
        {
            service.Delete(entity);
        }


        public UserVipRankingEntity GetUserVipRankingEntity(int userID, int type)
        {
            var expression = ExtLinq.True<UserVipRankingEntity>();
            expression = expression.And(x => x.F_UserID == userID);
            expression = expression.And(x => x.F_Status == 0);
            expression = expression.And(x => x.F_Type == type);
            return service.IQueryable(expression).FirstOrDefault();
        }



        public List<UserVipRankingEntity> GetUserVipRankingEntityList(int userID)
        {
            var expression = ExtLinq.True<UserVipRankingEntity>();
            expression = expression.And(x => x.F_UserID == userID);
            return service.IQueryable(expression).ToList();
        }


        public UserVipRankingEntity GetUvrEntity(int userID, int type)
        {
            var expression = ExtLinq.True<UserVipRankingEntity>();
            expression = expression.And(x => x.F_UserID == userID);
            expression = expression.And(x => x.F_Type == type);
            return service.IQueryable(expression).FirstOrDefault();
        }

        public List<UserVipRankingEntity> GetUserVipRankingList(int type)
        {
            var expression = ExtLinq.True<UserVipRankingEntity>();
            expression = expression.And(x => x.F_Type == type);
            return service.IQueryable(expression).ToList();
        }

        public List<UserVipRankingEntity> GetUserVipRankingList()
        {
            var expression = ExtLinq.True<UserVipRankingEntity>();
            return service.IQueryable(expression).ToList();
        }

        public void GetSuperiorList(int index, int userID, int type, ref Stack<UvrActionModel> listUvrActionModel)
        {
            if (index <= 3)
            {
                index = index + 1;
                UserVipRankingEntity uvrEntity = GetUvrEntity(userID, type);
                UserVipEntity uvEntity = uvApp.GetUserVipEntityByUserID((int)uvrEntity.F_UserID);
                UserVipEntity uvParentEntity = uvApp.GetUserVipEntityByUserID((int)uvEntity.F_ParentID);
                List<UvrModel> uvrModelList = new List<UvrModel>();

                UserVipEntity uvSuperiorEntity = null;
                UserVipRankingEntity superUserVipRankingEntity = GetUvrEntity((int)uvrEntity.F_SuperiorID, type);
                if (superUserVipRankingEntity != null)
                {
                    uvSuperiorEntity = uvApp.GetUserVipEntityByUserID((int)superUserVipRankingEntity.F_UserID);
                }

                int days = 0;
                string note = "已达标";
                if (uvrEntity.F_Status == 0)
                {
                    days = (((DateTime)uvrEntity.F_CreatorTime).AddDays(7) - DateTime.Now).Days;
                    note = string.Format("未达标,还剩{0}天", days);
                }

                uvrModelList.Add(new UvrModel()
                {
                    ExpiryDays = days,
                    NickName = uvEntity == null ? "" : uvEntity.F_NickName,
                    ParentName = uvParentEntity == null ? "" : uvParentEntity.F_NickName,
                    UserName = uvEntity == null ? "" : uvEntity.F_UserName,
                    Note = note,
                    UserID = (int)uvrEntity.F_UserID,
                    SuperiorID = "a" + (int)uvrEntity.F_SuperiorID,
                    SuperiorName = uvSuperiorEntity == null ? "" : uvSuperiorEntity.F_NickName,
                    point = uvEntity == null ? 0 : (type == 0 ? (int)uvEntity.F_Integral : (int)uvEntity.F_Integral1)
                });
                listUvrActionModel.Push(new UvrActionModel()
                {
                    UvrModelList = uvrModelList
                });
                if (superUserVipRankingEntity != null)
                {
                    GetSuperiorList(index, (int)uvrEntity.F_SuperiorID, type, ref listUvrActionModel);
                }
            }
        }

        public void GetChildrenList(int index, string superiorIDs, int type, ref List<UvrActionModel> listUvrActionModel)
        {
            int tmpNum = type == 1 ? 2 : 4;
            if (index <= tmpNum)
            {
                int layer = listUvrActionModel.Count + 1;
                List<UserVipRankingEntity> allList = GetUserVipRankingList(type);
                int[] parentList = Array.ConvertAll<string, int>(superiorIDs.Split(','), s => int.Parse(s));
                List<int> superiorList = new List<int>();//存放上级
                List<UvrModel> uvrModelList = new List<UvrModel>();
                foreach (var userID in parentList)
                {
                    UserVipRankingEntity uvrSuperiorUserVipRankingEntity = allList.Where(x => x.F_UserID == userID).FirstOrDefault();
                    List<UserVipRankingEntity> tmpList = allList.Where(x => x.F_SuperiorID == userID).OrderBy(x => x.F_CreatorTime).ToList();
                    UserVipRankingEntity leftUserVipRankingEntity = tmpList.Where(x => x.F_UserID == uvrSuperiorUserVipRankingEntity.F_LeftUserID).FirstOrDefault();
                    UserVipRankingEntity rightUserVipRankingEntity = tmpList.Where(x => x.F_UserID == uvrSuperiorUserVipRankingEntity.F_RightUserID).FirstOrDefault();
                    if (uvrSuperiorUserVipRankingEntity.F_LeftUserID > 0 && leftUserVipRankingEntity != null)
                    {
                        superiorList.Add((int)uvrSuperiorUserVipRankingEntity.F_LeftUserID);
                    }
                    if (uvrSuperiorUserVipRankingEntity.F_RightUserID > 0 && rightUserVipRankingEntity != null)
                    {
                        superiorList.Add((int)uvrSuperiorUserVipRankingEntity.F_RightUserID);
                    }
                    #region 左
                    if (leftUserVipRankingEntity != null)
                    {
                        UserVipRankingEntity uvrEntity = GetUvrEntity((int)leftUserVipRankingEntity.F_UserID, type);
                        UserVipEntity uvEntity = uvApp.GetUserVipEntityByUserID((int)uvrEntity.F_UserID);
                        UserVipEntity uvParentEntity = uvApp.GetUserVipEntityByUserID((int)uvEntity.F_ParentID);
                        UserVipEntity uvSuperiorEntity = uvApp.GetUserVipEntityByUserID((int)uvrEntity.F_SuperiorID);


                        int days = 0;
                        string note = "已达标";
                        if (uvrEntity.F_Status == 0)
                        {
                            days = (((DateTime)uvrEntity.F_CreatorTime).AddDays(7) - DateTime.Now).Days;
                            note = string.Format("未达标,还剩{0}天", days);
                        }

                        uvrModelList.Add(new UvrModel()
                        {
                            ExpiryDays = days,
                            NickName = uvEntity == null ? "" : uvEntity.F_NickName,
                            ParentName = uvParentEntity == null ? "" : uvParentEntity.F_NickName,
                            UserName = uvEntity == null ? "" : uvEntity.F_UserName,
                            Note = note,
                            UserID = (int)uvrEntity.F_UserID,
                            SuperiorID = "a" + (int)uvrEntity.F_SuperiorID,
                            SuperiorName = uvSuperiorEntity == null ? "" : uvSuperiorEntity.F_NickName,
                            layer = layer,
                            point = uvEntity == null ? 0 : (type == 0 ? (int)uvEntity.F_Integral : (int)uvEntity.F_Integral1)
                        });
                    }
                    else
                    {
                        uvrModelList.Add(new UvrModel() { SuperiorID = null });
                    }
                    #endregion

                    #region 右
                    if (rightUserVipRankingEntity != null)
                    {
                        UserVipRankingEntity uvrRithtEntity = GetUvrEntity((int)rightUserVipRankingEntity.F_UserID, type);
                        UserVipEntity uvRightEntity = uvApp.GetUserVipEntityByUserID((int)uvrRithtEntity.F_UserID);
                        UserVipEntity uvRightParentEntity = uvApp.GetUserVipEntityByUserID((int)uvRightEntity.F_ParentID);
                        UserVipEntity uvRightSuperiorEntity = uvApp.GetUserVipEntityByUserID((int)rightUserVipRankingEntity.F_SuperiorID);

                        int days = 0;
                        string note = "已达标";
                        if (uvrRithtEntity.F_Status == 0)
                        {
                            days = (((DateTime)uvrRithtEntity.F_CreatorTime).AddDays(7) - DateTime.Now).Days;
                            note = string.Format("未达标,还剩{0}天", days);
                        }
                        uvrModelList.Add(new UvrModel()
                        {
                            ExpiryDays = days,
                            NickName = uvRightEntity == null ? "" : uvRightEntity.F_NickName,
                            ParentName = uvRightParentEntity == null ? "" : uvRightParentEntity.F_NickName,
                            UserName = uvRightEntity == null ? "" : uvRightEntity.F_UserName,
                            Note = note,
                            UserID = (int)uvrRithtEntity.F_UserID,
                            SuperiorID = "a" + (int)uvrRithtEntity.F_SuperiorID,
                            SuperiorName = uvRightSuperiorEntity == null ? "" : uvRightSuperiorEntity.F_NickName,
                            layer = layer,
                            point = uvRightEntity == null ? 0 : (type == 0 ? (int)uvRightEntity.F_Integral : (int)uvRightEntity.F_Integral1)
                        });
                    }
                    else
                    {
                        uvrModelList.Add(new UvrModel() { SuperiorID = null });
                    }
                    #endregion

                }
                int num = (int)Math.Pow(2, index + 1);
                int tmp = num - uvrModelList.Count;
                for (int i = 0; i < tmp; i++)
                {
                    uvrModelList.Add(new UvrModel() { SuperiorID = null });
                }
                listUvrActionModel.Add(new UvrActionModel()
                {
                    UvrModelList = uvrModelList
                });
                if (superiorList.Count > 0)
                {
                    index = index + 1;
                    GetChildrenList(index, string.Join(",", superiorList.Select(x => x)), type, ref listUvrActionModel);
                }
            }
        }


        public void GetSuperiorRanking(string superiorIDs, int type, ref UserVipRankingEntity userVipRankingEntity)
        {
            List<UserVipRankingEntity> allList = GetUserVipRankingList(type);
            int[] parentList = Array.ConvertAll<string, int>(superiorIDs.Split(','), s => int.Parse(s));//将整型字符串转为整型数组
            List<int> resultList = new List<int>();

            foreach (var item in parentList)
            {
                UserVipRankingEntity tmpUserVipRankingEntity= GetUvrEntity(item, type);
                if (tmpUserVipRankingEntity.F_LeftUserID > 0)
                {
                    resultList.Add((int)tmpUserVipRankingEntity.F_LeftUserID);//7
                }

                if (tmpUserVipRankingEntity.F_RightUserID > 0)
                {
                    resultList.Add((int)tmpUserVipRankingEntity.F_RightUserID);//8
                }
            }
            int[] tempNumList;
            if (resultList.Count == 0)
            {
                tempNumList = parentList;
            }
            else
            {
                tempNumList = resultList.ToArray();
            }

            List<UserVipRankingEntity> tmpList = allList.Where(x => tempNumList.Contains((int)x.F_SuperiorID)).OrderBy(x => x.F_CreatorTime).ToList();//取7、8下面的会员，如11。12，13，14
            int code = 0;
            foreach (var item in tmpList)
            {
                if (item.F_Status == 0)
                {
                    userVipRankingEntity = item;
                    code = 1;
                    break;
                }
            }
            if (code == 0)
            {
                string str = string.Join(",", tmpList.Select(x => x.F_UserID));
                if (!string.IsNullOrEmpty(str))
                {
                    GetSuperiorRanking(string.Join(",", tmpList.Select(x => x.F_UserID)), type, ref userVipRankingEntity);
                }
            }
        }

        public void SubmitForm(UserVipRankingEntity entity, string keyValue)
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