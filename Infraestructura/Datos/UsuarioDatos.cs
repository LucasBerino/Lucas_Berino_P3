using Infraestructura.Modelos;
using Infraestructura.Conexiones;
using System.Data;

namespace Infraestructura.Datos;

public class UsuarioDatos
{
    private ConexionDB conexion;

    public UsuarioDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }
    
    public List<UsuarioModel> obtenerUsuario()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT p.*, u.* " +
                                               $"FROM persona p " +
                                               $"INNER JOIN usuario u ON p.id_persona = u.id_persona ", conn);
        List<UsuarioModel> usuario = new List<UsuarioModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            usuario.Add(new UsuarioModel()
            {
                id_usuario = reader.GetInt32("id_usuario"),
                nombre_usuario = reader.GetString("nombre_usuario"),
                contrasena = reader.GetString("contrasena"),
                nivel = reader.GetString("nivel"),
                estado = reader.GetString("estado"),
                persona = new PersonaModel()
                {
                    id_persona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    nro_documento = reader.GetString("nro_documento"),
                    direccion = reader.GetString("direccion"),
                    email = reader.GetString("email"),          
                    celular = reader.GetString("celular"),          
                    estado = reader.GetString("estado"),
                }
            });
        }
    
        return usuario;
    }
    
    public UsuarioModel obtenerUser(string  username)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand(
            $"SELECT u.*, p.* FROM usuario u " +
            $"INNER JOIN persona p ON u.id_persona = p.id_persona " +
            $"WHERE u.nombre_usuario = '{username}'", conn);

        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            return new UsuarioModel
            {
                id_usuario = reader.GetInt32("id_usuario"),
                id_persona = reader.GetInt32("id_persona"),
                nombre_usuario = reader.GetString("nombre_usuario"),
                nivel = reader.GetString("nivel"),
                estado = reader.GetString("estado"),
                contrasena = reader.GetString("contrasena"),
                persona = new PersonaModel()
                {
                    id_persona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    nro_documento = reader.GetString("nro_documento"),
                    direccion = reader.GetString("direccion"),
                    email = reader.GetString("email")
                }
            };
        }
        return null;
    }
    
    public void insertUsuario(UsuarioModel usuario)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("INSERT INTO public.usuario (nombre_usuario, contrasena, nivel, estado, id_persona) " +
                                               "VALUES (@nombre_usuario, @contrasena, @nivel, @estado, @id_persona)", conn);
        comando.Parameters.AddWithValue("nombre_usuario", usuario.nombre_usuario);
        comando.Parameters.AddWithValue("contrasena", usuario.contrasena);
        comando.Parameters.AddWithValue("nivel", usuario.nivel);
        comando.Parameters.AddWithValue("estado", usuario.estado);
        comando.Parameters.AddWithValue("id_persona", usuario.persona.id_persona);

        comando.ExecuteNonQuery();
    }
    
      public UsuarioModel obtenerDatosUsuario(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"SELECT p.*, u.* " +
                                                   $"FROM persona p " +
                                                   $"INNER JOIN usuario u ON p.id_persona = u.id_persona " +
                                                   $"WHERE u.id_usuario = {id}", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel()
                {
                    id_usuario = reader.GetInt32("id_usuario"),
                    nombre_usuario = reader.GetString("nombre_usuario"),
                    contrasena = reader.GetString("contrasena"),
                    nivel = reader.GetString("nivel"),
                    estado = reader.GetString("estado"),
                    persona = new PersonaModel()
                    {
                        id_persona = reader.GetInt32("id_persona"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        nro_documento = reader.GetString("nro_documento"),
                        direccion = reader.GetString("direccion"),    
                    }
                };
            }
            return null;
        }
     
      public void EditUsuario(UsuarioModel usuario) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"UPDATE persona " +
                                                 $"SET nombre_usuario = '{usuario.nombre_usuario}', " +
                                                 $"contrasena = '{usuario.contrasena}', " +
                                                 $"nivel = '{usuario.nivel}', " +
                                                 $"estado = '{usuario.estado}', " + 
                                                 $"id_persona = {usuario.persona.id_persona} " +
                                                 $"WHERE id_usuario = {usuario.id_usuario}", conn);

          comando.ExecuteNonQuery();
      }
      
      public UsuarioModel ElimiUsuario(int id) {
          var conn = conexion.GetConexion();
          var comando = new Npgsql.NpgsqlCommand($"DELETE FROM usuario WHERE id_usuario = {id}", conn);
          using var reader = comando.ExecuteReader();
          return null ;
      }
}