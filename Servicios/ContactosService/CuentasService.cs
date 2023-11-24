using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class CuentasService 
{
    
    CuentasDatos cuentasD;
    
    public CuentasService(string cadenaConexion) 
    {
        cuentasD = new CuentasDatos(cadenaConexion);
    }
    
    public CuentasModel ElimCliente(int id) 
    {
        return cuentasD.ElimCliente(id);
    }
    
    public CuentasModel obtenerDatosCuenta(int id) 
    {
        return cuentasD.obtenerDatosCuenta(id);
    }
    
    public void insertCuentas(CuentasModel cuentas)
    {
        cuentasD.insertCuentas(cuentas);
    }
    
    public void EditCuenta(CuentasModel cuentas) 
    {
        cuentasD.EditCuenta(cuentas);
    } 
    
    public List<CuentasModel> obtenerasCuentas()
    {
        return cuentasD.obtenerasCuentas();
    }
    
}