using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CRMSystem.Business.Services.QuestPDF;

public class OrderPdfService : IOrderPdfService
{
    public byte[] GenerateOrderPdf(OrderDocumentModel data)
    {
        var workSum = 0m;
        var partSum = 0m;
        var saleValue = 0;

        var carStationInfo =
            "ИНН: 502003661704, ИП «Севрюк П.Ю.», р/сч: 222222 в ООО Банк, БИК: 44444, кор/сч: 33333" +
            "\n197343, аг. Новогородейский, ул. Терешковой 2Г, корп. 4, тел: +375(44)757-05-85";
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.DefaultTextStyle(x => 
                    x.FontSize(10)
                        .FontFamily("Verdana")
                        .FontColor(Colors.Black)
                        .LineHeight(1f));
                page.Margin(40);
                
                page.Header()
                    .PaddingBottom(15)
                    .Column(col =>
                {
                    col.Item()
                        .PaddingBottom(30)
                        .Text($"ЗАКАЗ-НАРЯД №{data.OrderId}")
                        .FontSize(16)
                        .SemiBold()
                        .AlignCenter();
                    
                    col.Item().PaddingBottom(10).Row(row =>
                    {
                            row.RelativeItem()
                                .Text($"Дата приемки: {data.StartDate:dd.MM.yyyy}");
                            row.RelativeItem()
                                .Text($"Дата окончания работ: {data.FinishDate:dd.MM.yyyy.hh.mm.ss}");
                    });
                    
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(3);
                            cd.RelativeColumn(7);
                        });

                        table.Cell()
                            .Element(CellStyle)
                            .Text("Поставщик:")
                            .AlignStart();
                        table.Cell()
                            .Element(CellStyle)
                            .Text($"{carStationInfo}")
                            .AlignStart();
                        
                        table.Cell()
                            .Element(CellStyle)
                            .Text("Владелец/Заказчик:")
                            .AlignStart();
                        table.Cell()
                            .Element(CellStyle)
                            .Text($"{data.ClientData.Name 
                                     + " " + data.ClientData.Surname} ({data.ClientData.PhoneNumber})")
                            .AlignStart();
                        
                        table.Cell()
                            .Element(CellStyle)
                            .Text("Автомобиль:")
                            .AlignStart();
                        table.Cell()
                            .Element(CellStyle)
                            .Text($"{data.CarData.Model 
                                     + " " + data.CarData.Brand 
                                     + " " + data.CarData.VinNumber}({data.CarData.StateNumber})")
                            .AlignStart();
                    });
                });
                
                page.Content().Column(col =>
                {
                    col.Item()
                        .PaddingBottom(15)
                        .Text("Выполненные работы, использованные запчасти и материалы:")
                        .SemiBold()
                        .AlignCenter();
                    
                    col.Item()
                        .PaddingBottom(10)
                        .Border(1)
                        .BorderColor(Colors.Black)
                        .Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(5);
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                        });
                        
                        table.Header(h =>
                        {
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Наименование работы")
                                .AlignStart();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Н/в")
                                .AlignCenter();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Цена")
                                .AlignCenter();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Сумма")
                                .AlignCenter();
                        });
                        
                        foreach (var work in data.WorkLines)
                        {
                             table.Cell()
                                 .Element(CellStyle)
                                 .Text(work.Job)
                                 .AlignStart();
                             table.Cell()
                                 .Element(CellStyle)
                                 .Text($"{work.TimeSpent}")
                                 .AlignEnd();
                             table.Cell()
                                 .Element(CellStyle)
                                 .Text($"{work.HourlyPrice}")
                                 .AlignEnd();
                             table.Cell()
                                 .Element(CellStyle)
                                 .Text($"{work.HourlyPrice * work.TimeSpent}")
                                 .AlignEnd();
                             
                             workSum += work.HourlyPrice * work.TimeSpent;
                        }
                        
                        table.Footer(f =>
                        {
                            f.Cell()
                                .ColumnSpan(3)
                                .Element(CellStyle)
                                .Text("ИТОГО:")
                                .ExtraBold()
                                .AlignEnd();
                            f.Cell()
                                .Element(CellStyle)
                                .Text($"{workSum}")
                                .AlignEnd();
                        });
                    });
                     
                    col.Item().PaddingBottom(10).Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(5);
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                        });
                        
                        table.Header(h =>
                        {
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Наименование запчасти")
                                .AlignStart();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Кол-во")
                                .AlignCenter();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Цена")
                                .AlignStart();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Сумма")
                                .AlignStart();
                        });
                        
                        foreach (var part in data.PartsData)
                        {
                            table.Cell()
                                .Element(CellStyle)
                                .Text(part.Name)
                                .LineHeight(1f)
                                .AlignStart();
                            table.Cell()
                                .Element(CellStyle)
                                .Text($"{part.Quantity}")
                                .AlignEnd();
                            table.Cell()
                                .Element(CellStyle)
                                .Text($"{part.SoldPrice}")
                                .AlignEnd();
                            table.Cell()
                                .Element(CellStyle)
                                .Text($"{part.Quantity * part.SoldPrice}")
                                .AlignEnd();
                            
                            partSum += part.Quantity * part.SoldPrice;
                        }
                        
                        table.Footer(f =>
                        {
                            f.Cell()
                                .ColumnSpan(3)
                                .Element(CellStyle)
                                .Text("ИТОГО:")
                                .AlignEnd()
                                .ExtraBold();
                            f.Cell()
                                .Element(CellStyle)
                                .Text($"{partSum}")
                                .AlignEnd();
                        });
                    });

                    col.Item()
                        .PaddingBottom(10)
                        .Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(2);
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                            cd.RelativeColumn();
                            cd.RelativeColumn(2);
                        });
                        
                        table.Header(h =>
                        {
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Стоимость без скидки:")
                                .AlignCenter()
                                .ExtraBold();
                            h.Cell()
                                .Element(CellStyle)
                                .Text($"{partSum + workSum}")
                                .AlignEnd();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("Скидка:")
                                .ExtraBold()
                                .AlignEnd();
                            h.Cell()
                                .Element(CellStyle)
                                .Text($"{(partSum + workSum) * saleValue}")
                                .AlignEnd();
                            h.Cell()
                                .Element(CellStyle)
                                .Text("ИТОГО:")
                                .AlignEnd()
                                .ExtraBold();
                            h.Cell()
                                .Element(CellStyle)
                                .Text($"{(partSum + workSum) - ((partSum + workSum) * saleValue)}")
                                .FontSize(12)
                                .AlignEnd()
                                .ExtraBold()
                                .Underline();
                        });
                    });
                    
                    col.Item()
                        .AlignLeft()
                        .Text($"Гарантийные условия: ")
                        .Bold();
                    col.Item()
                        .SemanticParagraph()
                        .PaddingTop(2)
                        .Text($"{string.Join(", ", data.GuaranteeTerms)}");
                    col.Item()
                        .PaddingTop(10)
                        .AlignLeft()
                        .Text($"Гарантийные обязательства: ")
                        .Bold();
                    foreach (var guarantee in data.GuaranteeData)
                    {
                        col.Item()
                            .SemanticParagraph()
                            .PaddingTop(2)
                            .Text($"{guarantee}");
                    }
                });
                
                page.Footer()
                    .Column(col =>
                {
                    col.Item()
                        .Row(row =>
                    {
                        row.RelativeItem()
                            .Text($"{data.ClientData.Name} {data.ClientData.Surname} ________________ (Заказчик)")
                            .AlignStart();
                        
                        row.RelativeItem()
                            .Text("М.П. ________________ (Мастер)");
                    });
                    
                    col.Item()
                        .PaddingTop(10)
                        .Row(row =>
                    {
                        row.RelativeItem()
                            .Width(240)
                            .AlignLeft()
                            .Text("CTO \"Севрюк\" аг. Новогородейский," +
                                  "\n ул. Терешковой 2Г, корп. 4" +
                                  "\n тел: +375(44)757-05-85");
                        
                        row.RelativeItem()
                            .AlignRight()
                            .Text(x =>
                        {
                            x.Span("Страница ")
                                .FontSize(8)
                                .Italic();
                            
                            x.CurrentPageNumber()
                                .FontSize(8)
                                .Italic();
                            
                            x.Span(" из ")
                                .FontSize(8)
                                .Italic();

                            x.TotalPages()
                                .FontSize(8)
                                .Italic();
                        });
                    });
                });
            });
        })
        .WithMetadata(new DocumentMetadata
        {
            Language = "en-US",
            CreationDate = DateTimeOffset.Now,
            ModifiedDate = DateTimeOffset.Now
        });

        return document.GeneratePdf();
    }
    
    static IContainer CellStyle(IContainer container)
    {
        return container
            .Border(1)
            .BorderColor(Colors.Black)
            .PaddingVertical(2)
            .PaddingHorizontal(5);
    }
}