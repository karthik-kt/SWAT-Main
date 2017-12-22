using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System;
using System.Net.Http.Headers;

namespace SWAT.TFSClient
{
    internal class TtsHttpClient
    {
        public  HttpClientHandler _HttpClientHandler 
            = new HttpClientHandler() { UseDefaultCredentials = true };
        public HttpRequestMessage _HttpPatchRequest 
            = new HttpRequestMessage() { Method = new HttpMethod("PATCH") };
        public HttpRequestMessage _HttpPostRequest 
            = new HttpRequestMessage() { Method = new HttpMethod("POST") };
        public HttpRequestMessage _HttpPutRequest
            = new HttpRequestMessage() { Method = new HttpMethod("PUT") };
        public TtsHttpClient()
        {
        }

        public async Task<string> Put(string url, string json)
        {
            using (HttpClient client = new HttpClient(_HttpClientHandler))
            {
                _HttpPutRequest.RequestUri = new Uri(url);
                _HttpPutRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                _HttpPutRequest.Content.Headers.ContentLength = Encoding.UTF8.GetByteCount(json);
                HttpResponseMessage response = await client.SendAsync(_HttpPutRequest);
                //HttpResponseMessage response =  client.PutAsJsonAsync(url, json).Result;
                return response.ToString();
            }
        }

        public async Task<string> Patch(string url, string json)
        {
            using (HttpClient client = new HttpClient(_HttpClientHandler))
            {
                _HttpPatchRequest.RequestUri = new Uri(url);
                _HttpPatchRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(_HttpPatchRequest);
                return response.ToString();
            }
        }
        
        public async Task<string> Get(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient(_HttpClientHandler))
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        return data;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> Post(string url,string json)
        {
            using (HttpClient client = new HttpClient(_HttpClientHandler))
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(url,json);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
            }
            return null;
        }

        public async Task<string> PostSend(string url, string json)
        {
            using (HttpClient client = new HttpClient(_HttpClientHandler))
            {
                 client.BaseAddress = new Uri(url);
                _HttpPostRequest.RequestUri = new Uri(url);
                _HttpPostRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                _HttpPostRequest.Content.Headers.ContentLength = Encoding.UTF8.GetByteCount(json);
                HttpResponseMessage response = await client.SendAsync(_HttpPostRequest);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
            }
            return null;
        }
    }
}
