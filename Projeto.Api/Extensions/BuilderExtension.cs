﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Projeto.Core;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace Projeto.Api.Extensions
{
    public static class BuilderExtension
    {
        public static void AdicionarBanco(
                this IServiceCollection services
            )
        {
            services.AddScoped<SqlConnection>(x
                => new SqlConnection(Configuracao.BancoDados.StringConexao));
        }

        public static void AdicionarConfiguracaoSegredos(this WebApplicationBuilder builder)
        {
            Configuracao.BancoDados.StringConexao =
                builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            Configuracao.SenhaSegredos.ChaveApi =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveApi") ?? string.Empty;
            Configuracao.SenhaSegredos.JwtChavePrivada =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveJwtPrivado") ?? string.Empty;
            Configuracao.SenhaSegredos.ChaveSenhaSalt =
                builder.Configuration.GetSection("SenhaSegredos").GetValue<string>("ChaveSenhaSalt") ?? string.Empty;
        }

        public static void AdicionarConfiguracaoSendgrid(this WebApplicationBuilder builder)
        {
            Configuracao.SendgridApi.ChaveApi =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("ChaveApi") ?? string.Empty;            
            Configuracao.SendgridDestinatario.EmailDestinatarioPadrao =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("EmailDestinatario") ?? string.Empty;            
            Configuracao.SendgridDestinatario.NomeDestinatarioPadrao =
                builder.Configuration.GetSection("SendGrid").GetValue<string>("NomeDestinatario") ?? string.Empty;
        }

        public static void AdicionarMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(x
                => x.RegisterServicesFromAssembly(typeof(Configuracao).Assembly));
        }

        public static void AdicionarConfiguracaoCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });
        }

        public static void AdicionarAutenticacao(this WebApplicationBuilder builder)
        {
            byte[] key = Encoding.ASCII.GetBytes(Configuracao.SenhaSegredos.JwtChavePrivada);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
);
        }
    }
}
