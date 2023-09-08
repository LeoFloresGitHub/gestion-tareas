using ProyectoLoginLeo.models;

namespace ProyectoLoginLeo.Servicios.Contrato

{
    public interface ICategoriaService
    {
        public Task <List<Categorium>> GetCategorias(int idUsuario);
        public Task <Categorium> GetCategoria(int idUsuario, int idCategoria);
        public Task SaveCategoria(Categorium modelo); //Void
        public Task UpdateCategoria(Categorium modelo); //Void
        public Task DeleteCategoria(int id); //Void

    }
}
