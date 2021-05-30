using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using WebApplication1_2021_02_17_secondASPNET.Repository;
using WebApplication1_2021_02_17_secondASPNET.Repository.Users;

namespace WebApplication1_2021_02_17_secondASPNET
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUsersRepository, UsersDatabaseRepository>();
            services.AddSingleton<UserDataManager>();

            services.AddSingleton<IPredictionsRepository, PredictionDatabaseInMemory>();
            services.AddSingleton<PredictionsManager>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = new PathString("/auth"));
            services.AddAuthorization();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
            });

            app.UseEndpoints(endpoints =>
            {
                #region pages
                endpoints.MapGet("/auth", async context =>
                {
                    string page = File.ReadAllText(@"Site/loginPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/register", async context =>
                {
                    string page = File.ReadAllText(@"Site/registrationPage.html");
                    await context.Response.WriteAsync(page);
                });
                //endpoints.MapGet("/password", async context =>
                //{
                //    string password = "admin";
                //    await context.Response.WriteAsync(password);
                //});
                endpoints.MapPost("/login", async context =>
                {
                    Credentials requestedCredentials = await context.Request.ReadFromJsonAsync<Credentials>();
                    UserData user = app.ApplicationServices.GetService<UserDataManager>().GetUser(requestedCredentials.Login);
                    if (user!=null)
                    {
                        if (requestedCredentials.Login == user.Login && requestedCredentials.Password == user.Password)
                        {
                            List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, requestedCredentials.Login)
                        };// ������� ������ ClaimsIdentity
                            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                            context.Response.Redirect("/adminPage");

                        }
                        string answ = (requestedCredentials.Login == user.Login && requestedCredentials.Password == user.Password) ? "ok" : "no";

                        await context.Response.WriteAsync(answ);
                    }
                    else
                    {

                    }
                });
                endpoints.MapPost("/registrate", async context =>
                {
                    UserRegistrationData newUsersData = await context.Request.ReadFromJsonAsync<UserRegistrationData>();
                    UserDataManager userDataManager = app.ApplicationServices.GetService<UserDataManager>();
                    UserData AddingUser = new UserData(newUsersData);
                    userDataManager.RegistrateUser(AddingUser);
                    await context.Response.WriteAsync("success");
                });

                endpoints.MapGet("/adminPage", async context =>
                {
                    string login = context.User.Identity.Name;
                    UserData user = app.ApplicationServices.GetService<UserDataManager>().GetUser(login);
                    if (user!=null && user.Role == UserRole.Admin)
                    {
                        string page = File.ReadAllText(@"Site/adminPage.html");
                        await context.Response.WriteAsync(page);
                    }
                    else
                    {
                        await context.Response.WriteAsync("alert(\"asd\")");
                    }
                }).RequireAuthorization();
                endpoints.MapGet("/answers", async context =>
                {
                    string page = File.ReadAllText(@"Site/answersPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/", async context =>
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
                endpoints.MapGet("/get_random_prediction", async context =>
                {
                    string table = context.Request.Headers[":path"][0].Split('?')[1];
                    await context.Response.WriteAsync(table);
                });
                endpoints.MapGet("/DoSomething", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    var query = context.Request.Query;
                    string text = query["spend"];
                    string text2 = query["test"];
                    string text3 = query["what"];
                    pm.AddPrediction($"{text} {text2.Remove(text2.Length - 1)}  --- {text3}");
                    await context.Response.WriteAsync("successfully added");
                });
                #endregion
                #region Post
                endpoints.MapGet("/GetAllPredictions", async context =>
                {
                    PredictionsManager pm = app.ApplicationServices.GetService<PredictionsManager>();
                    var answers = new { answers = pm.GetAnswers() };
                    await context.Response.WriteAsJsonAsync(answers);
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
