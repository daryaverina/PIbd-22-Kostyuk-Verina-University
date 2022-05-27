using UniversityContracts.StorageContracts;
using UniversityBusinessLogic.BusinessLogics;
using UniversityContracts.BusinessLogicsContracts;
using UniversityDatabaseImplement.Implements;
using UniversityBusinessLogic.MailWorker;
using UniversityContracts.BindingModels;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UniversityBusinessLogic.OfficePackage;
using UniversityBusinessLogic.OfficePackage.Implements;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDecreeStorage, DecreeStorage>();
builder.Services.AddTransient<IEducationalStatusStorage, EducationalStatusStorage>();
builder.Services.AddTransient<IProviderStorage, ProviderStorage>();
builder.Services.AddTransient<IStudentStorage, StudentStorage>();
builder.Services.AddTransient<IFlowStorage, FlowStorage>();
builder.Services.AddTransient<IGroupStorage, GroupStorage>();
builder.Services.AddTransient<ISubjectStorage, SubjectStorage>();

builder.Services.AddTransient<IDecreeLogic, DecreeLogic>();
builder.Services.AddTransient<IEducationalStatusLogic, EducationalStatusLogic>();
builder.Services.AddTransient<IProviderLogic, ProviderLogic>();
builder.Services.AddTransient<IStudentLogic, StudentLogic>();
builder.Services.AddTransient<IFlowLogic, FlowLogic>();
builder.Services.AddTransient<IGroupLogic, GroupLogic>();
builder.Services.AddTransient<ISubjectLogic, SubjectLogic>();

builder.Services.AddTransient<ProviderAbstractSaveToExcel, ProviderSaveToExcel>();
builder.Services.AddTransient<ProviderAbstractSaveToWord, ProviderSaveToWord>();
builder.Services.AddTransient<ProviderAbstractSaveToPdf, ProviderSaveToPdf>();

builder.Services.AddSingleton<MailKitWorker>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversityProviderRestApi v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

var mailSender = app.Services.GetService<MailKitWorker>();
mailSender.MailConfig(new MailConfigBindingModel
{
    //SmtpClientHost = configuration["SmtpClientHost"],
    //SmtpClientPort = Convert.ToInt32(configuration["SmtpClientPort"]),
    //MailLogin = configuration["MailLogin"],
    //MailPassword = configuration["MailPassword"],

    //MailLogin = Configuration?.GetSection("MailLogin")?.Value.ToString(),
    //MailPassword = Configuration?.GetSection("MailPassword")?.Value.ToString(),
    //SmtpClientHost = Configuration?.GetSection("SmtpClientHost")?.Value.ToString(),
    //SmtpClientPort = Convert.ToInt32(Configuration?.GetSection("SmtpClientPort")?.Value.ToString()),
    //PopHost = Configuration?.GetSection("PopHost")?.Value.ToString(),
    //PopPort = Convert.ToInt32(Configuration?.GetSection("PopPort")?.Value.ToString())
});

app.Run();
