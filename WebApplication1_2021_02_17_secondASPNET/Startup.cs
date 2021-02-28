using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                #region pages
                endpoints.MapGet("/", async context =>
                {
                    string page = File.ReadAllText(@"Site\basePage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/adminPage", async context =>
                {
                    string page = File.ReadAllText(@"Site\adminPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/answers", async context =>
                {
                    string page = File.ReadAllText(@"Site\answersPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/predictions", async context =>
                {
                    string page = File.ReadAllText(@"Site\predictionsPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/experiments", async context =>
                {
                    string page = File.ReadAllText(@"Site\experimentsPage.html");
                    await context.Response.WriteAsync(page);
                });
                #endregion
                #region Getter
                endpoints.MapGet("/password", async context =>
                {
                    string password = "admin";
                    await context.Response.WriteAsync(password);
                });
                endpoints.MapGet("/login", async context =>
                {
                    string login = "loginn";
                    await context.Response.WriteAsync(login);
                });
                endpoints.MapGet("/getAnswers", async context =>
                {
                    PredictionsManager pm = new PredictionsManager();
                    string answer = pm.GetRandomPrediction();

                    await context.Response.WriteAsync(answer);// + context.Request.Body);
                });
                endpoints.MapGet("/get_random_prediction", async context =>
                {
                    string table = context.Request.Headers[":path"][0].Split('?')[1];
                    //foreach (var header in context.Request.Headers)
                    //{
                    //    table += $"<tr><td>||  {header.Key}</td><td>{header.Value}  ||</td></tr>";
                    //}


                    //////PredictionsManager pm = new PredictionsManager();
                    //////var query = context.Request.Query;
                    //////string text = context.Request.Headers[":path"].ToString().Split('?')[1];
                    //////pm.AddPrediction(text);
                    //////await context.Response.WriteAsync("successfully added");

                    await context.Response.WriteAsync(table);
                });
                endpoints.MapGet("/addPrediction", async context =>
                {
                    PredictionsManager pm = new PredictionsManager();
                    var query = context.Request.Query;
                    string text = query["newPrediction"];
                    pm.AddPrediction(text);
                    await context.Response.WriteAsync("successfully added");
                });
                endpoints.MapGet("/DoSomething", async context =>
                {
                    PredictionsManager pm = new PredictionsManager();
                    var query = context.Request.Query;
                    string text = query["spend"];
                    string text2 = query["test"];
                    string text3 = query["what"];
                    pm.AddPrediction($"{text} {text2.Remove(text2.Length-1)}  --- {text3}");
                    await context.Response.WriteAsync("successfully added");
                });
                #endregion

                #region commands executer

                endpoints.MapGet("/execute", async context =>
                {
                    string page = File.ReadAllText(@"Site\commandsExecuter.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/Get_executeCommands", async context =>
                {
                    string table = context.Request.Headers[":path"][0].Split('?')[1];
                    string result = "";
                    using (StreamReader reader = new StreamReader(table)) // CommandList
                    {
                        result = reader.ReadToEnd();
                    }
                    await context.Response.WriteAsync(result);
                });
                #endregion
            });
        }
    }
}
