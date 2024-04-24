using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication().AddCookie("MyCookieAuth",option =>
{
    option.Cookie.Name = "MyCookieAuth";
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization(option =>

{
option.AddPolicy("AdminOnly",
    policy => policy.RequireClaim("Admin"));

option.AddPolicy("MustBelongToHRDepartment",
policy => policy.RequireClaim("Department", "HR"));

option.AddPolicy("HRManagerOnly", policy => policy
    .RequireClaim("Department", "HR")
    .RequireClaim("Manager"));


}

) ;
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
