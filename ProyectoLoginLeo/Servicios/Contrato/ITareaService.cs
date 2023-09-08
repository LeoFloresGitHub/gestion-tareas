
using ProyectoLoginLeo.models;

namespace ProyectoLoginLeo.Servicios.Contrato
{
    public interface ITareaService
    {
        public Task<List<Tarea>> GetTareas(int idCategoria);
        public Task<Tarea> GetTarea(int id);
        public Task SaveTarea(Tarea tarea); //Void
        public Task UpdateTarea(Tarea tarea); //Void
        public Task DeleteTarea(int id); //Void


    }
}
