    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyNotesApp.Models;
    using MyNotesApp.Data;
    using Microsoft.AspNetCore.Authentication.Cookies; 

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<MyNotesAppContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("MyNotesAppContext") ?? throw new InvalidOperationException("Connection string 'MyNotesAppContext' not found.")));

    builder.Services.AddControllersWithViews(); 

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
        });
    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        SeedData.Initialize(services);
    }
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
