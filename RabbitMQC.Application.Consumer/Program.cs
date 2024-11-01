

using MediatR;
using RabbitMQ.Client;
using RabbitMQC.Application.Consumer;
using RabbitMQC.Application.Consumer.Commands;
using RabbitMQC.Application.Consumer.Commands.Handlers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient<IRequestHandler<LogCommand, Unit>, LogCommandHandler>()
                .AddHostedService<LogConsumer>()
                .AddSingleton(serviceProvider =>
                {
                    var uri = new Uri("amqp://guest:guest@rabbit:5672/CUSTOM_HOST");
                    return new ConnectionFactory
                    {
                        Uri = uri,
                        DispatchConsumersAsync = true
                    };
                });

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

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
