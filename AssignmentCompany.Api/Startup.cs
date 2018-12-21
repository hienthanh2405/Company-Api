using System;
using AssignmentCompany.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4j.Driver.V1;
using Neo4jClient;

namespace AssignmentCompany.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Data.Neo4j>(Configuration.GetSection("Neo4j"));
            RegisterNeo4JDriver(services);
            RegisterGraphClient(services);
            services.AddScoped<IGraphRepository, GraphRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .Build();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("EnableCORS");
            app.UseMvc();
        }

        //register connect with neo4j drive
        private void RegisterNeo4JDriver(IServiceCollection services)
        {
            services.AddScoped(typeof(IDriver), resolver =>
            {
                var neo4JSetting = Configuration.GetSection("Neo4j").Get<Data.Neo4j>();
                var neo4JDriver = GraphDatabase.Driver(
                    neo4JSetting.Uri,
                    AuthTokens.Basic(neo4JSetting.User, neo4JSetting.Password));

                return neo4JDriver;
            });
        }

        //register connect with graph client
        private void RegisterGraphClient(IServiceCollection services)
        {
            services.AddScoped(typeof(IGraphClient), resolver =>
            {
                var neo4JSetting = Configuration.GetSection("Neo4j").Get<Data.Neo4j>();
                var client = new BoltGraphClient(
                    new Uri(neo4JSetting.Uri),
                    neo4JSetting.User, neo4JSetting.Password);

                client.Connect();

                return client;
            });
        }
    }
}
