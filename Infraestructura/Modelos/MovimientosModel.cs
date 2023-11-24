namespace Infraestructura.Modelos;

public class MovimientoModel 
{
    public int id_movimiento { get; set; }
    public int id_cuentas { get; set; }
    public DateTime fecha_movimiento { get; set; }
    public int monto_movimiento { get; set; }
    public string tipo_movimiento { get; set; }
    public int saldo_anterior { get; set; }
    public int saldo_actual { get; set; }
    public int cuenta_origen { get; set; }
    public int cuenta_destino { get; set; }
    public string canal_decimal { get; set; }
}