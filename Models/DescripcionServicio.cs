using System;
using System.Collections.Generic;

namespace sistema_servicio_tecnico.Models
{
    public partial class DescripcionServicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int ServicioId { get; set; }

        // Relación con Servicio
        public virtual Servicio Servicio { get; set; } = null!;
    }
}
