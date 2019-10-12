using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using Hellang.Middleware.ProblemDetails;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Msa.Comm.Lab.Services.Catalog.Exceptions;
using Msa.Comm.Lab.Services.Catalog.IntegrationEventHandlers;
using Msa.Comm.Lab.Services.Catalog.IntegrationEvents;

namespace Msa.Comm.Lab.Services.Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedEventHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri($"rabbitmq://rabbitmq:"), hostConfig =>
                    {
                        hostConfig.Username("guest");
                        hostConfig.Password("guest");
                    });

                    cfg.UseExtensionsLogging(provider.GetRequiredService<ILoggerFactory>());

                    cfg.ReceiveEndpoint(host, "integration", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(mr => mr.Interval(2, 100));
                        ep.ConfigureConsumer<OrderCreatedEventHandler>(provider);
                    });
                }));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseProblemDetails();

            //app.UseHttpsRedirection();
            app.UseMvc();

            var bus = app.ApplicationServices.GetService<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
            applicationLifetime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
