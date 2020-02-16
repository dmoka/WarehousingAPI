using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using Dapper;
using KaliGasService.Core.Application.CQRS;
using KaliGasService.Core.Data.Dapper;
using KaliGasService.Core.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Warehousing.API.Application.Product.Commands;
using Warehousing.API.ServiceConfigurations;
using Warehousing.Data;
using Warehousing.Data.Database;
using Warehousing.Data.Entities.Product;
using Warehousing.Domain;
using Warehousing.Domain.Product;

namespace Warehousing.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(Configuration.GetConnectionString("WarehousingDb"));

            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<PickProductCommand>, PickProductCommandValidator>();
            services.AddScoped<IValidator<UnpickProductCommand>, NullValidator<UnpickProductCommand>>();

            services.AddScoped<IProductDao, ProductDao>();
            services.AddScoped<IProductHistoryLineDao, ProductHistoryLineDao>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductHistoryLineRepository, ProductHistoryLineRepository>();
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());

            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddMediatR(Assembly.GetExecutingAssembly(), new DomainModelExecutingAssemblyGetter().Get());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
