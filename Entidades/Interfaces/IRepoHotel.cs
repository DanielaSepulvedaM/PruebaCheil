using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Interfaces
{
    public interface IRepoHotel
    {
        IEnumerable<Hotel> Listar();
        void Crear(Hotel hotel);
        void Eliminar(int hotelID);
        void Editar(int hotelID, Hotel hotel);
        Hotel Obtener(int hotelID);
        IEnumerable<Hotel> ListarPorCalificacion(int calificacion);
        IEnumerable<Hotel> ListarPorCategoria(int categoria);
        IEnumerable<Hotel> ListarPorPrecio(bool orden);



    }
}
