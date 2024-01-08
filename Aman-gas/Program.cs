using BL.Helpers;
using BL.IServices;
using BL.Services;
using BL.TwilioSMSService;
using BL.UOW;
using Data;
using Data.Entities;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
 string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";






builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:52594")
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials();
        });
});
// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<TwilioModel>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DbContainer>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnention"), b => b.MigrationsAssembly("Aman-Gas")));
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
}
         ).AddEntityFrameworkStores<DbContainer>();

// Auto Mapper
builder.Services.AddAutoMapper(x=>x.AddProfile<DomainProfile>());


#region Swagger Service 
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Aman-Gas Project",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion
// Authenticatinon
#region ConfigureAuthentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        //  ValidIssuer = Configuration["JWT:Isser"],
        ValidIssuer = builder.Configuration["JWT:Isseur"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});
#endregion


// HangFire 
#region Configure Hangfire 
//builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnention")));
//builder.Services.AddHangfireServer();
#endregion
// Injections 
#region injections
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddScoped< DbContainer>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepo<>),typeof(BaseRepo<>));
builder.Services.AddScoped< DbContainer>();
builder.Services.AddScoped<IStationService , StationService>();
builder.Services.AddScoped<IFuelingService , FuelingService>();
builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddScoped<IReportService , ReportService>();
builder.Services.AddScoped<IManagerService , ManagerService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion




var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseRouting();
//app.UseHangfireDashboard("/BackGroundTasks");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseFileServer();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
