using BookingDreams.Data;
using BookingDreams.Helpers;
using BookingDreams.Respositories;
using BookingDreamsServices.Model;
using BookingDreamsServices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//using NETCore.MailKit.Core;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op=>
{
    op.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    op.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



//Khai báo để api public cho mn sử dụng
builder.Services.AddCors(options => options.AddPolicy("MyCors",policy => policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
//Đăng kí đb
builder.Services.AddIdentity<TaiKhoan, IdentityRole>().AddEntityFrameworkStores<BookingDreamsContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<BookingDreamsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDreams"));
});
//Đăng kí mapper
builder.Services.AddAutoMapper(typeof(Program));
//Đăng ký folder res
builder.Services.AddScoped<FuncSupport>();
builder.Services.AddScoped<KhachSanRes>();
//builder.Services.AddScoped<ChucVuRes>();
builder.Services.AddScoped<DatPhongRes>();
builder.Services.AddScoped<DichVuRes>();
//builder.Services.AddScoped<HinhAnhRes>();
builder.Services.AddScoped<KhachHangRes>();
//builder.Services.AddScoped<PhanQuyenRes>();
builder.Services.AddScoped<PhongRes>();
//uilder.Services.AddScoped<RoleRes>();
builder.Services.AddScoped<TaiKhoanRes>();
builder.Services.AddScoped<ThanhToanRes>();
builder.Services.AddScoped<TinhThanhRes>();
builder.Services.AddScoped<MaGiamGiaRes>();
builder.Services.AddScoped<DanhGiaRes>();
builder.Services.AddScoped<Microsoft.AspNetCore.Mvc.Routing.UrlActionContext>();

//Thêm cấu hình email
builder.Services.Configure<IdentityOptions>(op => op.SignIn.RequireConfirmedEmail = true);
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailService,EmailService>();
//builder.Services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
//{
//    var actionContext = implementationFactory.GetRequiredService<IActionContextAccessor>().ActionContext;
//    var factory = implementationFactory.GetRequiredService<IUrlHelperFactory>();
//    return (UrlHelper)factory.GetUrlHelper(actionContext);
//});
//Khai bao JWT
builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op=>
{
    op.SaveToken = true;
    op.RequireHttpsMetadata = false;
    op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Sử dụng cors
app.UseCors("MyCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
