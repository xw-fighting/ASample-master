using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ASample.DataExportToExcel.Test.Models
{
    /// <summary>
    /// 浙江省最多跑一次办事指南
    /// </summary>
    [DataContract]
    public class OnceTransactGuideModel
    {
        public string Key { get; set; }

        /// <summary>
        /// 适用范围
        /// </summary>
        [DataMember(Name = "适用范围")]
        public string Scope { get; set; }

        /// <summary>
        /// 事项类型
        /// </summary>
        [DataMember(Name = "事项类型")]
        public string Category { get; set; }

        /// <summary>
        /// 权力来源
        /// </summary>
        [DataMember(Name = "权力来源")]
        public string Source { get; set; }

        /// <summary>
        /// 受理机构
        /// </summary>
        [DataMember(Name = "受理机构")]
        public string AcceptOrg { get; set; }

        /// <summary>
        /// 决定机构
        /// </summary>
        [DataMember(Name = "决定机构")]
        public string DecisionOrg { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMember(Name = "联系电话")]
        public string Tel { get; set; }

        /// <summary>
        /// 责任科室
        /// </summary>
        [DataMember(Name = "责任处_科_室")]
        public string DutyOffice { get; set; }

        /// <summary>
        /// 申请方式
        /// </summary>
        [DataMember(Name = "申请方式")]
        public string ApplyType { get; set; }

        /// <summary>
        /// 事项审查类型
        /// </summary>
        [DataMember(Name = "事项审查类型")]
        public string InspectType { get; set; }


        /// <summary>
        /// 咨询电话
        /// </summary>
        //[DataMember(Name = "咨询电话")]
        //public string ConsultTel { get; set; }


        /// <summary>
        /// 监督投诉电话
        /// </summary>
        [DataMember(Name = "监督投诉电话")]
        public string SuperviseTel { get; set; }


        /// <summary>
        /// 审批结果
        /// </summary>
        [DataMember(Name = "审批结果")]
        public string ApprovalResult { get; set; }
        /// <summary>
        /// 办结时限
        /// </summary>
        [DataMember(Name = "办结时限")]
        public string CompletedTime { get; set; }


        /// <summary>
        /// 结果送达
        /// </summary>
        [DataMember(Name = "结果送达")]
        public string Delivery { get; set; }


        /// <summary>
        /// 办理地点、时间
        /// </summary>
        [DataMember(Name = "办公地址_时间")]
        public string TimePlace { get; set; }


        /// <summary>
        /// 办事者到办事现场次数
        /// </summary>
        [DataMember(Name = "办事者到办事现场次数")]
        public string SpotTimes { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        [DataMember(Name = "事项标题")]
        public string Title { get; set; }

        //1公共服务
        /// <summary>
        /// 服务单位
        /// </summary>
        [DataMember(Name = "服务单位")]
        public string ComServiceDepartment { get; set; }

        /// <summary>
        /// 主管部门
        /// </summary>
        [DataMember(Name = "主管部门")]
        public string ComResponsibleDepartment { get; set; }

        /// <summary>
        /// 服务对象
        /// </summary>
        [DataMember(Name = "服务对象")]
        public string ComServiceTarget { get; set; }

        /// <summary>
        /// 事项子类型
        /// </summary>
        [DataMember(Name = "事项子类型")]
        public string ComChildCategory { get; set; }

        /// <summary>
        /// 服务内容
        /// </summary>
        [DataMember(Name = "服务内容")]
        public string ComServiceContent { get; set; }

        /// <summary>
        /// 办理时间
        /// </summary>
        [DataMember(Name = "办理时间")]
        public string ComTransactTime { get; set; }

        /// <summary>
        /// 办理时限
        /// </summary>
        [DataMember(Name = "办理时限")]
        public string ComLimitTime { get; set; }

        /// <summary>
        /// 法定期限
        /// </summary>
        [DataMember(Name = "法定期限")]
        public string ComDeadLine { get; set; }

        /// <summary>
        /// 承诺期限
        /// </summary>
        [DataMember(Name = "承诺期限")]
        public string ComPromiseDeadLine { get; set; }


        //[DataMember(Name = "申请材料")]
        //public List<OnceMaterial> OnceMaterialList { get; set; }

        //[DataMember(Name = "TabContents")]
        //public List<OnceTabContent> OnceTabContents { get; set; }
    }
}
