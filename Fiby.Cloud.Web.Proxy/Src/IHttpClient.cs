using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Proxy.Src
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        Task<string> GetStringAsync<T>(string uri, T item);
        Task<HttpResponseMessage> PostInitAsync<T>(string uri, T item);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string requestId = null);
        Task<HttpResponseMessage> DeleteAsync(string uri, string requestId = null);
        Task<HttpResponseMessage> DeleteAsync<T>(string uri, T item, string requestId = null);
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string requestId = null);
    }
}
