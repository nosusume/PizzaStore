using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore.Services;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => { });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 添加swagger文档
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// 设置Mysql数据库连接
builder.Services.AddDbContext<PizzaContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 注册Pizza服务
builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

// 添加Swagger文档
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}

// 初始化数据库
app.CreateDbIfNotExists();
app.MapControllers();

app.Run();
