using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using ProyectoLoginLeo.Servicios.Contrato;
using System.Security.Claims;
using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoLoginLeo.Controllers
{

    [Authorize] //Para que solo puedan acceder los autorizados con el login
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;
        private readonly ICategoriaService _categoriaService;
        int _idUsuario = 0;
        

        public TareaController(ITareaService tareaService, ICategoriaService caregoriaService) //El param es una referencia a la interfaz de nuesto servicio
        {
            _tareaService = tareaService;
            _categoriaService = caregoriaService;
            

        }
        public void cargarClaims()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            int idUsuario = int.Parse(claimuser.Claims.Where(c => c.Type == "idUsuario")
                    .Select(c => c.Value).FirstOrDefault());
            _idUsuario = idUsuario;

        }

       

        [HttpGet]
        public async Task<IActionResult> GestionarTareas(int idCategoria)
        {

            cargarClaims();

            Categorium objCategoria = await _categoriaService.GetCategoria(_idUsuario,idCategoria);

            if(objCategoria == null)
            {
                return View("PaginaNoEncontrada");
            } 
            
            ListAndNListTarea objListAndN = new ListAndNListTarea();
            
           objListAndN.ListaTareas = await _tareaService.GetTareas(idCategoria);

            DateTime currentDate = DateTime.Today;
            foreach (Tarea tarea in objListAndN.ListaTareas)
            {
                if (tarea.Fven < currentDate)
                {
                    tarea.Estado = 5;
                    await _tareaService.UpdateTarea(tarea);
                }
            }

            ViewData["idCategoria"] = idCategoria;
            objListAndN.NoETarea = new Tarea();
            return View(objListAndN);
        }

        [HttpGet]
        public  IActionResult NuevaTarea(int idCategoria)
        {
            cargarClaims();
            Tarea objTarea= new Tarea();
            
            objTarea.IdCategoria = idCategoria;
            return View(objTarea);
        }

        [HttpPost]
        public async Task<IActionResult> GrabarTarea(Tarea tarea)
        {
            
            await _tareaService.SaveTarea(tarea);            
            return RedirectToAction("GestionarTareas", "Tarea", new { idCategoria = tarea.IdCategoria });

        }

        public async Task<IActionResult> EliminarTarea(int idTarea, int idCategoria)
        {
            await _tareaService.DeleteTarea(idTarea);
            return RedirectToAction("GestionarTareas", "Tarea", new { idCategoria = idCategoria });
        }

        
        public async Task<IActionResult> ActualizarTarea(Tarea tarea)
        {
            cargarClaims();
             await _tareaService.UpdateTarea(tarea);
            return RedirectToAction("GestionarTareas", "Tarea", new { idCategoria = tarea.IdCategoria });

        }


    }
}
