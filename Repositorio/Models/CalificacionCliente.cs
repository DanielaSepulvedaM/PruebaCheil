using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Repositorio.Models
{
    public partial class CalificacionCliente
    {
        [Key]
        public int CalificacionID { get; set; }
        public int Calificacion { get; set; }
        [Required]
        [StringLength(300)]
        public string Comentario { get; set; }
        public int HotelID { get; set; }

        [ForeignKey(nameof(HotelID))]
        [InverseProperty("CalificacionCliente")]
        public virtual Hotel Hotel { get; set; }
    }
}
