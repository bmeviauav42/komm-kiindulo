using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Msa.Comm.Lab.Services.Order.ApiClients;
using Polly;
using Refit;

namespace Msa.Comm.Lab.Services.Order
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
            services.AddMvc();

            bool RetryableStatusCodesPredicate(HttpStatusCode statusCode) =>
                statusCode == HttpStatusCode.BadGateway
                    || statusCode == HttpStatusCode.ServiceUnavailable
                    || statusCode == HttpStatusCode.GatewayTimeout;

            services.AddRefitClient<ICatalogApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://msa.comm.lab.services.catalog"))
                .AddPolicyHandler(Policy
                    .Handle<HttpRequestException>()
                    .OrResult<HttpResponseMessage>(msg => RetryableStatusCodesPredicate(msg.StatusCode))
                    //.RetryAsync(5)
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                ));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
