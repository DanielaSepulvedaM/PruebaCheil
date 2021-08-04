using Entidades.Interfaces;
using Repositorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio
{
    public class RepoHotel : IRepoHotel
    {
        private HotelContext Context;

        public RepoHotel(HotelContext ctx)
        {
            Context = ctx;
        }

        public void Crear(Entidades.Hotel hotel)
        {
            var hot = new Hotel
            {
                Nombre = hotel.Nombre,
                Categoria = hotel.Categoria,
                Precio = hotel.Precio,
                //Foto = hotel.Foto,       
            };
            Context.Hotel.Add(hot);
            Context.SaveChanges();
        }
        public IEnumerable<Entidades.Hotel> Listar()
        {
            return Context.Hotel.Where(hot => hot.Eliminado == false)
                                .Select(hot => new Entidades.Hotel
                                {
                                    HotelID = hot.HotelID,
                                    Nombre = hot.Nombre,
                                    Categoria = hot.Categoria,
                                    Precio = hot.Precio,
                                    //Foto = hot.Foto,
                                }).ToList();
        }
        public void Editar(int hotelID, Entidades.Hotel hotel)
        {
            var obtHotel = Context.Hotel.FirstOrDefault(hot => hot.HotelID == hotelID);
            obtHotel.Nombre = hotel.Nombre;
            obtHotel.Categoria = hotel.Categoria;
            obtHotel.Precio = hotel.Precio;
            //obtHotel.Foto = hotel.Foto;
            Context.SaveChanges();        }

        public void Eliminar(int hotelID)
        {
            var hotel = Context.Hotel.FirstOrDefault(hot => hot.HotelID == hotelID);
            if (hotel != null)
                hotel.Eliminado = true;
            Context.SaveChanges();
        }  

        public Entidades.Hotel Obtener(int hotelID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entidades.Hotel> ListarPorCalificacion(int calificacion)
        {
            var promedios = Context.CalificacionCliente
                .GroupBy(c => c.HotelID)
                .Select(h => new { HotelID = h.Key, Calificacion = Math.Round(h.Average(t => t.Calificacion)) });

            var hoteles = (from hotel in Context.Hotel
                          join califica in promedios
                          on hotel.HotelID equals califica.HotelID
                          where califica.Calificacion == calificacion
                          orderby califica.Calificacion descending
                          select new Entidades.Hotel
                          {
                              Nombre = hotel.Nombre,
                              Calificacion = califica.Calificacion,
                              Categoria = hotel.Categoria,
                              HotelID = hotel.HotelID,
                              Precio = hotel.Precio
                          }).ToList();
            return hoteles;
        }

        public IEnumerable<Entidades.Hotel> ListarPorCategoria(int categoria)
        {
            return Context.Hotel.Where(c => c.Categoria == categoria)
                                .OrderByDescending(c => c.Categoria)
                                .Select(c => new Entidades.Hotel {
                                    Nombre = c.Nombre,
                                    Categoria = c.Categoria,
                                    Precio = c.Precio,
                                    HotelID = c.HotelID,
                                }).ToList();           
        }

        public IEnumerable<Entidades.Hotel> ListarPorPrecio(bool ordenarAscendentemente)
        {
            var query = Context.Hotel.AsQueryable();
            query = ordenarAscendentemente ? query.OrderBy(vl => vl.Precio) : query.OrderByDescending(vl => vl.Precio);

            return query.Select(vl => new Entidades.Hotel
            {
                Nombre = vl.Nombre,
                Categoria = vl.Categoria,
                HotelID = vl.HotelID,
                Precio = vl.Precio
            }).ToList();
        }
    }
}
