using App.Domain.AppServices.hw15.Card;
using App.Domain.AppServices.hw15.Transaction;
using App.Domain.Core.hw15.Card.AppServices;
using App.Domain.Core.hw15.Card.Data.Repositories;
using App.Domain.Core.hw15.Card.Services;
using App.Domain.Core.hw15.Transaction.AppServices;
using App.Domain.Core.hw15.Transaction.Data.Repositories;
using App.Domain.Core.hw15.Transaction.Services;
using App.Domain.Core.hw15.User.Data.Repository;
using App.Domain.Core.hw15.User.Services;
using App.Domain.Services.hw15.Card;
using App.Domain.Services.hw15.Transaction;
using App.Domain.Services.hw15.User;
using App.Infra.Data.Db.SqlServer.Ef;
using App.Infra.Data.Db.SqlServer.Ef.Dal;
using App.Infra.Data.Repos.Ef.hw15.Card;
using App.Infra.Data.Repos.Ef.hw15.Transaction;
using App.Infra.Data.Repos.Ef.hw15.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionStrings = builder.Configuration.GetConnectionString("Hw18");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStrings));

builder.Services.AddScoped<ICardServices, CardServices>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICardRepositories, CardRepository>();
builder.Services.AddScoped<ITransactionRepositories, TransactionRepository>();
builder.Services.AddScoped<ICardAppServices, CardAppServices>();
builder.Services.AddScoped<ITransactionAppServices, TransactionAppServices>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
