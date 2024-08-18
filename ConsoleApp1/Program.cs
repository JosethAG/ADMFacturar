using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.IO;

var orderNumber = "0997";
var invoiceNumber = "00100001010000000221";
var clientName = "TIENDA LA MODA .";
var clientPerson = "ANTONY  MENDEZ VENEGAS";
var clientId = "114090745";
var clientEmail = "tiendalamodasiquirres@yahoo.com";
var clientPhone = "87502196";
var paymentCondition = "CREDITO 45 DIAS";
var sellerName = "LUIS MOLINA";
var transport = "";

var products = new List<(int Cantidad, string Codigo, string Descripcion, string Descuento, string Precio, string Total)>
{
    (2, "05960-46281", "PANTALON DAMA POPSUGAR", "0%", "₡11,900.00", "₡23,800.00"),
    (2, "10901-49388", "PANTALON DAMA MOST WANTED", "0%", "₡11,500.00", "₡23,000.00"),
    // Agrega más productos si es necesario
};

// Datos adicionales para la nueva sección
var subtotal = "₡215,800.00";
var descuento = "₡0.00";
var iva = "₡28,054.00";
var total = "₡243,854.00";

var document = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Margin(20);

        page.Content().Column(column =>
        {
            // Columna derecha - Fecha
            column.Item().PaddingTop(20).AlignRight().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(40);
                    columns.ConstantColumn(60);
                    columns.ConstantColumn(50);
                });

                table.Cell().Border(1).AlignCenter().Text("Día").FontSize(10).Bold();
                table.Cell().Border(1).AlignCenter().Text("Mes").FontSize(10).Bold();
                table.Cell().Border(1).AlignCenter().Text("Año").FontSize(10).Bold();

                table.Cell().Border(1).AlignCenter().Text("19").FontSize(10);
                table.Cell().Border(1).AlignCenter().Text("julio").FontSize(10);
                table.Cell().Border(1).AlignCenter().Text("2024").FontSize(10);
            });

            // Información del negocio y la imagen
            column.Item().PaddingTop(20).Row(row =>
            {
                // Información del negocio
                row.RelativeItem().Column(innerColumn =>
                {
                    innerColumn.Item().Text("MANUEL ANTONIO DELGADO AGUERO").FontSize(16).Bold();
                    innerColumn.Item().Text("Cédula Jurídica: 204620779").FontSize(12);
                    innerColumn.Item().Text("Teléfono: 61767777").FontSize(12);
                    innerColumn.Item().Text("Correo: manueldelgadocr@hotmail.com").FontSize(12);
                    innerColumn.Item().Text("Dirección: Alajuela, Alajuela, La Trinidad").FontSize(12);
                });

                // Imagen
                row.ConstantItem(100).AlignMiddle().Column(innerColumn =>
                {
                    var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Users\joset\Source\Repos\JosethAG\ADMFacturar\ConsoleApp1\IMPORTACIONES_MDA.png");
                    if (File.Exists(imagePath))
                    {
                        innerColumn.Item().Image(imagePath); // Ajusta la imagen
                    }
                    else
                    {
                        innerColumn.Item().Text("Imagen no encontrada").FontSize(12).Italic().FontColor(Colors.Red.Medium);
                    }
                });
            });

            // Barra de pedido y número de factura
            column.Item().PaddingTop(20).Background(Colors.Blue.Medium).Padding(5).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text($"N° FACTURA: {invoiceNumber}")
                    .FontSize(14).Bold().FontColor(Colors.White);
            });

            // Cuadro de información del cliente y vendedor
            column.Item().Padding(10).Row(infoRow =>
            {
                // Información del cliente
                infoRow.RelativeItem().Column(clientColumn =>
                {
                    clientColumn.Item().Text($"Cliente: {clientName}").Bold();
                    clientColumn.Item().Text($"Nombre: {clientPerson}").Bold();
                    clientColumn.Item().Text($"Identificación: {clientId}");
                    clientColumn.Item().Text($"Correo: {clientEmail}");
                    clientColumn.Item().Text($"Teléfono: {clientPhone}");
                });

                // Información del vendedor
                infoRow.RelativeItem().Column(vendorColumn =>
                {
                    vendorColumn.Item().Text($"Condición de Pago: {paymentCondition}");
                    vendorColumn.Item().Text($"Vendedor: {sellerName}");
                    vendorColumn.Item().Text($"Transporte: {transport}");
                });
            });

            // Tabla de productos
            column.Item().PaddingTop(20).Border(1).Table(table =>
            {
                // Definición de columnas
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(40);  // Cantidad
                    columns.ConstantColumn(100); // Código
                    columns.RelativeColumn();    // Descripción
                    columns.ConstantColumn(80);  // Precio
                    columns.ConstantColumn(80);  // Total
                });

                // Encabezado
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Cant").FontColor(Colors.White).FontSize(12).Bold();
                    header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Codigo").FontColor(Colors.White).FontSize(12).Bold();
                    header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Descripción").FontColor(Colors.White).FontSize(12).Bold();
                    header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Precio").FontColor(Colors.White).FontSize(12).Bold();
                    header.Cell().Background(Colors.Blue.Medium).AlignCenter().Text("Total").FontColor(Colors.White).FontSize(12).Bold();
                });

                // Filas de productos
                foreach (var product in products)
                {
                    table.Cell().AlignCenter().Text(product.Cantidad.ToString());
                    table.Cell().AlignCenter().Text(product.Codigo);
                    table.Cell().AlignCenter().Text(product.Descripcion);
                    table.Cell().AlignCenter().Text(product.Precio);
                    table.Cell().AlignCenter().Text(product.Total);
                }
                table.Cell().PaddingBottom(3);
            });

            // Nueva sección: Observaciones con QR y resumen
            column.Item().Border(1).Row(row =>
            {
                row.RelativeItem().Column(innerColumn =>
                {
                    
                });

                // Resumen de Totales y QR
                row.ConstantItem(200).Column(summaryColumn =>
                {
                    summaryColumn.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Subtotal").Bold();
                        row.ConstantItem(100).AlignRight().Text(subtotal).Bold();
                    });

                    summaryColumn.Item().Row(row =>
                    {
                        row.RelativeItem().Text("IVA").Bold();
                        row.ConstantItem(100).AlignRight().Text(iva).Bold();
                    });

                    summaryColumn.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Total").Bold().FontSize(12);
                        row.ConstantItem(100).AlignRight().Text(total).FontSize(12).Bold();
                    });

                });
            });

            // Cuentas a depositar y texto de resolución
            column.Item().Border(1).Row(row =>
            {
                row.RelativeItem().PaddingLeft(5).Column(innerColumn =>
                {
                    innerColumn.Item().Text("Cuentas a depositar:").Bold();

                    innerColumn.Item().Text("Banco Nacional:\nCta IBAN: CR94015100220010795717");
                    innerColumn.Item().Text("Banco de Costa Rica:\nCta IBAN: CR510152020205005717928");
                    innerColumn.Item().Text("BAC Credomatic:\nCta IBAN: CR49010200009544076311");
                });
            });
        });
    });
});

// use the following invocation
document.ShowInPreviewer();
