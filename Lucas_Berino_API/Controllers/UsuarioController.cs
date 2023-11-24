using Microsoft.AspNetCore.Mvc;
using Servicios.ContactosService;

namespace Lucas_Berino_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : Controller {
    
    private UsuarioService usuarioService;

    public UsuarioController()
    {
        usuarioService = new UsuarioService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerinoP3;");
    }
    
    
    [HttpGet("{id}")]
    public IActionResult obtenerDatosUsuario(int id)
    {
        return Ok(usuarioService.obtenerDatosUsuario(id));
    }

    
    [HttpPost()]
    public IActionResult insertUsuario([FromBody] Models.UsuarioModels modelo)
    {
        usuarioService.insertUsuario(
            new Infraestructura.Modelos.UsuarioModel()
            {
                nombre_usuario = modelo.nombre_usuario,
                contrasena = modelo.contrasena,
                nivel = modelo.nivel,
                estado = modelo.estado,
                persona  = new Infraestructura.Modelos.PersonaModel()
                {
                    id_persona = modelo.id_persona
                }
            });
        return Ok("Datos insertados correctamente");
    }
    
    [HttpDelete("{id}")]
    public IActionResult ElimiUsuario(int id)
    {
        return Ok(usuarioService.ElimiUsuario(id));
    }
    
  
    [HttpPut()]
    public IActionResult EditUsuario([FromBody] Infraestructura.Modelos.UsuarioModel modelo) {
        try {
            usuarioService.EditUsuario(modelo);
        }
        catch (Exception ex) {
            return BadRequest(ex.Message);
            throw;
        }
        return Ok("se actualizo con exito");
    }
    
    [HttpGet()]
    public IActionResult obtenerUsuario()
    {
        return Ok(usuarioService.obtenerUsuario());
    }
    


}