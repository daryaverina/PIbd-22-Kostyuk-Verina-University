using UniversityContracts.StorageContracts;
using UniversityBusinessLogic.BusinessLogics;
using UniversityContracts.BusinessLogicsContracts;
using UniversityDatabaseImplement.Implements;
using UniversityBusinessLogic.MailWorker;

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

builder.Services.AddSingleton <MailKitWorker>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


//var mailSender = app.Services.GetService<MailKitWorker>();

//mailSender.MailConfig(new MailConfigBindingModel
//{
//    MailLogin = Configuration?.GetSection("MailLogin")?.Value.ToString(),
//    MailPassword = Configuration?.GetSection("MailPassword")?.Value.ToString(),
//    SmtpClientHost = Configuration?.GetSection("SmtpClientHost")?.Value.ToString(),
//    SmtpClientPort = Convert.ToInt32(Configuration?.GetSection("SmtpClientPort")?.Value.ToString()),
//    PopHost = Configuration?.GetSection("PopHost")?.Value.ToString(),
//    PopPort = Convert.ToInt32(Configuration?.GetSection("PopPort")?.Value.ToString())
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
