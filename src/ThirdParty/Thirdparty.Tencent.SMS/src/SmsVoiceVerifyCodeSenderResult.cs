using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;


namespace qcloudsms_csharp
{
    public class SmsVoiceVerifyCodeSenderResult : SmsResultBase
    {
        public int result;
        public string errMsg;
        public string ext;
        public string callid;

        public SmsVoiceVerifyCodeSenderResult()
        {
            this.errMsg = "";
            this.ext = "";
            this.callid = "";
        }

        public override void parseFromHTTPResponse(HTTPResponse response)
        {
            JObject json = parseToJson(response);

            try
            {
                result = json.GetValue("result").Value<int>();
                errMsg = json.GetValue("errmsg").Value<string>();
            }
            catch (ArgumentNullException e)
            {
                throw new JSONException(String.Format("res: {0}, exception: {1}", response.body, e.Message));
            }

            if (result == 0)
            {
                try
                {
                    ext = json.GetValue("ext").Value<string>();
                    callid = json.GetValue("callid").Value<string>();
                }
                catch (ArgumentNullException e)
                {
                    throw new JSONException(String.Format("res: {0}, exception: {1}", response.body, e.Message));
                }
            }
        }
    }
}