
namespace ASample.ThirdParty.OAuth.Google.Model
{
    public class Constants
    {
        internal const string DefaultAuthenticationType = "Google";

        internal const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        internal const string TokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        internal const string UserInformationEndpoint = "https://www.googleapis.com/plus/v1/people/me";




        #region 新浪微博
        //public const string DefaultAuthenticationType = "weibo";
        ///// <summary>
        ///// Default value for <see cref="OAuthOptions.AuthorizationEndpoint"/>.
        ///// </summary>
        //public const string AuthorizationEndpoint = "https://api.weibo.com/oauth2/authorize";

        ///// <summary>
        ///// Default value for <see cref="OAuthOptions.TokenEndpoint"/>.
        ///// </summary>
        //public const string TokenEndpoint = "https://api.weibo.com/oauth2/access_token";

        ///// <summary>
        ///// Default value for <see cref="OAuthOptions.UserInformationEndpoint"/>.
        ///// </summary>
        //public const string UserInformationEndpoint = "https://api.weibo.com/2/users/show.json";
        #endregion

    }
}
