using System;
using System.Collections.Generic;

namespace sistema_servicio_tecnico.Models;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Precio { get; set; }

    public string? Sku { get; set; }

    public int Estado { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<DescripcionServicio> DescripcionServicios { get; } = new List<DescripcionServicio>();

    public virtual ICollection<RecepcionEquipo> RecepcionEquipos { get; } = new List<RecepcionEquipo>();

    public virtual Usuario Usuario { get; set; } = null!;
}
