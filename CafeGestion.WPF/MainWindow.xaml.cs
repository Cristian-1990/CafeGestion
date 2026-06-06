using System.IO;
using System.Windows;
using System.Windows.Controls;
using CafeGestion.Enums;
using CafeGestion.Factory.Productos;
using CafeGestion.Models;
using CafeGestion.Repository.Productos;
using CafeGestion.Service.Productos;
using CafeGestion.Storage.StorageJson;
using CafeGestion.Storage.StorageReport;
using CafeGestion.Validators;
using CafeGestion.Config;
using CafeGestion.Repository.Productos;
namespace CafeGestion.WPF;

public partial class MainWindow : Window
{
    private readonly IProductoService _service;
    private Cafe? _cafeSeleccionado;

    public MainWindow()
    {
        Directory.CreateDirectory("Data");
        InitializeComponent();
        var repo = new ProductoRepo();
        var storage = new StorageJson();
        var validador = new ValidadorCafe();
        _service = new ProductoService(repo, storage, validador);
        if (!_service.GetAll().Any())
            ProductoFactory.SeedCafe().ToList().ForEach(c => _service.Guardar(c));
        CargarComboBoxes();
        CargarCafes();
    }

    private void CargarComboBoxes()
    {
        CmbOrigen.ItemsSource = Enum.GetValues<TipoOrigen>();
        CmbVariedad.ItemsSource = Enum.GetValues<TipoVariedad>();
        CmbProceso.ItemsSource = Enum.GetValues<TipoProceso>();
    }

    private void CargarCafes()
    {
        GridCafes.ItemsSource = _service.GetAll().ToList();
    }

    private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_service is null) return;
    
        var texto = TxtBuscar.Text.ToLower();
        if (string.IsNullOrWhiteSpace(texto) || texto == "buscar...")
        {
            CargarCafes();
            return;
        }
        GridCafes.ItemsSource = _service.GetAll()
            .OfType<Cafe>()
            .Where(c => c.Nombre.ToLower().Contains(texto) ||
                        c.Region.ToLower().Contains(texto) ||
                        c.Origen.ToString().ToLower().Contains(texto))
            .ToList();
    }
    private void GridCafes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (GridCafes.SelectedItem is not Cafe cafe) return;
        _cafeSeleccionado = cafe;
        TxtNombre.Text = cafe.Nombre;
        TxtRegion.Text = cafe.Region;
        CmbOrigen.SelectedItem = cafe.Origen;
        CmbVariedad.SelectedItem = cafe.Variedad;
        CmbProceso.SelectedItem = cafe.Proceso;
        TxtPuntuacion.Text = cafe.Puntuacion.ToString();
        TxtCantidad.Text = cafe.Cantidad.ToString();
        TxtNotaCata.Text = cafe.NotaDeCata;
    }

    private void BtnNuevo_Click(object sender, RoutedEventArgs e)
    {
        _cafeSeleccionado = null;
        TxtNombre.Text = "";
        TxtRegion.Text = "";
        CmbOrigen.SelectedIndex = 0;
        CmbVariedad.SelectedIndex = 0;
        CmbProceso.SelectedIndex = 0;
        TxtPuntuacion.Text = "";
        TxtCantidad.Text = "";
        TxtNotaCata.Text = "";
        GridCafes.SelectedItem = null;
    }

    private void BtnGuardar_Click(object sender, RoutedEventArgs e)
    {
        if (!double.TryParse(TxtPuntuacion.Text, out var puntuacion) ||
            !int.TryParse(TxtCantidad.Text, out var cantidad))
        {
            MessageBox.Show("Puntuacion y Cantidad deben ser numeros validos.",
                "Error de validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var origen = (TipoOrigen)(CmbOrigen.SelectedItem ?? TipoOrigen.Desconocido);
            var variedad = (TipoVariedad)(CmbVariedad.SelectedItem ?? TipoVariedad.Desconocido);
            var proceso = (TipoProceso)(CmbProceso.SelectedItem ?? TipoProceso.Desconocido);

            if (_cafeSeleccionado is null)
            {
                var nuevoCafe = new Cafe
                {
                    Nombre = TxtNombre.Text,
                    Region = TxtRegion.Text,
                    Puntuacion = puntuacion,
                    Cantidad = cantidad,
                    NotaDeCata = TxtNotaCata.Text,
                    Origen = origen,
                    Variedad = variedad,
                    Proceso = proceso,
                    FechaTueste = DateTime.Now
                };
                _service.Guardar(nuevoCafe);
            }
            else
            {
                var cafeActualizado = _cafeSeleccionado with
                {
                    Nombre = TxtNombre.Text,
                    Region = TxtRegion.Text,
                    Puntuacion = puntuacion,
                    Cantidad = cantidad,
                    NotaDeCata = TxtNotaCata.Text,
                    Origen = origen,
                    Variedad = variedad,
                    Proceso = proceso
                };
                _service.Actualizar(_cafeSeleccionado.Id, cafeActualizado);
            }

            CargarCafes();
            MessageBox.Show("Cafe guardado correctamente.", "OK",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error de validacion",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void BtnEliminar_Click(object sender, RoutedEventArgs e)
    {
        if (_cafeSeleccionado is null) return;
        _service.Delete(_cafeSeleccionado.Id);
        CargarCafes();
    }

    private void BtnFicha_Click(object sender, RoutedEventArgs e)
    {
        if (_cafeSeleccionado is null)
        {
            MessageBox.Show("Selecciona un cafe primero.", "Aviso",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        try
        {
            var report = new StorageReport();
            report.GuardarHtml(_cafeSeleccionado, Configuracion.CafeHtml(_cafeSeleccionado.Id));
            report.GuardarPdf(_cafeSeleccionado, Configuracion.CafePdf(_cafeSeleccionado.Id));
            MessageBox.Show($"Ficha generada:\n{Configuracion.CafePdf(_cafeSeleccionado.Id)}", "OK",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnImportar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var total = _service.ImportarDatos();
            CargarCafes();
            MessageBox.Show($"Importados {total} cafes.", "Importar",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnExportar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var total = _service.ExportarDatos();
            MessageBox.Show($"Exportados {total} cafes a JSON.", "Exportar",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BtnAcercaDe_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("CafeGestion v1.0\nDesarrollado por Cristian\nhttps://github.com/Cristian-1990/CafeGestion",
            "Acerca de", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}