
using ProyectoLoginLeo.models;


namespace ProyectoLoginLeo.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario>GetUsuario(String correo,String clave);

        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
