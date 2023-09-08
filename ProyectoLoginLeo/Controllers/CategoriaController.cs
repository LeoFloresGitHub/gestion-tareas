using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Models;
using ProyectoLoginLeo.Servicios.Contrato;
using System.Security.Claims;

namespace ProyectoLoginLeo.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        int _idUsuario = 0;

        public CategoriaController(ICategoriaService categoriaService) //El param es una referencia a la interfaz de nuesto servicio
        {
            _categoriaService = categoriaService;

        }
        public void cargarClaims()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            int idUsuario = int.Parse(claimuser.Claims.Where(c => c.Type == "idUsuario")
                    .Select(c => c.Value).FirstOrDefault());
            _idUsuario = idUsuario;
        }

        public IActionResult MostrarCategorias()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreUsuario = "";
            string idUsuario = "k";
            DateTime fecha = DateTime.Now;
            string fechaSolo = fecha.ToString("yyyy-MM-dd");

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).FirstOrDefault();

                ViewData["nombreUsuario"] = nombreUsuario;

                idUsuario = claimuser.Claims.Where(c => c.Type == "idUsuario")
                    .Select(c => c.Value).FirstOrDefault();

                ViewData["idUsuario"] = idUsuario;
            }


            ViewData["fechaHoy"] = fechaSolo;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirCategorias(ListAndNListCategorium modelo)
        {
            cargarClaims();
            modelo.NuevoCategorium.IdUsuario = _idUsuario;
            await _categoriaService.SaveCategoria(modelo.NuevoCategorium);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> EliminarCategoria(int idCategoria)
        {
       
            await _categoriaService.DeleteCategoria(idCategoria);


            return RedirectToAction("Index", "Home");
        }
    }
}
