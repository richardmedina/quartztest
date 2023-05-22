using Quartz;
using QuartzTest;
using QuartzTest.HostedServices;
using QuartzTest.JobFactories;
using QuartzTest.JobParameters;
using QuartzTest.Jobs;
using QuartzTest.Services;

IScheduler scheduler = await QuartzUtils.ConfigureQuartz();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton(provider => scheduler);
// builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IEmailService, EmailService>();
// builder.Services.AddTransient<IocJob>();

var app = builder.Build();

await QuartzUtils.ScheduleIocJob(scheduler, app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
