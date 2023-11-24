namespace Lucas_Berino_API.Models;

public class UsuarioModels
{
    public int id_usuario { get; set; }
    public string nombre_usuario { get; set; }
    public string nivel { get; set; }
    public string estado { get; set; }
    public string contrasena { get; set; }

    public int id_persona { get; set; }
}