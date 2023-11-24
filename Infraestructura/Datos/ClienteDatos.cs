using System.Data;
using Infraestructura.Conexiones;
namespace Infraestructura.Datos;
using Infraestructura.Modelos;

public class ClienteDatos 
{
    
    private ConexionDB conexion;
    
    public ClienteDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public ClienteModel obtenerDatosCliente(int id) {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"select pe.*, cl.*, ci.* " +
                                               $"from persona pe " +
                                               $"inner join cliente cl on pe.id_persona = cl.id_persona " +
                                               $"inner join ciudad ci on pe.id_ciudad = ci.id_ciudad " +
                                               $"where cl.id_cliente = {id}", conn);
        using var reader = comando.ExecuteReader();
  if (reader.Read())
        {
                return new ClienteModel()
                {
                    id_cliente = reader.GetInt32("id_cliente"),
                    fecha_ingreso = reader.GetDateTime("fecha_ingreso"),
                    estado = reader.GetString("estado"),
                    calificacion = reader.GetString("calificacion"),
                    persona = new PersonaModel()
                    {
                        id_persona = reader.GetInt32("id_persona"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        email = reader.GetString("email"),
                        direccion = reader.GetString("direccion"),
                        estado = reader.GetString("estado"),
                        celular = reader.GetString("celular"),
                        nro_documento = reader.GetString("nro_documento"),
                        ciudad = new CiudadModel()
                        {
                            id_ciudad = reader.GetInt32("id_ciudad"),
                            ciudad = reader.GetString("ciudad"),
                            postal_code = reader.GetInt32("postal_code"),
                            departamento = reader.GetString("departamento"),
                        }                        
                    },
                };
        }
        return null;
    }
    
    public void insertCliente(ClienteModel cliente) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("insert into cliente( id_persona, fecha_ingreso, calificacion,estado)" +
                                               "values(@id_persona, @fecha_ingreso, @calificacion, @estado)", conn);
        comando.Parameters.AddWithValue("id_persona", cliente.persona.id_persona);
        comando.Parameters.AddWithValue("estado", cliente.estado);
        comando.Parameters.AddWithValue("calificacion", cliente.calificacion);
        comando.Parameters.AddWithValue("fecha_ingreso", cliente.fecha_ingreso);

        comando.ExecuteNonQuery();
    }
    
    public void editCliente(ClienteModel cliente)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"update cliente set id_persona = '{cliente.persona.id_persona}', " +
                                               $"fecha_ingreso = '{cliente.fecha_ingreso}', " +
                                               $"estado = '{cliente.estado}' " +
                                               $"calificacion = '{cliente.calificacion}', " +
                                               $" where id_cliente = {cliente.id_cliente}", conn);
        comando.ExecuteNonQuery();
    }
    
    
    public List<ClienteModel> obtenerClientes()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"select pe.*, cl.*, ci.* " +
                                               $"from persona pe " +
                                               $"inner join  cliente cl on pe.id_persona = cl.id_persona " +
                                               $"inner join ciudad ci on pe.id_ciudad = ci.id_ciudad ", conn);
        List<ClienteModel> personas = new List<ClienteModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            personas.Add(new ClienteModel()
            {
                id_cliente = reader.GetInt32("id_cliente"),
                calificacion = reader.GetString("calificacion"),
                fecha_ingreso = reader.GetDateTime("fecha_ingreso"),
                estado = reader.GetString("estado"),
                persona = new PersonaModel()
                {
                    id_persona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    direccion = reader.GetString("direccion"),
                    estado = reader.GetString("estado"),
                    email = reader.GetString("email"),
                    nro_documento = reader.GetString("nro_documento"),
                    celular = reader.GetString("celular"),
                    apellido = reader.GetString("apellido"),
                    ciudad = new CiudadModel()
                    {
                        id_ciudad = reader.GetInt32("id_ciudad"),
                        departamento = reader.GetString("departamento"),
                        ciudad = reader.GetString("ciudad"),
                        postal_code = reader.GetInt32("postal_code"),
                    }                        
                },
            });
        }
    
        return personas;
    }
    
    public ClienteModel ElimCliente(int id) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"delete from cliente where id_cliente = {id}", conn);
        using var reader = comando.ExecuteReader();
        return null;
    }
}