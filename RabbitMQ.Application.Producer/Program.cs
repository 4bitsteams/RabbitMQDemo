using RabbitMQ.Application.Producer.BackgroundTasks;
using RabbitMQ.Application.Producer.IntegrationEvents;
using RabbitMQ.Application.Producer;
using RabbitMQ.Client;
using RabbitMQCommon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHostedService<LogBackgroundTask>()
                .AddSingleton<IRabbitMqProducer<LogIntegrationEvent>, LogProducer>()
                .AddSingleton(serviceProvider =>
                {
                    var uri = new Uri("amqp://guest:guest@rabbit:5672/CUSTOM_HOST");
                    return new ConnectionFactory
                    {
                        Uri = uri
                    };
                });

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
