using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Repositorio.Models
{
    public partial class Foto
    {
        [Key]
        public int FotoID { get; set; }
        [Required]
        [StringLength(150)]
        public string RutaImagen { get; set; }
        public int HotelID { get; set; }

        [ForeignKey(nameof(HotelID))]
        [InverseProperty("Foto")]
        public virtual Hotel Hotel { get; set; }
    }
}
