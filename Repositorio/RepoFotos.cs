using Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace Repositorio
{
    public class RepoFotos : IRepoFotos
    {
        private HotelContext Context;
        private readonly IHostingEnvironment hostingEnvironment;

        public RepoFotos(HotelContext ctx, IHostingEnvironment hostingEnvironment)
        {
            Context = ctx;
            this.hostingEnvironment = hostingEnvironment;
        }
        public void Insertar(Entidades.Foto foto)
        {
            string path = Path.Combine(this.hostingEnvironment.WebRootPath, "Uploads", foto.HotelID.ToString());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var nombre = DateTime.Now.ToLongTimeString().Replace(':', '_');
            string filepath = Path.Combine(path, nombre.ToString()) + ".jpg";
            foto.imagen.Save(filepath);

            var fot = new Models.Foto
            {
                HotelID = foto.HotelID,
                RutaImagen = filepath,
            };
            Context.Foto.Add(fot);
            Context.SaveChanges();
        }

        public IEnumerable<Entidades.Foto> Listar(int hotelID)
        {
            var fotos = Context.Foto.Where(f => f.HotelID == hotelID)
                               .Select(f => new Entidades.Foto
                               {
                                   FotoID = f.FotoID,
                                   HotelID = f.HotelID,
                                   RutaImagen = f.RutaImagen,
                               }).ToList();

            return fotos;
        }
        
        public void Eliminar(int fotoID)
        {
            var fotoEliminar = Context.Foto.FirstOrDefault(f => f.FotoID == fotoID);
            Context.Foto.Remove(fotoEliminar);
            Context.SaveChanges();
        }

        public Entidades.Foto Obtener(int fotoId)
        {
            var f = Context.Foto.FirstOrDefault(f => f.FotoID == fotoId);
            if (f == null)
                throw new ApplicationException("Id de foto no existe en la base de datos.");

            if (File.Exists(f.RutaImagen))
            {
                var foto = new Entidades.Foto
                {
                    imagen = (Bitmap)Image.FromFile(f.RutaImagen)
                };

                return foto;
            }
            else
                throw new ApplicationException("La imagen no existe en el sistema de archivos.");
        }
    }
}
