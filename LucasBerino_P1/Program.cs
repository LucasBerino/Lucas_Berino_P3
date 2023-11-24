// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Infraestructura.Modelos;
using Servicios.ContactosService;


CiudadService ciudadService = new CiudadService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerino_P1;");

ciudadService.insertCiudad(new Infraestructura.Modelos.CiudadModel {
    ciudad = "Capiata",
    departamento = "Central",
    postal_code = 8759
});

var EnseCiudad = ciudadService.obtenerDatosCiudad(2);
Console.WriteLine($"Ciudad: {EnseCiudad.ciudad} departamento: {EnseCiudad.departamento}");

var EditCiudad = ciudadService.obtenerDatosCiudad(1);
EditCiudad.ciudad = "Lambare";
EditCiudad.departamento = "Central";
EditCiudad.postal_code = 852;
ciudadService.EditCiudad(EditCiudad);

ciudadService.ElimCiudad(3);

PersonaService personaService = new PersonaService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerino_P1;");

personaService.insertPersona(new Infraestructura.Modelos.PersonaModel() {
    id_ciudad = 2,
    nombre = "Lucas Berino",
    apellido = "Berino",
    nro_documento = "5769778",
    direccion = "Brasilia casi General Santos",
    celular = "0991879910",
    email = "lucasberino@gmail.com",
    estado = "acti",
});

var EnsePersona = personaService.obtenerDatosPersona(1);
Console.WriteLine($"Nombre: {EnsePersona.nombre} Apellido: {EnsePersona.apellido} ");

var EditPersona  = personaService.obtenerDatosPersona(2);
EditPersona.id_ciudad = 6;
EditPersona.nombre = "Axel";
EditPersona.apellido = "Perez";
EditPersona.nro_documento = "7841549694";
EditPersona.direccion = "Carmelita";
EditPersona.celular = "02598755546";
EditPersona.email = "axelperez@gmail.com";
EditPersona.estado = "acti";
personaService.EditPersona(EditPersona);

personaService.ElimPersona(3);

ClienteService clienteService = new ClienteService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerino_P1;");

clienteService.insertCliente(new Infraestructura.Modelos.ClienteModel() {
    id_persona = 4,
    fecha_ingreso = DateTime.Now,
    calificacion = "8",
    estado = "acti",
});

var EnseCliente = clienteService.obtenerDatosCliente(6);
Console.WriteLine($"Fecha ingreso: {EnseCliente.fecha_ingreso} Estado: {EnseCliente.estado}");

var EditCliente = clienteService.obtenerDatosCliente(1);
EditCliente.id_persona = 4;
EditCliente.fecha_ingreso = DateTime.Now;;
EditCliente.calificacion = "5";
EditCliente.estado = "acti";
clienteService.editCliente(EditCliente);

clienteService.ElimCliente(2);



CuentasService cuentaService = new CuentasService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerino_P1;");

cuentaService.insertCuentas(new Infraestructura.Modelos.CuentasModel() {
    id_cliente = 6,
    nro_cuenta = "867",
    fecha_alta = DateTime.Now,
    tipo_cuenta = "Cuenta correinte",
    estado = "acti",
    saldo = 254862,
    nro_contrato = "289",
    costo_mantenimiento = 1000000,
    promedio_acreditacion = "2",
    moneda = "Guaranies",
});

var EnseCuenta = cuentaService.obtenerDatosCuenta(1);
Console.WriteLine($"Numero cuenta: {EnseCuenta.nro_cuenta}  Saldo: {EnseCuenta.saldo} Fecha Alta: {EnseCuenta.fecha_alta}Tipo cuenta: {EnseCuenta.tipo_cuenta} Nro. contrato: {EnseCuenta.nro_contrato}");

var EditCuenta = cuentaService.obtenerDatosCuenta(1);
EditCuenta.id_cliente = 6;
EditCuenta.nro_cuenta = "8578";
EditCuenta.fecha_alta = DateTime.Now;;
EditCuenta.tipo_cuenta = "caja de ahorro";
EditCuenta.estado = "acti";
EditCuenta.saldo = 297;
EditCuenta.nro_contrato = "951";
EditCuenta.costo_mantenimiento = 20;
EditCuenta.promedio_acreditacion = "2";
EditCuenta.moneda = "Euro";
cuentaService.EditCuenta(EditCuenta);

cuentaService.ElimCliente(3);

MovimientoService movimientoService = new MovimientoService("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=LucasBerino_P1;");

movimientoService.insertMovimiento(new Infraestructura.Modelos.MovimientoModel() {
    id_cuentas = 6,
    fecha_movimiento = DateTime.Now,
    tipo_movimiento = "Deposito",
    saldo_anterior = 20000,
    saldo_actual = 90000,
    monto_movimiento = 70000,
    cuenta_origen = 7845256,
    cuenta_destino = 7845256,
    canal_decimal = "2.845",
});

var EnseMovimiento = movimientoService.obtenerDatosMovimiento(2);
Console.WriteLine($"FechaMovimiento: {EnseMovimiento.fecha_movimiento} Saldo: {EnseMovimiento.saldo_actual}  Cuenta destino: {EnseMovimiento.cuenta_destino} Montom movimiento: {EnseMovimiento.monto_movimiento}");

var EditMovimiento = movimientoService.obtenerDatosMovimiento(2);
EditMovimiento.id_cuentas = 1;
EditMovimiento.fecha_movimiento = DateTime.Now;
EditMovimiento.tipo_movimiento = "Transferencia";
EditMovimiento.saldo_anterior = 600000;
EditMovimiento.saldo_actual = 300000;
EditMovimiento.monto_movimiento = 300000;
EditMovimiento.cuenta_origen = 25254852;
EditMovimiento.cuenta_destino = 24245896;
EditMovimiento.canal_decimal = "5.2";
movimientoService.EditMovimiento(EditMovimiento);


movimientoService.ElimMovimiento(3);
