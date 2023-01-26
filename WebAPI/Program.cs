using Business.Abstract;
using Business.ActionFilters.Validation;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Abstract;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.PatientDtos;
using Entities.DTOs.UserDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using static Business.ValidationRules.FluentValidation.HospitalValidator;
using static Business.ValidationRules.FluentValidation.PatientValidator;
using static Business.ValidationRules.FluentValidation.UserValidator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ValidationFilterAttribute<PatientRegisterDto, PatientRegisterValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<PatientUpdateDto, PatientUpdateValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<PatientDeletionDto, PatientDeletionValidator>>();

builder.Services.AddScoped<ValidationFilterAttribute<UserRegisterDto, UserRegisterValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<UserUpdateDto, UserUpdateValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<UserDeletionDto, UserDeletionValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<UserLoginDto, UserLoginValidator>>();


builder.Services.AddScoped<ValidationFilterAttribute<HospitalRegisterDto, HospitalRegisterValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<HospitalUpdateDto, HospitalUpdateValidator>>();
builder.Services.AddScoped<ValidationFilterAttribute<HospitalDeletionDto, HospitalDeletionValidator>>();


builder.Services.AddSingleton<IPatientService, PatientManager>();
builder.Services.AddSingleton<IPatientDal, EfPatientDal>();

builder.Services.AddSingleton<IHospitalService, HospitalManager>();
builder.Services.AddSingleton<IHospitalDal, EfHospitalDal>();

builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();

builder.Services.AddSingleton<IAuthService, AuthManager>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();

builder.Services.AddSingleton<IRefreshTokenDal, EfRefreshTokenDal>();
builder.Services.AddSingleton<IRefreshTokenService, RefreshTokenManager>();

builder.Services.AddCors();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
        ValidAudience = builder.Configuration["TokenOptions:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(builder.Configuration["TokenOptions:SecurityKey"]),
        ClockSkew = TimeSpan.Zero // So important for expiration date to work properly
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureCustomExceptionMiddleware();


app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();