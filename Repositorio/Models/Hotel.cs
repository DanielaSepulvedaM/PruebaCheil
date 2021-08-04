using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Repositorio.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            CalificacionCliente = new HashSet<CalificacionCliente>();
            Foto = new HashSet<Foto>();
        }

        [Key]
        public int HotelID { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Precio { get; set; }
        public bool? Eliminado { get; set; }

        [InverseProperty("Hotel")]
        public virtual ICollection<CalificacionCliente> CalificacionCliente { get; set; }
        [InverseProperty("Hotel")]
        public virtual ICollection<Foto> Foto { get; set; }
    }
}
