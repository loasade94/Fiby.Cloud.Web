using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Proxy.Src
{
    public class StandardHttpClient : IHttpClient
    {
        private ILogger<StandardHttpClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private CircuitBreakerPolicy _circuitBreakerPolicy;
        //private PolicyWrap _retry;

        public StandardHttpClient(ILogger<StandardHttpClient> logger, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor, HttpClient client)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = httpContextAccessor.HttpContext;

            //_circuitBreakerPolicy = Policy.Handle<HttpRequestException>().
            //    CircuitBreaker(3, TimeSpan.FromSeconds(5));

            //_retry = Policy.Handle<HttpRequestException>()
            //        .WaitAndRetryForever(attemp => TimeSpan.FromSeconds(1))
            //        .Wrap(_circuitBreakerPolicy);
        }

        //private string GetRefreshToken()
        //{
        //    var result = this._httpContextAccessor.HttpContext
        //                      .User.Claims
        //                      .FirstOrDefault(claim => claim.Type == "TokenRefresh");

        //    return result.Value;
        //}

        private string GetValue(string cookieName, string keyName)
        {
            var principal = _httpContext.User;
            var cp = principal.Identities.First(i => i.AuthenticationType == cookieName.ToString());
            return cp.FindFirst(keyName).Value;
        }

        private async void SetValue(string cookieName, string keyName, string value)
        {
            var principal = _httpContext.User;
            var cp = principal.Identities.First(i => i.AuthenticationType == cookieName.ToString());

            if (cp.FindFirst(keyName) != null)
            {
                cp.RemoveClaim(cp.FindFirst(keyName));
                cp.AddClaim(new Claim(keyName, value));
            }

            await _httpContext.SignOutAsync();
            await _httpContext.SignInAsync(new ClaimsPrincipal(cp),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true
                });
        }

        private class AuthDTOHttpClient
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }

        private class ResponseObject<T>
        {
            public T Data { get; set; }
            public bool Success { get; set; } = true;
            public ErrorDetails Details { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        private class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }


        //private async Task<ResponseObject<AuthDTOHttpClient>> CallRefreshToken()
        //{
        //    string token = GetValue(CookieAuthenticationDefaults.AuthenticationScheme, "Token");
        //    string tokenRefresh = GetValue(CookieAuthenticationDefaults.AuthenticationScheme, "TokenRefresh");

        //    string uriRefresh = _configuration["Proxy:UrlSecurityRefresh"];
        //    _logger.LogInformation($"Send HTTP request: {uriRefresh}");
        //    ResponseObject<AuthDTOHttpClient> response = new ResponseObject<AuthDTOHttpClient>() { Success = false };
        //    AuthDTOHttpClient objHttpClient = new AuthDTOHttpClient() { Token = token, RefreshToken = tokenRefresh };
        //    var result = await PostAsync(uriRefresh, objHttpClient);
        //    if (result.StatusCode == HttpStatusCode.OK)
        //    {
        //        result.EnsureSuccessStatusCode();
        //        var data = await result.Content.ReadAsStringAsync();
        //        response = JsonConvert.DeserializeObject<ResponseObject<AuthDTOHttpClient>>(data);

        //        SetValue(CookieAuthenticationDefaults.AuthenticationScheme, "Token", response.Data.Token);
        //        SetValue(CookieAuthenticationDefaults.AuthenticationScheme, "TokenRefresh", response.Data.RefreshToken);
        //    }
        //    return response;
        //}

        #region Delete
        public async Task<HttpResponseMessage> DeleteAsync(string uri, string requestId = null)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            HttpResponseMessage response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            
            return response;
        }

        private async Task<HttpResponseMessage> DeleteRecursiveAsync(string uri)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            var response = await _client.SendAsync(requestMessage);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string uri, T item, string requestId = null)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        private async Task<HttpResponseMessage> DeleteRecursiveAsync<T>(string uri, T item)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            string token = GetValue(CookieAuthenticationDefaults.AuthenticationScheme, "Token");
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(requestMessage);
            return response;
        }

        #endregion Delete

        #region Get
        public async Task<string> GetStringAsync(string uri)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
         
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> GetStringRecursiveAsync(string uri)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);    
            var response = await _client.SendAsync(requestMessage);
            return response;
        }


        public async Task<string> GetStringAsync<T>(string uri, T item)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> GetStringRecursiveAsync<T>(string uri, T item)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(requestMessage);
            return response;
        }

        #endregion Get

        #region Post
        public async Task<HttpResponseMessage> PostInitAsync<T>(string uri, T item)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string requestId = null)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
          
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        private async Task<HttpResponseMessage> PostRecursivAsync<T>(string uri, T item)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(requestMessage);
            return response;
        }

        #endregion Post

        #region Put

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string requestId = null)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        private async Task<HttpResponseMessage> PutRecursivAsync<T>(string uri, T item)
        {
            _logger.LogInformation($"Send HTTP request: {uri}");
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);
            return response;
        }


        #endregion Put

    }
}
