﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using MassTransit;
using MassTransit.Transports.RabbitMq;

namespace WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bus.Initialize(
                sbc =>
                {
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom("rabbitmq://Ahk5wwl1/azurewebsite");
                });
        }
    }
}
