using Microsoft.AspNetCore.Mvc;
using ProyectoLoginLeo.models;
using System.Diagnostics;
using System.Security.Claims;
using ProyectoLoginLeo.Servicios.Contrato;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ProyectoLoginLeo.Models;
using Microsoft.AspNetCore;

namespace ProyectoLoginLeo.Controllers
{
    [Authorize] //Para que solo puedan acceder los autorizados con el login

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriaService _categoriaService;
        private readonly ITareaService _tareaService;
        int _idUsuario = 0;


        public HomeController(ILogger<HomeController> logger , ICategoriaService categoriaService, ITareaService tareaService)
        {
            _logger = logger;
            _categoriaService = categoriaService;
            _tareaService = tareaService;
        }

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreUsuario = "";
            string idUsuario = "k";
            DateTime fecha = DateTime.Now;
            string fechaSolo = fecha.ToString("yyyy-MM-dd");

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario =  claimuser.Claims.Where(c=>c.Type == ClaimTypes.Name)
                    .Select(c=>c.Value).FirstOrDefault();

                ViewData["nombreUsuario"] = nombreUsuario;

                idUsuario = claimuser.Claims.Where(c => c.Type == "idUsuario")
                    .Select(c => c.Value).FirstOrDefault();

                ViewData["idUsuario"] = idUsuario;
                _idUsuario = int.Parse(idUsuario);
            }


            ViewData["idCategoria"] =fechaSolo;

            List<Categorium> listCategorias = await _categoriaService.GetCategorias(_idUsuario);
            ListAndNListCategorium objListNList = new ListAndNListCategorium();

            //Asignamos las tareas por idCategoria para que se pueda contabilizar en el index

            foreach(Categorium categoria in listCategorias)
            {
                List<Tarea> listTarea = await _tareaService.GetTareas(categoria.IdCategoria);
                categoria.Tareas = listTarea;
            }
            
            objListNList.ListaCategorias = listCategorias;

            return View(objListNList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("IniciarSesion", "Inicio");
        }
    }
}