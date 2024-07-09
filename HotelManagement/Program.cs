using BusinessObjects;
using Quartz;
using Repository;
using Service;
using Service.Quartzs;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<FuminiHotelManagementContext, FuminiHotelManagementContext>();

// Repositories
builder.Services.AddScoped<IBookingHistoryRepository, BookingHistoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// Services
builder.Services.AddScoped<IBookingHistoryService, BookingHistoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Register SignalR
builder.Services.AddSignalR().AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.PayloadSerializerOptions.MaxDepth = 64;
});
builder.Services.AddTransient<BookingHub>();

//Register quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Configure UpdateRoomStatusJob
    var statusJobKey = new JobKey("UpdateRoomStatusJob");
    q.AddJob<UpdateRoomStatusJob>(opts => opts.WithIdentity(statusJobKey));

    var statusCronSchedule = builder.Configuration.GetSection("CronJobs:UpdateRoomStatusJob")?.Value;
    if (string.IsNullOrWhiteSpace(statusCronSchedule))
    {
        throw new ArgumentException("The cron schedule for UpdateRoomStatusJob is not configured properly.");
    }

    q.AddTrigger(opts => opts
        .ForJob(statusJobKey)
        .WithIdentity("UpdateRoomStatusJob-trigger")
        .WithCronSchedule(statusCronSchedule));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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

app.UseSession();

app.MapHub<BookingHub>("/bookingHub");

app.Run();
