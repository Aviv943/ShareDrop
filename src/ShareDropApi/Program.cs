using ShareDropApi.Application.Services.Implementations;
using ShareDropApi.Application.Services.Interfaces;
using ShareDropApi.Domain.Providers;
using ShareDropApi.Infrastructure.Providers;
using System.Diagnostics;
using TextCopy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddEventLog();
builder.Services.AddControllers();
builder.WebHost.UseUrls("http://0.0.0.0:5170");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWindowsService();
builder.Services.AddSingleton<INotificationProvider, NotificationProvider>();
builder.Services.AddSingleton<IClipboardProvider>(CreateClipboardProvider);
builder.Services.AddSingleton<IDropService>(CreateSharedropService);
builder.Services.AddSingleton<ICopyService, CopyService>();
builder.Services.InjectClipboard();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

IClipboardProvider CreateClipboardProvider(IServiceProvider serviceProvider)
{
    var configuration = serviceProvider.GetService<IConfiguration>();
    var setClipboardTextFilePath = configuration.GetValue<string>("SetClipboardSettings:Text:FilePath");
    var setClipboardTaskFilePath = configuration.GetValue<string>("SetClipboardSettings:Task:FilePath");
    var setClipboardTaskName = configuration.GetValue<string>("SetClipboardSettings:Task:Name");
    var getClipboardTextFilePath = configuration.GetValue<string>("SetClipboardSettings:Text:FilePath");
    var getClipboardTaskFilePath = configuration.GetValue<string>("GetClipboardSettings:Task:FilePath");
    var getClipboardTaskName = configuration.GetValue<string>("GetClipboardSettings:Task:Name");
    var getClipboardOutputFilePath = configuration.GetValue<string>("GetClipboardSettings:Output:FilePath");

    CreateTaskProcess(getClipboardTaskFilePath);
    CreateTaskProcess(setClipboardTaskFilePath);

    return new ClipboardProvider(setClipboardTextFilePath,setClipboardTaskName,getClipboardTaskName, getClipboardOutputFilePath);
}

IDropService CreateSharedropService(IServiceProvider serviceProvider)
{
    var configuration = serviceProvider.GetService<IConfiguration>();
    var notificationProvider = serviceProvider.GetRequiredService<INotificationProvider>();
    var folderPath = configuration.GetValue<string>("ShareDropPath");

    return new DropService(notificationProvider, folderPath);
}

void CreateTaskProcess(string filePath)
{
    ProcessStartInfo createTasksProcess = new ProcessStartInfo
    {
        FileName = filePath,
        WindowStyle = ProcessWindowStyle.Hidden
    };

    Process.Start(createTasksProcess);
}