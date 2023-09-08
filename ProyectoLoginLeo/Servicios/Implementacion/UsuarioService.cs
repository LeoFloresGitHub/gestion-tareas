using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Servicios.Contrato;
using Microsoft.EntityFrameworkCore;

namespace ProyectoLoginLeo.Servicios.Implementacion

{
    public class UsuarioService : IUsuarioService
    {
        private readonly MisistemapruebaContext _dbContext;

        public UsuarioService(MisistemapruebaContext dbContext){
            _dbContext = dbContext;

        }


    public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
                .FirstOrDefaultAsync(); //FirstOrDefaultAsync() devuelve el primer registro enconrado y si no encuentre devuelve un nullo, de forma asincrona
            return usuario_encontrado;
               

            //throw new NotImplementedException();
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo); //Agregamos con el metodo add
            await _dbContext.SaveChangesAsync(); //Guardamos de manera asyncrona
            return modelo;
           // throw new NotImplementedException();
        }
    }
}
