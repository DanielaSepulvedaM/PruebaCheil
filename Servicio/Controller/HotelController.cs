using Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IRepoHotel repositorio;

        public HotelController(IRepoHotel repo)
        {
            repositorio = repo;
        }

        /// <summary>
        /// Listado de Hoteles sin filtros
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Listar()
        {
            var hoteles = repositorio.Listar();
            return Ok(hoteles);
        }

        /// <summary>
        /// Listado de Hoteles filtrados por calificacion
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpGet("calificacion/{calificacion:int}")]
        public IActionResult Listar(int calificacion)
        {
            var hoteles = repositorio.ListarPorCalificacion(calificacion);
            return Ok(hoteles);
        }

        /// <summary>
        /// Listado de Hoteles filtrados por categoria
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpGet("categoria/{categoria:int}")]
        public IActionResult ListarPorCategoria(int categoria)
        {
            var hoteles = repositorio.ListarPorCategoria(categoria);
            return Ok(hoteles);
        }

        /// <summary>
        /// Listado de Hoteles ordenados ascedente y descendentemente
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpGet("precio/{orden:regex(^(asc|desc)$)}")]
        public IActionResult ListarPorPrecio(string orden)
        {
            var hoteles = repositorio.ListarPorPrecio(orden == "asc");
            return Ok(hoteles);
        }

        /// <summary>
        /// Metodo para crear un hotel
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Crear(Hotel hotel)
        {
            if(ModelState.IsValid)
            {
                repositorio.Crear(hotel);
                return Ok(hotel);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para editar un hotel
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpPut("{hotelID:int}")]
        public IActionResult Editar(int hotelID, Hotel hotel)
        {
            repositorio.Editar(hotelID, hotel);
            return Ok();
        }

        /// <summary>
        /// Elimina un hotel
        /// </summary>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        [HttpDelete("{hotelID:int}")]
        public IActionResult Eliminar(int hotelID)
        {
            repositorio.Eliminar(hotelID);
            return Ok();
        }
    }
}
