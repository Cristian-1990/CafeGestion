using CafeGestion.Exceptions.Productos;
using CafeGestion.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CafeGestion.Storage.StorageReport;

/// <summary>
/// Genera fichas de café en HTML y PDF.
/// </summary>
public partial class StorageReport : IStorageReport
{
    /// <inheritdoc/>
    public void GuardarHtml(Cafe cafe, string path)
    {
        try
        {
            var html = $@"
<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""UTF-8"">
    <title>Ficha de Café - {cafe.Nombre}</title>
    <style>
        body {{ font-family: 'Segoe UI', sans-serif; padding: 40px; background: #f8f4f0; }}
        .card {{ background: white; border-radius: 12px; padding: 30px; max-width: 600px; margin: auto; box-shadow: 0 4px 12px rgba(0,0,0,0.1); }}
        h1 {{ color: #6f4e37; border-bottom: 2px solid #6f4e37; padding-bottom: 10px; }}
        .field {{ margin: 12px 0; }}
        .label {{ font-weight: bold; color: #6f4e37; }}
        .value {{ color: #333; }}
        .footer {{ text-align: center; color: #999; margin-top: 20px; font-size: 0.8em; }}
    </style>
</head>
<body>
    <div class=""card"">
        <h1>☕ {cafe.Nombre}</h1>
        <div class=""field""><span class=""label"">ID: </span><span class=""value"">{cafe.Id}</span></div>
        <div class=""field""><span class=""label"">Origen: </span><span class=""value"">{cafe.Origen}</span></div>
        <div class=""field""><span class=""label"">Región: </span><span class=""value"">{cafe.Region}</span></div>
        <div class=""field""><span class=""label"">Variedad: </span><span class=""value"">{cafe.Variedad}</span></div>
        <div class=""field""><span class=""label"">Proceso: </span><span class=""value"">{cafe.Proceso}</span></div>
        <div class=""field""><span class=""label"">Puntuación: </span><span class=""value"">{cafe.Puntuacion}</span></div>
        <div class=""field""><span class=""label"">Cantidad: </span><span class=""value"">{cafe.Cantidad} kg</span></div>
        <div class=""field""><span class=""label"">Disponible: </span><span class=""value"">{(cafe.Disponible ? "Sí" : "No")}</span></div>
        <div class=""field""><span class=""label"">Notas de cata: </span><span class=""value"">{cafe.NotaDeCata}</span></div>
        <div class=""field""><span class=""label"">Fecha de tueste: </span><span class=""value"">{cafe.FechaTueste:dd/MM/yyyy}</span></div>
        <div class=""field""><span class=""label"">Fecha de entrada: </span><span class=""value"">{cafe.Entrada:dd/MM/yyyy}</span></div>
        <div class=""footer"">Generado el {DateTime.Now:dd/MM/yyyy HH:mm}</div>
    </div>
</body>
</html>";

            if (!Directory.Exists(Path.GetDirectoryName(path)!))
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            File.WriteAllText(path, html);
        }
        catch (Exception ex) { throw new ProductosException.StorageError(ex.Message); }
    }

    /// <inheritdoc/>
    public void GuardarPdf(Cafe cafe, string path)
    {
        try
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"☕ Ficha de Café — {cafe.Nombre}")
                            .FontSize(22).Bold().FontColor("#6f4e37");

                        col.Item().PaddingVertical(10).LineHorizontal(1).LineColor("#6f4e37");

                        col.Item().Text($"ID: {cafe.Id}");
                        col.Item().Text($"Origen: {cafe.Origen}");
                        col.Item().Text($"Región: {cafe.Region}");
                        col.Item().Text($"Variedad: {cafe.Variedad}");
                        col.Item().Text($"Proceso: {cafe.Proceso}");
                        col.Item().Text($"Puntuación: {cafe.Puntuacion}");
                        col.Item().Text($"Cantidad: {cafe.Cantidad} kg");
                        col.Item().Text($"Disponible: {(cafe.Disponible ? "Sí" : "No")}");
                        col.Item().Text($"Notas de cata: {cafe.NotaDeCata}");
                        col.Item().Text($"Fecha de tueste: {cafe.FechaTueste:dd/MM/yyyy}");
                        col.Item().Text($"Fecha de entrada: {cafe.Entrada:dd/MM/yyyy}");
                        col.Item().PaddingVertical(10).LineHorizontal(1).LineColor("#6f4e37");
                        col.Item().Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(9).FontColor(Colors.Grey.Medium);
                    });
                });
            }).GeneratePdf(path);
        }
        catch (Exception ex) { throw new ProductosException.StorageError(ex.Message); }
    }
}