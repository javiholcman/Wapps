using System;
using System.Threading.Tasks;
using System.Net;

namespace Wapps.Core
{
    public static class WebRequestExtensions
    {
        public static Task<WebResponse> GetResponseAsync(this WebRequest request)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
                {
                    var t = Task.Factory.FromAsync<WebResponse>(
                        request.BeginGetResponse,
                        request.EndGetResponse,
                        null);

                    t.Wait();

                    return t.Result;
                });
        }

    }
}

