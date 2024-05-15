using Microsoft.EntityFrameworkCore;
using RepositoryProject.DBContext;
using RepositoryProject.Service.Interface;
using RepositoryProject.Service;
using RepositoryProject.AuoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddTransient<IStudent, StudentService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "AccessRoute",
    pattern: "{controller=Access}/{action=Login}/{id?}");

//app.MapControllerRoute(
//    name: "AreaRoute",
//    pattern: "{area}/{controller}/{action}/{id?}",
//    defaults: new { area = "Student", controller = "Student", action = "Index" });

//app.MapControllerRoute(
//    name: "Default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
