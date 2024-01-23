using ASP.NETScholl.Models;
using ASP.NETScholl.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection")));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:AzureDb"]));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SomeeDbConnection")));
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<GradeService>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opts => {
  opts.Password.RequireDigit = false;                     //nevyžaduje číslici
  opts.Password.RequireLowercase = false;                 //nevyžaduje malé písmeno
  opts.Password.RequireUppercase = true;                  //vyžaduje velké písmeno   
  opts.Password.RequireNonAlphanumeric = false;           // nevyžaduje alfanumerický znak
  opts.Password.RequiredLength = 8;                       //délka musí být 8 znaků
});
builder.Services.ConfigureApplicationCookie(options => {                 //nastavuje cookies
  options.Cookie.Name = ".AspNetCore.Identity.Application";
  options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
  options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
