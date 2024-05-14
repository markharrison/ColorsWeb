//using System;
//using ColorsWeb;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;


namespace ColorsWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.ConfigureFilter(new Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryTokenAttribute());
                    });
            //builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<AppConfig>(new AppConfig(builder.Configuration));
            //builder.Services.AddResponseCaching();
            //builder.Services.AddApplicationInsightsTelemetry();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    //OnPrepareResponse = (context) =>
            //    //{
            //    //    var headers = context.Context.Response.GetTypedHeaders();
            //    //    headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            //    //    {
            //    //        Public = true,
            //    //        MaxAge = TimeSpan.FromDays(1)
            //    //    };
            //    //}
            //});

            // app.UseResponseCaching();

            app.UseRouting();

            app.MapRazorPages();

            app.Run();
        }
    }
}