using System.Security.Principal;

namespace DRapid.Web.VisitAnalyse.Core
{
    public class VisitIdentity : IIdentity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 认证类型
        /// </summary>
        public virtual string AuthenticationType { get; set; }

        /// <summary>
        /// 是否认证通过
        /// </summary>
        public virtual bool IsAuthenticated { get; set; }

        public static VisitIdentity Build(IIdentity identity)
        {
            if (identity == null)
                return null;
            return new VisitIdentity
            {
                Name = identity.Name,
                IsAuthenticated = identity.IsAuthenticated,
                AuthenticationType = identity.AuthenticationType
            };
        }
    }
}