using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos;

public class MovimientoDatos 
{
    
    private ConexionDB conexion;
    
    public MovimientoDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public void insertMovimiento(MovimientoModel movimiento)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("insert into movimiento( id_cuentas,fecha_movimiento, tipo_movimiento, saldo_anterior ,saldo_actual,monto_movimiento,cuenta_origen,cuenta_destino,canal_decimal)" +
                                               "values(@id_cuentas, @fecha_movimiento, @tipo_movimiento, @saldo_anterior,@saldo_actual, @monto_movimiento,@cuenta_origen,@cuenta_destino,@canal_decimal)", conn);
        comando.Parameters.AddWithValue("id_cuentas", movimiento.id_cuentas);
        comando.Parameters.AddWithValue("fecha_movimiento", movimiento.fecha_movimiento);
        comando.Parameters.AddWithValue("tipo_movimiento", movimiento.tipo_movimiento);
        comando.Parameters.AddWithValue("saldo_anterior", movimiento.saldo_anterior);
        comando.Parameters.AddWithValue("saldo_actual", movimiento.saldo_actual);
        comando.Parameters.AddWithValue("monto_movimiento", movimiento.monto_movimiento);
        comando.Parameters.AddWithValue("cuenta_origen", movimiento.cuenta_origen);
        comando.Parameters.AddWithValue("cuenta_destino", movimiento.cuenta_destino);
        comando.Parameters.AddWithValue("canal_decimal", movimiento.canal_decimal);

        comando.ExecuteNonQuery();
    }
    
    public MovimientoModel obtenerDatosMovimiento(int id) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"Select * from movimiento where id_movimiento = {id}", conn);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            return new MovimientoModel()
            {
                id_movimiento = reader.GetInt32("id_movimiento"),
                id_cuentas = reader.GetInt32("id_cuentas"),
                fecha_movimiento = reader.GetDateTime("fecha_movimiento"),
                tipo_movimiento = reader.GetString("tipo_movimiento"),
                saldo_anterior = reader.GetInt32("saldo_anterior"),
                saldo_actual = reader.GetInt32("saldo_actual"),
                monto_movimiento = reader.GetInt32("monto_movimiento"),
                cuenta_origen = reader.GetInt32("cuenta_origen"),
                cuenta_destino = reader.GetInt32("cuenta_destino"),
                canal_decimal = reader.GetString("canal_decimal"),
            };
        }
        return null;
    }
    
    public void EditMovimiento(MovimientoModel movimiento) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"update movimiento set id_cuentas = '{movimiento.id_cuentas}', " +
                                               $"fecha_movimiento = '{movimiento.fecha_movimiento}', " +
                                               $"tipo_movimiento = '{movimiento.tipo_movimiento}', " +
                                               $"saldo_anterior = '{movimiento.saldo_anterior}', " +
                                               $"saldo_actual = '{movimiento.saldo_actual}', " +
                                               $"monto_movimiento = '{movimiento.monto_movimiento}', " +
                                               $"cuenta_origen = '{movimiento.cuenta_origen}', " +
                                               $"cuenta_destino = '{movimiento.cuenta_destino}', " +
                                               $"canal_decimal = '{movimiento.canal_decimal}' " +
                                               $" where id_movimiento = {movimiento.id_movimiento}", conn);
        comando.ExecuteNonQuery();
    }
    
    public MovimientoModel ElimMovimiento(int id) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"delete from movimiento where id_movimiento = {id}", conn);
        using var reader = comando.ExecuteReader();
        return null;
    }
    
}