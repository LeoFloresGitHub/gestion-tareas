using Microsoft.AspNetCore.Mvc;
using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Recursos;
using ProyectoLoginLeo.Servicios.Contrato;
using ProyectoLoginLeo.Servicios.Implementacion;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace ProyectoLoginLeo.Controllers
{
    public class InicioController : Controller
    {

        private readonly IUsuarioService _usuarioService;

        public InicioController(IUsuarioService usuarioService) //El param es una referencia a la interfaz de nuesto servicio
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Registrarse() //Este get solo devuelve la vista
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo) //Este post atiende las solicitudes post
        {
            modelo.Clave = Utilidad.EncriptarClave(modelo.Clave);  //Encryptamos la clave en forma Hash256
            Usuario usuario_creado = await _usuarioService.SaveUsuario(modelo);

            if (usuario_creado != null) //tmbn podriamos comprobar si usuario_creado.Id es >0
            {
                return RedirectToAction("IniciarSesion", "Inicio"); //El IniciarSesion de este controller y este controller es Inicio xD
            }
            else
                ViewData["Message"] = "No se pudo crear el usuario";
            return View();


          
        }

        public IActionResult IniciarSesion()
        {
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else 

                return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuario_encontrado = await _usuarioService.GetUsuario(correo, Utilidad.EncriptarClave(clave));

            if(usuario_encontrado == null)
            {
                ViewData["Message"] = "No se encontraron coincidencias";
                return View();
            }

            //Creamos lista Claim(reclamaciones) que tendra solo un elemento
            List<Claim> claims = new List<Claim>() 
            {
                new Claim (ClaimTypes.Name, usuario_encontrado.NombreUsuario),
                new Claim ("idUsuario",usuario_encontrado.IdUsuario.ToString())
            };

            //creamos la identidad ClaimsIdentity que representa al usuario encontrado y se le asgina la lista de claims
            //y se usa un esquema de autentificacion

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Se eñaden las propiedeades que tendraa la autentificacion como en este caso, refrescar
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            //Se llama a este metodo para la verificacion de todo lo anterior  desde la lista de claims
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties);

            return RedirectToAction("Index", "Home");
        }
    }
}
