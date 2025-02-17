using WebAPI.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 注册依赖注入
builder.Register();

// 解决 CORS 问题
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region 鉴权授权
app.UseAuthentication();
app.UseAuthorization();
#endregion

// 启用 CORS
app.UseCors("AllowAll");

app.MapControllers();

// 输出 API 运行的 URL，方便调试
Console.WriteLine($"API running at {string.Join(", ", app.Urls)}");

app.Run();
