using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Hotel
    {
       
        public int HotelID { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del hotel")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Elegir la categoria 1 a 5 estrellas")]
        public int Categoria { get; set; }

        [Required(ErrorMessage = "Elegir el precio del hotel")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "agregar fotos")]
        public string Foto { get; set; }

        public double Calificacion { get; set; }
    }
}
