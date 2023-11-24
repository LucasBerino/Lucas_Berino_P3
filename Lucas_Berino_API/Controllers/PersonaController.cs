using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Lucas_Berino_API.Controllers;
[ApiController]
[Route("[controller]")]
public class PersonaController : Controller {
    
    private PersonaService personaServicio;

    public PersonaController()
    {
        personaServicio = new PersonaService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerinoP3;");
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(personaServicio.obtenerDatosPersona(id));
    }
    
    
    [HttpPost]
    public IActionResult insertPersona([FromBody] Models.PersonaModels modelo)
    {
        personaServicio.insertPersona(
            new Infraestructura.Modelos.PersonaModel
            {
                nombre = modelo.nombre,
                apellido = modelo.apellido,
                nro_documento = modelo.nro_documento,
                direccion = modelo.direccion,
                email = modelo.email,
                celular = modelo.celular,
                estado = modelo.estado,
                ciudad  = new Infraestructura.Modelos.CiudadModel
                {
                    id_ciudad = modelo.id_ciudad
                }
            });
        return Ok("Datos insertados correctamente");
    } 
    
    
    [HttpPut]
    public IActionResult EditPersona([FromBody] Infraestructura.Modelos.PersonaModel modelo) {
        try {
            personaServicio.EditPersona(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("Actualizado");
    }
    
    [HttpDelete("{id}")]
    public IActionResult ElimPersona(int id)
    {
        return Ok(personaServicio.ElimPersona(id));
    }
    
    [HttpGet]
    public IActionResult obtenerPersonas()
    {
        return Ok(personaServicio.obtenerPersonas());
    }
}