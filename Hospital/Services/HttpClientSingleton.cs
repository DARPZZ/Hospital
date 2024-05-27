using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public static class HttpClientSingleton
    {
        public static readonly HttpClientHandler Handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        public static readonly HttpClient Client = new HttpClient(Handler);
    }
}

