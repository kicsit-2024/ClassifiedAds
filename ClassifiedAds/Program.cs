using ClassifiedAds.Data;
using ClassifiedAds.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
var connectionString = "Server=.;Database=ClassifiedAdsDb;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=true";// builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
builder.Services.AddScoped<CategoriesHandler>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = "CookiesAndJwt";
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddPolicyScheme("CookiesAndJwt", "Cookies or JWT", options =>
//    {
//        options.ForwardDefaultSelector = context => context.Request.Cookies.ContainsKey("AuthV1")
//            ? JwtBearerDefaults.AuthenticationScheme
//            :
//            // Otherwise use cookie authentication
//            CookieAuthenticationDefaults.AuthenticationScheme;
//    })
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Auth/Login";
//        options.AccessDeniedPath = "/Auth/AccessDenied";
//    })
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };

//        // Read the token from the cookie
//        options.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                context.Token = context.HttpContext.Request.Cookies["AuthV1"];
//                return Task.CompletedTask;
//            }
//        };
//    });

builder.Services.AddAuthorization();
builder.Services.AddAuthorization();


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

#if !DEBUG
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
#endif

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();