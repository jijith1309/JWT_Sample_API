﻿using JWT_SampleApp.TokenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JWT_SampleApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new JwtTokenHandler());
        }
    }
}
