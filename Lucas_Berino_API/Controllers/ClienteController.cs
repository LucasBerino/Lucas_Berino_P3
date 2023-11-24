using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;


namespace Lucas_Berino_API.Controllers;
[ApiController]
[Route("[controller]")]

public class ClienteController : Controller
{
    private ClienteService clienteServicio;
    
    public ClienteController()
    {
        clienteServicio = new ClienteService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerinoP3;");
    }
    
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(clienteServicio.obtenerDatosCliente(id));
    }
    
    /*
    [HttpPost]
    public IActionResult insertCliente([FromBody] Models.ClienteModels modelo)
    {
        clienteServicio.insertCliente(
            new Infraestructura.Modelos.ClienteModel()
            {
                fecha_ingreso = modelo.fecha_ingreso,
                calificacion = modelo.calificacion,
                estado = modelo.estado,
                persona  = new Infraestructura.Modelos.PersonaModel()
                {
                    id_persona = modelo.id_persona
                }
            });
        return Ok("Datos insertados correctamente");
    }*/    

    [HttpPut]
    public IActionResult editCliente([FromBody] Infraestructura.Modelos.ClienteModel modelo) {
        try {
            clienteServicio.editCliente(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("Actualizado");
    }

    
    [HttpDelete("{id}")]
    public IActionResult ElimCliente(int id)
    {
        return Ok(clienteServicio.ElimCliente(id));
    }
    
    [HttpGet]
    public IActionResult obtenerClientes()
    {
        return Ok(clienteServicio.obtenerClientes());
    }
}