using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        public double Calificacion { get; set; }
    }
}
