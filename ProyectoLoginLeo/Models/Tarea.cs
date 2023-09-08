using System;
using System.Collections.Generic;

namespace ProyectoLoginLeo.models;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public int IdCategoria { get; set; }

    public string Titulo { get; set; }

    public string Descripcion { get; set; }

    public DateTime Fven { get; set; }

    public int Prioridad { get; set; }

    public int Estado { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
