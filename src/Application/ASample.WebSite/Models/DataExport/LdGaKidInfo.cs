using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASample.WebSite.Models.DataExport
{
    public class LdGaKidInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string KidName { get; set; }

        /// <summary>
        /// 公民身份号码
        /// </summary>
        public string KidNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 派出所
        /// </summary>
        public string PoliceStation { get; set; }

        /// <summary>
        /// 乡镇（街道）
        /// </summary>
        public string TownOrStreet { get; set; }

        /// <summary>
        /// 居委会
        /// </summary>
        public string Committee { get; set; }

        /// <summary>
        /// 其他详址
        /// </summary>
        public string OtherAddress { get; set; }

        /// <summary>
        /// 街巷路
        /// </summary>
        public string Road { get; set; }

        /// <summary>
        /// 门楼牌号
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// 门楼详址
        /// </summary>
        public string HouseAddress { get; set; }

        /// <summary>
        /// 户主姓名
        /// </summary>
        public string HouseOwner { get; set; }

        /// <summary>
        /// 与户主关系
        /// </summary>
        public string RelationShip { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 何时迁来
        /// </summary>
        public string MoveCityDate { get; set; }

        /// <summary>
        /// 何因来本市
        /// </summary>
        public string MoveCityReason { get; set; }

        /// <summary>
        /// 何时来本址
        /// </summary>
        public string MoveAddressDate { get; set; }

        /// <summary>
        /// 何因来本址
        /// </summary>
        public string MoveAddressReason { get; set; }

        /// <summary>
        /// 监护人一姓名
        /// </summary>
        public string GuardianOneName { get; set; }

        /// <summary>
        /// 监护人一身份号码
        /// </summary>
        public string GuardianOneCardNumber { get; set; }

        /// <summary>
        /// 监护人一关系
        /// </summary>
        public string GuardianOneRelation { get; set; }

        /// <summary>
        /// 监护人二姓名
        /// </summary>
        public string GuardianTwoName { get; set; }

        /// <summary>
        /// 监护人二身份号码
        /// </summary>
        public string GuardianTwoCardNumber { get; set; }

        /// <summary>
        /// 监护人二关系
        /// </summary>
        public string GuardianTwoRelation { get; set; }
    }
}