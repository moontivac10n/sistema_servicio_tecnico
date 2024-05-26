﻿using System;
using System.Collections.Generic;

namespace sistema_servicio_tecnico.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<Servicio> Servicios { get; } = new List<Servicio>();
}
