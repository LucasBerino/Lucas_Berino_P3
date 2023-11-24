using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Lucas_Berino_API.Controllers;

[ApiController]
[Route("[controller]")]

public class CiudadController : ControllerBase {
    
    private const string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerinoP3;";
    private CiudadService servicio;

    public CiudadController() {
        servicio = new CiudadService(connectionString);
    }

    [HttpGet("por-parametro")]
    public IActionResult obtenerDatosCiudad([FromQuery] int id) {
        var ciudad = servicio.obtenerDatosCiudad(id);
        return Ok(ciudad);
    }
    
    [HttpPost]
    public IActionResult insertCiudad([FromBody] Infraestructura.Modelos.CiudadModel ciudad) {
        servicio.insertCiudad(ciudad);
        return Created("Se creo con exito", ciudad);
    }
    
    [HttpDelete("{id}")]
    public IActionResult ElimCiudad(int id)
    {
        return Ok(servicio.ElimCiudad(id));
    }
    
    [HttpPut]
    public IActionResult EditCiudad([FromBody] Infraestructura.Modelos.CiudadModel ciudad) {
        try {
            servicio.EditCiudad(ciudad);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("Actualizado");
    }

    
    [HttpGet]
    public IActionResult obtenerCiudades()
    {
        return Ok(servicio.obtenerCiudades());
    }

}