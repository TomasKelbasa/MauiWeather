using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Services
{
    interface IWeatherHelperService
    {
        public static abstract Task<RestResponse> FetchFromApi(string apiRequest);
    }
}
