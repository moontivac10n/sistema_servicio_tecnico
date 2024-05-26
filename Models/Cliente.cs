using System;
using System.Collections.Generic;

namespace sistema_servicio_tecnico.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<RecepcionEquipo> RecepcionEquipos { get; } = new List<RecepcionEquipo>();
}
