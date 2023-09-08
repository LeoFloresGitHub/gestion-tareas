using System;
using System.Collections.Generic;

namespace ProyectoLoginLeo.models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();
}
