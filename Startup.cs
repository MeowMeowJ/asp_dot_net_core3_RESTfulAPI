using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Api.Data;
using Routine.Api.Services;

namespace Routine.Api
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true; // ��Ϊtrue�Ļ��������ʽ��ƥ����Է���406״̬��
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); // �������ʽ���������xml��ʽ�����������ӷ�������Ϊxml��֧�֣���Ĭ�ϸ�ʽ������Json��Add��Add������
                // setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter()); // �ڵ����λ�ò���xml��ʼ��������˳������xml-json��Ĭ�ϵľ���xml
            }).AddXmlDataContractSerializerFormatters(); // ��仰�����ڶ������ܣ�����input�ĸ�ʽ����Ҳ�����

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICompanyRepository,CompanyRepository>();

            services.AddDbContext<RoutineDbContext>(options => 
                { options.UseSqlite("Data Source=routine.db"); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
