using Infraestructura.Conexiones;
using System.Data;
namespace Infraestructura.Datos;
using Infraestructura.Modelos;

public class CuentasDatos 
{
    
    private ConexionDB conexion;

    public CuentasDatos(string cadenaConexion) 
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    
    public CuentasModel obtenerDatosCuenta(int id) {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"select cl.*, cu.* " +
                                               $"from cliente cl " +
                                               $"inner join cuentas cu on cl.id_cliente = cl.id_cliente " +
                                               $"where cu.id_cuentas = '{id}'", conn);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            return new CuentasModel() {
                id_cuentas = reader.GetInt32("id_cuentas"),
                nro_cuenta = reader.GetString("nro_cuenta"),
                fecha_alta = reader.GetDateTime("fecha_alta"),
                tipo_cuenta = reader.GetString("tipo_cuenta"),
                estado = reader.GetString("estado"),
                saldo = reader.GetInt32("saldo"),
                nro_contrato = reader.GetString("nro_contrato"),
                costo_mantenimiento = reader.GetInt32("costo_mantenimiento"),
                promedio_acreditacion = reader.GetString("promedio_acreditacion"),
                moneda = reader.GetString("moneda"),
                cliente = new ClienteModel()
                {
                    id_cliente = reader.GetInt32("id_cliente"),
                    fecha_ingreso = reader.GetDateTime("fecha_ingreso"),
                    calificacion = reader.GetString("calificacion"),
                    estado = reader.GetString("estado"),
                }
            };
        }
        return null;
    }
    
    public void insertCuentas(CuentasModel cuentas)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("insert into cuentas( id_cliente,nro_cuenta, fecha_alta, tipo_cuenta ,estado,saldo,nro_contrato,costo_mantenimiento,promedio_acreditacion,moneda)" +
                                               "values(@id_cliente, @nro_cuenta, @fecha_alta, @tipo_cuenta,@estado, @saldo,@nro_contrato,@costo_mantenimiento,@promedio_acreditacion,@moneda)", conn);
        comando.Parameters.AddWithValue("id_cliente", cuentas.cliente.id_cliente);
        comando.Parameters.AddWithValue("fecha_alta", cuentas.fecha_alta);
        comando.Parameters.AddWithValue("tipo_cuenta", cuentas.tipo_cuenta);
        comando.Parameters.AddWithValue("saldo", cuentas.saldo);
        comando.Parameters.AddWithValue("nro_contrato", cuentas.nro_contrato);
        comando.Parameters.AddWithValue("promedio_acreditacion", cuentas.promedio_acreditacion);
        comando.Parameters.AddWithValue("costo_mantenimiento", cuentas.costo_mantenimiento);
        comando.Parameters.AddWithValue("nro_cuenta", cuentas.nro_cuenta);
        comando.Parameters.AddWithValue("moneda", cuentas.moneda);
        comando.Parameters.AddWithValue("estado", cuentas.estado);

        comando.ExecuteNonQuery();
    }
    
    public CuentasModel ElimCliente(int id)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"delete from cuentas where id_cuentas = {id}", conn);
        using var reader = comando.ExecuteReader();
        return null;
    }
    
    public List<CuentasModel> obtenerasCuentas()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"select cl.*, cu.* " +
                                               $"from cliente cl " +
                                               $"inner join cuentas cu on cl.id_cliente = cl.id_cliente", conn);
        List<CuentasModel> cuentas = new List<CuentasModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            cuentas.Add(new CuentasModel()
            {
                id_cuentas = reader.GetInt32("id_cuentas"),
                nro_cuenta = reader.GetString("nro_cuenta"),
                saldo = reader.GetInt32("saldo"),
                tipo_cuenta = reader.GetString("tipo_cuenta"),
                nro_contrato = reader.GetString("nro_contrato"),
                costo_mantenimiento = reader.GetInt32("costo_mantenimiento"),
                promedio_acreditacion = reader.GetString("promedio_acreditacion"),
                moneda = reader.GetString("moneda"),
                fecha_alta = reader.GetDateTime("fecha_alta"),
                estado = reader.GetString("estado"),
                cliente = new ClienteModel()
                {
                    id_cliente = reader.GetInt32("id_cliente"),
                    estado = reader.GetString("estado"),
                    calificacion = reader.GetString("calificacion"),
                    fecha_ingreso = reader.GetDateTime("fecha_ingreso"),
                }
            });
        }
    
        return cuentas;
    }
    
    public void EditCuenta(CuentasModel cuentas) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"update cuentas set id_cliente = '{cuentas.cliente.id_cliente}', " +
                                               $"nro_cuenta = '{cuentas.nro_cuenta}', " +
                                               $"tipo_cuenta = '{cuentas.tipo_cuenta}', " +
                                               $"saldo = '{cuentas.saldo}', " +
                                               $"nro_contrato = '{cuentas.nro_contrato}', " +
                                               $"costo_mantenimiento = '{cuentas.costo_mantenimiento}', " +
                                               $"fecha_alta = '{cuentas.fecha_alta}', " +
                                               $"promedio_acreditacion = '{cuentas.promedio_acreditacion}', " +
                                               $"moneda = '{cuentas.moneda}' " +
                                               $"estado = '{cuentas.estado}', " +
                                               $" where id_cuentas = {cuentas.id_cuentas}", conn);
        comando.ExecuteNonQuery();
    }
    
}

