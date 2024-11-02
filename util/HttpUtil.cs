using RealDebrid.constant;
using System.Net.Http;

namespace RealDebrid.util
{
    public class HttpUtil
    {

        public static MultipartFormDataContent generateBody(String key, String value)
        {
            MultipartFormDataContent body = new MultipartFormDataContent();
            body.Add(new StringContent(value), key);
            return body;
        }


        public static Dictionary<String, String> generateDefaultHeader(String token)
        {
            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add(Constant.AUTHORIZATION, Constant.BEARER + " " + token);
            headers.Add(Constant.USER_AGENT, Constant.DEFAULT_USER_AGENT);
            return headers;
        }

    }
}
