﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace Entidades
{
    public class Foto
    {
        public int FotoID { get; set; }
        public string RutaImagen { get; set; }
        public int HotelID { get; set; }
        public Image imagen { get; set; }
    }
}
