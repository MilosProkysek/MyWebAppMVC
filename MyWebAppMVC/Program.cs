using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebAppMVC.Data;
using MyWebAppMVC.Models;
using MyWebAppMVC.Repository;
using MyWebAppMVC.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Department
builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
builder.Services.AddScoped<IGenericService<Department>, GenericService<Department>>();

// Employee
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();

// Category & Supplier — needed for Product dropdowns
builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<IGenericService<Category>, GenericService<Category>>();
builder.Services.AddScoped<IGenericRepository<Supplier>, GenericRepository<Supplier>>();
builder.Services.AddScoped<IGenericService<Supplier>, GenericService<Supplier>>();

// Product — specialized repository & service
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IProductServiceDTO, ProductServiceDTO>();
builder.Services.AddScoped<IProductServiceDTO, ProductServiceAutomapper>();

//builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ProductProfile>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
