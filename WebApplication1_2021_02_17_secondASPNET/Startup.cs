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
            services.AddSingleton<PredictionsManager>();
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
                    string page = File.ReadAllText(@"Site/basePage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/adminPage", async context =>
                {
                    string page = File.ReadAllText(@"Site/adminPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/answers", async context =>
                {
                    string page = File.ReadAllText(@"Site/answersPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/predictions", async context =>
                {
                    string page = File.ReadAllText(@"Site/predictionsPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/experiments", async context =>
                {
                    string page = File.ReadAllText(@"Site/experimentsPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/moldenbrodExperiments", async context =>
                {
                    string page = File.ReadAllText(@"Site/moldenbrodExperimentsPage.html");
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
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
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
                endpoints.MapGet("/DoSomething", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    var query = context.Request.Query;
                    string text = query["spend"];
                    string text2 = query["test"];
                    string text3 = query["what"];
                    pm.AddPrediction($"{text} {text2.Remove(text2.Length-1)}  --- {text3}");
                    await context.Response.WriteAsync("successfully added");
                });
                #endregion
                #region Post
                endpoints.MapPost("/addPrediction", async context =>
                {
                //    if (!context.Request.HasJsonContentType())
                //    {
                //        context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                //        return;
                //    }

                //    //var resp = await context.Request.ReadFromJsonAsync<Dictionary<string, string>>();
                //    //string text = resp["text"];

                //    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                //    //var query = context.Request.Query;
                //    //string text = query["newPrediction"];
                //    pm.AddPrediction(text);
                //    await context.Response.WriteAsync("successfully added");
                PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    Prediction resp = await context.Request.ReadFromJsonAsync<Prediction>();
                    pm.AddPrediction(resp.PredictionString);
                    await context.Response.WriteAsync(resp.PredictionString);
                });
                endpoints.MapGet("/GetAllPredictions", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    var answers = new { answers = pm.GetAnswers() };
                    await context.Response.WriteAsJsonAsync(answers);
                });
                endpoints.MapPost("/removePrediction", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    Prediction resp = await context.Request.ReadFromJsonAsync<Prediction>();
                    pm.RemovePrediction(resp.PredictionString);
                    await context.Response.WriteAsync(resp.PredictionString);
                });
                endpoints.MapPut("/changePrediction", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    PredictionChanged resp = await context.Request.ReadFromJsonAsync<PredictionChanged>();
                    pm.ChangePrediction(resp.OldPredictionString, resp.NewPredictionString);
                });
                #endregion
                #region commands executer

                endpoints.MapGet("/execute", async context =>
                {
                    string page = File.ReadAllText(@"Site/commandsExecuter.html");
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
                endpoints.MapGet("/Add_To_CommandsList", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    var query = context.Request.Query;
                    string text = query["CommandsToAdd"];
                    Console.WriteLine(text);
                    using (StreamWriter writer = new StreamWriter(query["way"]))
                    {
                        foreach (var c in text)
                        {
                            switch (c)
                            {
                                case 'u':
                                    writer.Write("Up ");
                                    break;
                                case 'd':
                                    writer.Write("Down ");
                                    break;
                                case 'l':
                                    writer.Write("Left ");
                                    break;
                                case 'r':
                                    writer.Write("Right ");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    await context.Response.WriteAsync("successfully added");
                });
                #endregion
            });
        }
    }
}
