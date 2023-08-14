using BookingDreams.Data;
using BookingDreams.Respositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Khai báo để api public cho mn sử dụng
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
//Đăng kí đb
builder.Services.AddDbContext<BookingDreamsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDreams"));
});
//Đăng kí mapper
builder.Services.AddAutoMapper(typeof(Program));
//Đăng ký folder res
builder.Services.AddScoped<KhachSanRes>();
builder.Services.AddScoped<ChucVuRes>();
builder.Services.AddScoped<DatPhongRes>();
builder.Services.AddScoped<DichVuRes>();
builder.Services.AddScoped<HinhAnhRes>();
builder.Services.AddScoped<KhachHangRes>();
builder.Services.AddScoped<PhanQuyenRes>();
builder.Services.AddScoped<PhongRes>();
builder.Services.AddScoped<RoleRes>();
builder.Services.AddScoped<TaiKhoanRes>();
builder.Services.AddScoped<ThanhToanRes>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
