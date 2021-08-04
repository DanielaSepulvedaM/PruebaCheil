﻿using Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotoController : ControllerBase
    {
        private readonly IRepoFotos repositorio;

        public FotoController(IRepoFotos repo)
        {
            repositorio = repo;
        }

        [HttpGet("listar/{hotelID:int}")]
        public IActionResult Listar(int hotelID)
        {
            var fotos = repositorio.Listar(hotelID);
            return Ok(fotos);
        }

        [HttpGet("{fotoID:int}")]
        public IActionResult ObtenerFoto(int fotoId) {
            var foto = repositorio.Obtener(fotoId);
            var outputStream = new MemoryStream();
            foto.imagen.Save(outputStream, ImageFormat.Jpeg);
            outputStream.Seek(0, SeekOrigin.Begin);
            return File(outputStream, "image/jpeg");
        }

        [HttpDelete("{fotoID:int}")]
        public IActionResult Eliminar(int fotoID)
        {
            repositorio.Eliminar(fotoID);
            return Ok();
        }

        [HttpPost("{hotelId:int}")]
        public IActionResult Insertar(int hotelId, IFormFile foto)
        {
            var imagen = (Bitmap)Image.FromStream(foto.OpenReadStream());
            var f = new Entidades.Foto
            {
                HotelID = hotelId,
                imagen = imagen
            };
            repositorio.Insertar(f);
            return Ok();
        }
    }
}
