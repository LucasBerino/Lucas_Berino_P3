using System.Data;
using Infraestructura.Conexiones;
namespace Infraestructura.Datos;
using Infraestructura.Modelos;
public class PersonaDatos 
{
    private ConexionDB conexion;
    public PersonaDatos(string cadenaConexion) 
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public PersonaModel obtenerDatosPersona(int id)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"select ci.*, pe.* " +
                                               $"from ciudad ci " +
                                               $"inner join persona pe on pe.id_ciudad = ci.id_ciudad " +
                                               $"where pe.id_persona = '{id}'", conn);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            return new PersonaModel()
            {
                id_persona = reader.GetInt32("id_persona"),
                nombre = reader.GetString("nombre"),
                apellido = reader.GetString("apellido"),
                nro_documento = reader.GetString("nro_documento"),
                direccion = reader.GetString("direccion"),
                email = reader.GetString("email"),
                celular = reader.GetString("celular"),
                estado = reader.GetString("estado"),
                ciudad = new CiudadModel()
                {
                    id_ciudad = reader.GetInt32("id_ciudad"),
                    ciudad = reader.GetString("ciudad"),
                    departamento = reader.GetString("departamento"),
                    postal_code = reader.GetInt32("postal_code"),
                }
            };
        }
        return null;
    }
    
    public void insertPersona(PersonaModel persona)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("insert into persona( id_ciudad,nombre, apellido, nro_documento,direccion,celular,email,estado)" +
                                               "values(@id_ciudad, @nombre, @apellido, @nro_documento, @direccion,@celular,@email,@estado)", conn);
        comando.Parameters.AddWithValue("id_ciudad", persona.ciudad.id_ciudad);
        comando.Parameters.AddWithValue("nombre", persona.nombre);
        comando.Parameters.AddWithValue("nro_documento", persona.nro_documento);
        comando.Parameters.AddWithValue("celular", persona.celular);
        comando.Parameters.AddWithValue("direccion", persona.direccion);
        comando.Parameters.AddWithValue("apellido", persona.apellido);
        comando.Parameters.AddWithValue("email", persona.email);
        comando.Parameters.AddWithValue("estado", persona.estado);

        comando.ExecuteNonQuery();
    }
    
    public void EditPersona(PersonaModel persona)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"update persona set id_ciudad = '{persona.ciudad.id_ciudad}', " +
                                               $"nombre = '{persona.nombre}', " +
                                               $"nro_documento = '{persona.nro_documento}', " +
                                               $"celular = '{persona.celular}', " +
                                               $"email = '{persona.email}', " +
                                               $"apellido = '{persona.apellido}', " +
                                               $"direccion = '{persona.direccion}', " +
                                               $"estado = '{persona.estado}' " +
                                               $" where id_persona = {persona.id_persona}", conn);
        comando.ExecuteNonQuery();
    }
    
    public PersonaModel ElimPersona(int id) 
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"delete from persona where id_persona = {id}", conn);
        using var reader = comando.ExecuteReader();
        return null;
    }
    
    public List<PersonaModel> obtenerPersonas()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"select ci.*, pe.* " +
                                               $"from ciudad ci " +
                                               $"inner join persona pe on pe.id_ciudad = ci.id_ciudad ", conn);
        List<PersonaModel> personas = new List<PersonaModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            personas.Add(new PersonaModel()
            {
                id_persona = reader.GetInt32("id_persona"),
                nombre = reader.GetString("nombre"),
                nro_documento = reader.GetString("nro_documento"),
                apellido = reader.GetString("apellido"),
                direccion = reader.GetString("direccion"),
                email = reader.GetString("email"),
                estado = reader.GetString("estado"),
                celular = reader.GetString("celular"),
                ciudad = new CiudadModel()
                {
                    id_ciudad = reader.GetInt32("id_ciudad"),
                    departamento = reader.GetString("departamento"),
                    ciudad = reader.GetString("ciudad"),
                    postal_code = reader.GetInt32("postal_code"),                }
            });
        }
    
        return personas;
    }
    
}