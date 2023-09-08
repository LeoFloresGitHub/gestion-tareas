using ProyectoLoginLeo.models;
using ProyectoLoginLeo.Servicios.Contrato;
using Microsoft.EntityFrameworkCore;

namespace ProyectoLoginLeo.Servicios.Implementacion
{
    public class CategoriaService : ICategoriaService
    {

        private readonly MisistemapruebaContext _dbContext;

        public CategoriaService(MisistemapruebaContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task DeleteCategoria(int id)
        {
            try
            {
                var cat = await _dbContext.Categoria.FindAsync(id);
                if (cat == null)
                {
                    return;
                }
                List<Tarea> listTarea = await _dbContext.Tareas.Where(
                c => c.IdCategoria == id).ToListAsync();

                foreach (Tarea tarea in listTarea)
                {
                    _dbContext.Tareas.Remove(tarea);
                }

                _dbContext.Categoria.Remove(cat);
                
                await _dbContext.SaveChangesAsync();

                return;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<Categorium> GetCategoria(int idUsuario, int idCategoria)
        {
            Categorium objCategoria = await _dbContext.Categoria.Where(
                c => c.IdUsuario == idUsuario && c.IdCategoria == idCategoria).FirstOrDefaultAsync();

            return objCategoria;

            throw new NotImplementedException();
        }

        public async Task<List<Categorium>> GetCategorias(int idUsuario)
        {
            List<Categorium> listCategorias = await _dbContext.Categoria.Where(
                c => c.IdUsuario == idUsuario).ToListAsync();
            return listCategorias;
            throw new NotImplementedException();
        }

        public async Task SaveCategoria(Categorium modelo)
        {
            _dbContext.Categoria.Add(modelo); //Agregamos con el metodo add
            await _dbContext.SaveChangesAsync(); //Guardamos de manera asyncrona
            //throw new NotImplementedException();
        }

        public Task UpdateCategoria(Categorium modelo)
        {
            throw new NotImplementedException();
        }
    }
}
