using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Interfaces
{
    public interface IRepoFotos
    {
        IEnumerable<Foto> Listar(int hotelID);
        Foto Obtener(int fotoId);
        void Insertar(Foto foto);
        void Eliminar(int FotoID);
    }
}
