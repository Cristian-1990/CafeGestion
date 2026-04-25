using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using CafeGestion;
using CafeGestion.Storage.StorageJson;
using Serilog;
using static System.Console;
using Spectre.Console;
using Serilog.Sinks.SystemConsole.Themes;
using CafeGestion.Factory.Repository;
using CafeGestion.Repository.Productos;
using CafeGestion.Service;
using CafeGestion.Enums;
using CafeGestion.Factory;
using CafeGestion.Factory.Productos;
using CafeGestion.Models;
using CafeGestion.Validators;
using CafeGestion.Service.Productos;

//Configuracion del ooger
var logger = new LoggerConfiguration()
 .MinimumLevel.Debug()
 .WriteTo.Console()
 .WriteTo.File("logs/cafeGestion.log")
 .CreateLogger();

Log.Logger = logger;

Title = "Gestión de Cafés";
OutputEncoding = Encoding.UTF8;

Clear(); //Limpia la consola

Main();

Log.CloseAndFlush(); // Asegura el guardado de los logs antes de cerrar la app
AnsiConsole.Markup("[darkorange dim]Pulsa cualquier tecla para finalizar...[/] \n");
ReadKey();
return;


void Main()
{
 var repository = new ProductoRepo();
 var storage = new StorageJson();
 IProductoService service = new ProductoService(repository, storage, new ValidadorCafe());

 ProductoFactory.SeedCafe().ToList().ForEach(c=> service.Guardar(c));
 
 MenuOpciones opcion;
 const string regexOpcionMenu = @"^[0-5]$";
 var contadorCafe = service.TotalProductos;
 

 
 do
 {
  MostrarMenu();
  var opcionStr = ValidarEntrada("Selecciona una opción del menú:",regexOpcionMenu,"La opción no es válida");
  var opcionVal = int.Parse(opcionStr);
  opcion = (MenuOpciones)opcionVal;

  
  switch (opcion)
  {
   case MenuOpciones.ListarCafes: ListarTodo(service); break;
   case MenuOpciones.AñadirCafe: AñadirNuevo(service); break;
   case MenuOpciones.BuscarCafe: BuscarPorId(service); break;
   case MenuOpciones.ModificarCafe: ActualizarCafe(service); break;
   case MenuOpciones.BorrarCafe: EliminarCafe(service); break;
   case MenuOpciones.Salir:   AnsiConsole.Markup($"[sandybrown]Cerrando el sistema...[/]\n"); break;
  }

  if (opcion != MenuOpciones.Salir)
  {
   AnsiConsole.Markup($"[sandybrown]Presiona cualquier tecla para cerrar.[/]\n");
   ReadKey();
  }
 }while(opcion != MenuOpciones.Salir);
}
void MostrarMenu(){
 AnsiConsole.Clear();
 AnsiConsole.WriteLine();

 var contenido =
  $"  [sandybrown]{(int)MenuOpciones.ListarCafes}.[/]  Mostrar todos los cafés 📜\n" +
  $"  [sandybrown]{(int)MenuOpciones.AñadirCafe}.[/]  Añadir un nuevo café 🫘\n" +
  $"  [sandybrown]{(int)MenuOpciones.BuscarCafe}.[/]  Buscar café por ID 🔍\n" +
  $"  [sandybrown]{(int)MenuOpciones.ModificarCafe}.[/]  Modificar café ✏️\n" +
  $"  [sandybrown]{(int)MenuOpciones.BorrarCafe}.[/]  Borrar café ❌\n" +
  $"  [sandybrown]{(int)MenuOpciones.Salir}.[/]  Salir";

 AnsiConsole.Write(new Panel(contenido)
  .Header(new PanelHeader("[darkorange dim] ☕  G E S T I Ó N  D E   C A F É ☕  [/]").Centered())
  .BorderColor(Color.Grey)
  .RoundedBorder()
  .Expand());
 
}
//--------------------------------------------------------------------------------------------------------------
void ListarTodo(IProductoService service)
{
 AnsiConsole.Markup($"[sandybrown]Listado de todos los cafés[/]\n");
 var listaCafe = service.GetAll().OfType<Cafe>().ToList();

 var table = new Table()
  .BorderColor(Color.Grey)
  .RoundedBorder()
  .Title("[darkorange dim] ☕  L I S T A D O   D E   C A F É S  ☕ [/]")
  .Expand()
  .AddColumn(new TableColumn("[darkorange dim]ID[/]").Centered())
  .AddColumn(new TableColumn("[darkorange dim]Nombre[/]"))
  .AddColumn(new TableColumn("[darkorange dim]Origen[/]"))
  .AddColumn(new TableColumn("[darkorange dim]Región[/]"))
  .AddColumn(new TableColumn("[darkorange dim]Variedad[/]"))
  .AddColumn(new TableColumn("[darkorange dim]Proceso[/]"))
  .AddColumn(new TableColumn("[darkorange dim]Puntuación[/]").Centered())
  .AddColumn(new TableColumn("[darkorange dim]Cantidad[/]").Centered())
  .AddColumn(new TableColumn("[darkorange dim]Disponible[/]").Centered())
  .AddColumn(new TableColumn("[darkorange dim]Entrada[/]").Centered())
  .AddColumn(new TableColumn("[darkorange dim]Tueste[/]").Centered());

 foreach (var cafe in listaCafe)
 {
  table.AddRow(
   $"[sandybrown]{cafe.Id}[/]",
   $"[sandybrown]{cafe.Nombre}[/]",
   $"[sandybrown]{cafe.Origen}[/]",
   $"[sandybrown]{cafe.Region}[/]",
   $"[sandybrown]{cafe.Variedad}[/]",
   $"[sandybrown]{cafe.Proceso}[/]",
   $"[sandybrown]{cafe.Puntuacion}[/]",
   $"[sandybrown]{cafe.Cantidad}[/]",
   cafe.Disponible ? "[green]✓[/]" : "[red]✗[/]",
   $"[grey]{cafe.Entrada:dd/MM/yyyy}[/]",
   $"[grey]{cafe.FechaTueste:dd/MM/yyyy}[/]"
  );
 }
 AnsiConsole.Write(table);
}

void AñadirNuevo(IProductoService service)
{
 AnsiConsole.Markup($"[Orange3]📦---AÑADIENDO NUEVO CAFE AL INVENTARIO---📦[/]\n");

 AnsiConsole.Markup($"[Orange3]---ID---[/]\n");
 var id = ReadLine();
 AnsiConsole.Markup($"[Orange3]---NOMBRE---\n[/]");
 var nombre = ReadLine();
 AnsiConsole.Markup($"[Orange3]---CANTIDAD---\n[/]");
 var cantidad = ComprobarCantidad();
 AnsiConsole.Markup($"[Orange3]---PUNTUACION---\n[/]");
 var puntuacion = ComprobarPuntuacion();
 AnsiConsole.Markup($"[Orange3]---Origen---[/]\n");
 var origen = ComprobarOrigen();
 AnsiConsole.Markup($"[Orange3]---VARIEDAD---\n[/]");
 var variedad = ComprobarVariedad();
 AnsiConsole.Markup($"[Orange3]---PROCESO---[/]\n");
 var proceso = ComprobarProceso();
 AnsiConsole.Markup($"[Orange3]---REGION---[/]\n");
 var region = ReadLine();
 AnsiConsole.Markup($"[Orange3]---NOTAS DE CATA---\n[/]");
 var notasDeCata = ReadLine();

 var cafeNuevo = new Cafe()
 {
  Id = int.Parse(id),
  Nombre = nombre,
  Cantidad = cantidad,
  Puntuacion = puntuacion,
  Origen = origen,
  Variedad = variedad,
  Proceso = proceso,
  Region = region,
  NotaDeCata = notasDeCata,
  FechaTueste = DateTime.Now

 };
 AnsiConsole.Markup($"[sandybrown]¿Confirmas que quieres guardar este café?[/]\n");
 AnsiConsole.Markup($"[sandybrown]1.[/][White]SI[/]\n");
 AnsiConsole.Markup($"[sandybrown]2.[/][White]NO[/]\n");

 var mostrarCafeNuevo = cafeNuevo.ToString();
 AnsiConsole.Markup($"[sandybrown]{mostrarCafeNuevo}[/]\n");
 var input = ReadLine()?.Trim() ?? "";
 if (input == "1")
 {
  service.Guardar(cafeNuevo);
 }
 else
 {
  AnsiConsole.Markup($"[Orange3]El café ha sido descartado...[/]");
 }
}


void BuscarPorId(IProductoService service)
{
 AnsiConsole.Markup($"[Orange3]Introduce el ID del café que quieres buscar[/]\n");
 var input = ReadLine()?.Trim() ?? "";
 if (int.TryParse(input, out int id) && id <= service.TotalProductos && id > 0 )
 {
  var cafeId = service.GetById(id);
  AnsiConsole.Markup($"[Orange3]{cafeId?.ToString()}[/]\n");
 }
 AnsiConsole.Markup($"[Orange3]El café con ID: {input}, no existe.3[/]\n");
}

void ActualizarCafe(IProductoService service)
{
 AnsiConsole.Markup($"[Orange3]Primero buscaremos el café que quieres modificar para confirmar si ya existe...[/]\n");
 BuscarPorId(service);
 AñadirNuevo(service);
}


void EliminarCafe(IProductoService service)
{
 AnsiConsole.Markup($"[Orange3]Primero comprobaremos si el producto existe.[/]\n");
 BuscarPorId(service);
 AnsiConsole.Markup($"[Orange3]Introduce el ID Que deseas borrar[/]\n");
 var input = ReadLine()?.Trim() ?? "";
 if (int.TryParse(input, out int id) && id <= service.TotalProductos)
 {
  service.Delete(id);
  AnsiConsole.Markup($"[Orange3]El producto se ha borrado correctamente[/]\n");
 }
 else
 {
  AnsiConsole.Markup($"[Red]el producto con ID:[/][White]{input}[/][Red] no existe[/]\n");
 }
}

//-------------------------------------------------------------------------------------------------------------------------
string ValidarEntrada(string prompt, string regex, string error)
{
 while (true)
 {
  AnsiConsole.Markup($"[sandybrown]{prompt}[/]\n");
  var input = ReadLine()?.Trim() ?? "";
  if(Regex.IsMatch(input, regex))return input;
  AnsiConsole.Markup($"[Red]{error}[/]\n");
 }
}



/*
 *--------------VALIDADORES DE ENTRADA DE DATOS DEL PROGRAM------------------------------
 */
int ComprobarCantidad()
 {
  while (true)
  {
   var input = ReadLine()?.Trim() ?? "";
   if (int.TryParse(input, out int cantidad) && cantidad >= 1)
    return cantidad;
   AnsiConsole.Markup($"[Red]Introduce un número válido mayor que 0[/]\n");
  }
 }

double ComprobarPuntuacion()
{
 while (true)
 {
  var input = ReadLine()?.Trim() ?? "";
  if (double.TryParse(input, out double puntuacion) && puntuacion > 7.99 && puntuacion < 10.00)
   return puntuacion;
  AnsiConsole.Markup($"[Red]Introduce un número válido mayor que 7.99 y menor que 10[/]\n");
 }
}

TipoOrigen ComprobarOrigen()
{
 var opcionInt = 0;
 while (true)
 {
  foreach (var origenes in Enum.GetValues<TipoOrigen>())
  {
   AnsiConsole.Markup($"[sandybrown]{opcionInt}. [/][white]{origenes}[/]\n");
   opcionInt++;
  }
  var input = ReadLine()?.Trim() ?? "";
  if (int.TryParse(input, out int origen))
   return (TipoOrigen)origen;
  AnsiConsole.Markup($"[Red]Origen desconocido[/]\n");
  AnsiConsole.Markup($"[Red]Introduce uno de la lista[/]\n");
  
 }
}

TipoVariedad ComprobarVariedad()
{
 var opcionInt = 0;
 while (true)
 {
  foreach (var variedades in Enum.GetValues<TipoVariedad>())
  {
   AnsiConsole.Markup($"[sandybrown]{opcionInt}. [/][white]{variedades}[/]\n");
   opcionInt++;
  }
  var input = ReadLine()?.Trim() ?? "";
  if (int.TryParse(input, out int variedad))
   return (TipoVariedad)variedad;
  AnsiConsole.Markup($"[Red]Origen desconocido[/]\n");
  AnsiConsole.Markup($"[Red]Introduce uno de la lista[/]\n");
  
 }
}

TipoProceso ComprobarProceso()
{
 var opcionInt = 0;
 while (true)
 {
  foreach (var procesos in Enum.GetValues<TipoProceso>())
  {
   AnsiConsole.Markup($"[sandybrown]{opcionInt}. [/][white]{procesos}[/]\n");
   opcionInt++;
  }
  var input = ReadLine()?.Trim() ?? "";
  if (int.TryParse(input, out int proceso))
   return (TipoProceso)proceso;
  AnsiConsole.Markup($"[Red]Origen desconocido[/]\n");
  AnsiConsole.Markup($"[Red]Introduce uno de la lista[/]\n");
  
 }
}



//====================================FIN, VALIDADORES DE ENTRADA DE DATOS DEL PROGRAM=====================================
//=========================================================================================================================