using Newtonsoft.Json;
using System.IO;
using System.Net.Http;


namespace RealDebrid.service
{
    class HttpService<T>
    {
        private HttpMethod method;
        private string url;
        private HttpClient client;
        private HttpContent content;

        public HttpService(string url, HttpMethod method, Dictionary<string, string> headers, HttpContent body)
        {
            client = new HttpClient();

            foreach (KeyValuePair<String, String> header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            this.method = method;
            this.url = url;
            this.content = body;
        }

        public async Task<T> jsonResponseAsync()
        {
            Console.WriteLine("INFO => URL: {} HEADERS : {}", client.BaseAddress, client.DefaultRequestHeaders.ToString());

            var task = await getCall();

            if (task.IsSuccessStatusCode)
            {
                string response = task.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(response);
                task.Dispose();
                return result;
            }
            return default;
        }


        public async Task<Stream> bodyResponseAsync()
        {
            Console.WriteLine("INFO => URL: {} HEADERS : {}", client.BaseAddress, client.DefaultRequestHeaders.ToString());
            var task = await getCall();
            if (task.IsSuccessStatusCode)
            {
                return await task.Content.ReadAsStreamAsync();
            }
            return default;
        }


        public async Task<HttpContent> httpContentAsync()
        {
            Console.WriteLine("INFO => URL: {} HEADERS : {}", client.BaseAddress, client.DefaultRequestHeaders.ToString());
            var task = await getCall();
            if (task.IsSuccessStatusCode)
            {
                return task.Content;
            }
            return default;
        }


        private Task<HttpResponseMessage> getCall()
        {
            if (method == HttpMethod.Get)
            {
                return client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            }
            else if (method == HttpMethod.Post)
            {
                return client.PostAsync(url, content);
            }
            else if (method == HttpMethod.Delete)
            {
                return client.DeleteAsync(url);
            }
            else
            {
                return null;
            }

        }
    }
}
