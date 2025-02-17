using WebAPI.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ע������ע��
builder.Register();

// ��� CORS ����
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

#region ��Ȩ��Ȩ
app.UseAuthentication();
app.UseAuthorization();
#endregion

// ���� CORS
app.UseCors("AllowAll");

app.MapControllers();

// ��� API ���е� URL���������
Console.WriteLine($"API running at {string.Join(", ", app.Urls)}");

app.Run();
