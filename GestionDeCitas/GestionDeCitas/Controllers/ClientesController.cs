using GestionDeCitasBLL.Dtos;
using GestionDeCitasBLL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeCitas.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClientesServicio _svc;

        public ClientesController(IClientesServicio svc) => _svc = svc;

        public async Task<IActionResult> Index()
        {
            var r = await _svc.ObtenerAsync();
            return View(r.Data ?? new List<ClienteDto>());
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_Form", new ClienteDto());

        [HttpPost]
        public async Task<IActionResult> Create(ClienteDto dto)
        {
            var r = await _svc.AgregarAsync(dto);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Cliente registrado correctamente");
        }

        [HttpGet]
        public IActionResult Edit(int id, string identificacion, string nombre, int edad)
            => PartialView("_Form", new ClienteDto { Id = id, Identificacion = identificacion, Nombre = nombre, Edad = edad });

        [HttpPost]
        public async Task<IActionResult> Edit(ClienteDto dto)
        {
            var r = await _svc.ActualizarAsync(dto);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Cliente actualizado correctamente");
        }

        [HttpGet]
        public IActionResult Delete(int id) => PartialView("_Delete", id);

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _svc.EliminarAsync(id);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Cliente eliminado");
        }
    }
}
