using Infraestructura.Datos;
using Infraestructura.Modelos;


namespace Servicios.ContactosService;

public class PersonaService 
{
    
    PersonaDatos personaD;
    
    public PersonaService(string cadenaConexion) 
    {
        personaD = new PersonaDatos(cadenaConexion);
    }
    
    public PersonaModel ElimPersona(int id) 
    {
        return personaD.ElimPersona(id);
    }
    
    public PersonaModel obtenerDatosPersona(int id)
    {
        return personaD.obtenerDatosPersona(id);
    }
    
    public void insertPersona(PersonaModel persona)
    {
        personaD.insertPersona(persona);
    }
    
    public void EditPersona(PersonaModel persona) 
    {
        personaD.EditPersona(persona);
    } 
    
    public List<PersonaModel> obtenerPersonas()
    {
        return personaD.obtenerPersonas();
    }
    
}