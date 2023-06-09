using BL.Helpers;
using BL.IServices;
using BL.Services;
using BL.TwilioSMSService;
using Data;
using Data.Entities;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<TwilioModel>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

#region Configure Hangfire 
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnention")));
builder.Services.AddHangfireServer();
#endregion
#region injection 
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ISMSService, SMSService>();
#endregion




var app = builder.Build();
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
app.UseHangfireDashboard("/BackGroundTasks");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseFileServer();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
