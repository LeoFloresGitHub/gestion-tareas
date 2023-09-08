using Microsoft.EntityFrameworkCore;
using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Servicios.Contrato;
using ProyectoLoginLeo.Servicios.Implementacion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc; //Oara ek cache

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//Agregamos para que se pueda usar el dataContext en todo el poryecto

builder.Services.AddDbContext<MisistemapruebaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));

});



//Agregamos para que se pueda usar los servicios en todo el poryecto

builder.Services.AddScoped<IUsuarioService,UsuarioService>();



builder.Services.AddScoped<ITareaService, TareaService>();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();

//Agregamos para que se pueda usar la autentificacion por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); //Para que expire en 20 minutos
    });

builder.Services.AddControllersWithViews(options =>
{ options.Filters.Add(
     new ResponseCacheAttribute 
     {
         NoStore = true,
         Location = ResponseCacheLocation.None,
     }
 
     );

});

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

//Agregamos para la autentificacion por cookies para poder utilizar ese recurso
app.UseAuthentication();



app.UseAuthorization();

//Aqui podemos configurar la pagina por la cual inicia el proy

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
