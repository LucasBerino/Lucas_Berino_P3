using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class ClienteService 
{
    
    ClienteDatos clienteD;

    public ClienteService(string cadenaConexion)
    {
        clienteD = new ClienteDatos(cadenaConexion);
    }
    
    public ClienteModel obtenerDatosCliente(int id) 
    {
        return clienteD.obtenerDatosCliente(id);
    }
    
    public ClienteModel ElimCliente(int id) 
    {
        return clienteD.ElimCliente(id);
    }

    public void editCliente(ClienteModel cliente) 
    {
        clienteD.editCliente(cliente);
    } 
    
    public List<ClienteModel> obtenerClientes()
    {
        return clienteD.obtenerClientes();
    }
    
       
    public void insertCliente(ClienteModel cliente)
    {
        clienteD.insertCliente(cliente);
    }
}