using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class MovimientoService {
    MovimientoDatos movimientoDatos;

    public MovimientoService(string cadenaConexion) {
        movimientoDatos = new MovimientoDatos(cadenaConexion);
    }
    
    public void insertMovimiento(MovimientoModel movimiento) {
        movimientoDatos.insertMovimiento(movimiento);
    }
    
    public MovimientoModel obtenerDatosMovimiento(int id) {
        return movimientoDatos.obtenerDatosMovimiento(id);
    }
    
    public void EditMovimiento(MovimientoModel movimiento) {
        movimientoDatos.EditMovimiento(movimiento);
    } 
    
    public MovimientoModel ElimMovimiento(int id) {
        return movimientoDatos.ElimMovimiento(id);
    }
}