using Microsoft.EntityFrameworkCore;
using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Servicios.Contrato;

namespace ProyectoLoginLeo.Servicios.Implementacion
{
    public class TareaService : ITareaService
    {
        private readonly MisistemapruebaContext _dbContext;

        public TareaService(MisistemapruebaContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task DeleteTarea(int id)
        {
            try
            {
                var tarea = await _dbContext.Tareas.FindAsync(id);
                if (tarea == null)
                {
                    return;
                }

                _dbContext.Tareas.Remove(tarea);
                await _dbContext.SaveChangesAsync();

                return;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<List<Tarea>> GetTareas(int idCategoria)
        {
            List<Tarea> tareas = new List<Tarea>();
            tareas = await _dbContext.Tareas.Where(t=>t.IdCategoria == idCategoria).ToListAsync();
            //throw new NotImplementedException();
            return tareas;
        }

        public async Task<Tarea> GetTarea(int id)
        {
            Tarea tarea = new Tarea();
            tarea = await _dbContext.Tareas.Where(t => t.IdTarea == id).FirstOrDefaultAsync();
            //throw new NotImplementedException();
            return tarea;
            //throw new NotImplementedException();
        }

        public  async Task SaveTarea(Tarea tarea)
        {
            if (tarea == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                _dbContext.Tareas.Add(tarea);
                await _dbContext.SaveChangesAsync();
                return;

            }

            //throw new NotImplementedException();
        }

        public async Task UpdateTarea(Tarea tarea)
        {
            if (tarea == null)
            {
                throw new ArgumentNullException(nameof(tarea), "La tarea no puede ser null");
            }
            else
            {
                _dbContext.Tareas.Update(tarea);
                await _dbContext.SaveChangesAsync();
                

            }

            
        }
    }
}
