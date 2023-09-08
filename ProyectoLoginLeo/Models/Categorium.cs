using System;
using System.Collections.Generic;

namespace ProyectoLoginLeo.models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }


    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual List<Tarea> Tareas { get; set; } = new List<Tarea>();
}
