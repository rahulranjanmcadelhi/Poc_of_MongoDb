using DocumentDbPoc.Persistance;
using DocumentDbPoc.Repository;
using DocumentDbPoc.Service.Business.Concrete;
using DocumentDbPoc.Service.Business.Interface;
using DocumentDbPoc.Service.DocumentDbServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();

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

app.Run();
