using ForumApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ForumApiDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
