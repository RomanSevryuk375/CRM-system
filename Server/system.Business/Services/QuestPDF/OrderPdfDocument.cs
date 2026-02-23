using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CRMSystem.Business.Services.QuestPDF;

public class OrderPdfDocument : IOrderPdfDocument
{
    public byte[] GenerateOrderPdf(OrderDocumentModel data)
    {
        var workSum = 0m;
        var partSum = 0m;
        var saleValue = 0;

        var carStationInfo =
            "ИНН: 502003661704, ИП «Севрюк П.Ю.», р/сч: 222222 в ООО Банк, БИК: 44444, кор/сч: 33333\n197343, аг. Новогородейский, ул. Терешковой 2Г, корп. 4, тел: +375(44)757-05-85";
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Verdana"));
                page.Margin(40);
                page.Header().PaddingBottom(15).Column(col =>
                {
                    col.Item().PaddingBottom(30).Text($"ЗАКАЗ-НАРЯД №{data.OrderId}").FontSize(16).SemiBold().FontColor(Colors.Black).AlignCenter();
                    
                    col.Item().PaddingBottom(10).Row(row =>
                    {
                            row.RelativeItem().Text($"Дата приемки: {data.StartDate:dd.MM.yyyy}").FontSize(10);
                            row.RelativeItem().Text($"Дата приемки: {data.FinishDate:dd.MM.yyyy.hh.mm.ss}").FontSize(10);
                    });
                    
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(3);
                            cd.RelativeColumn(7);
                        });

                        table.Cell().Element(CellStyle).Text("Поставщик:").LineHeight(1f).FontSize(10).AlignStart();
                        table.Cell().Element(CellStyle).Text($"{carStationInfo}").LineHeight(1f).FontSize(10).AlignStart();
                        
                        table.Cell().Element(CellStyle).Text("Владелец/Заказчик:").LineHeight(1f).FontSize(10).AlignStart();
                        table.Cell().Element(CellStyle).Text($"{data.ClientData.Name + " " + data.ClientData.Surname} ({data.ClientData.PhoneNumber})").LineHeight(1f).FontSize(10).AlignStart();
                        
                        table.Cell().Element(CellStyle).Text("Автомобиль:").LineHeight(1f).FontSize(10).AlignStart();
                        table.Cell().Element(CellStyle).Text($"{data.CarData.Model + " " + data.CarData.Brand + " " + data.CarData.VinNumber}({data.CarData.StateNumber})").LineHeight(1f).FontSize(10).AlignStart();
                    });
                });
                
                page.Content().Column(col =>
                {
                    col.Item().PaddingBottom(15).Text("Выполненные работы, использованные запчасти и материалы:").SemiBold().AlignCenter();
                    col.Item().PaddingBottom(10).Border(1).BorderColor(Colors.Black).Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(5);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                        });
                        
                        table.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Наименование работы").LineHeight(1f).FontSize(10).AlignStart();
                            h.Cell().Element(CellStyle).Text("Н/в").LineHeight(1f).FontSize(10).AlignCenter();
                            h.Cell().Element(CellStyle).Text("Цена").LineHeight(1f).FontSize(10).AlignCenter();
                            h.Cell().Element(CellStyle).Text("Сумма").LineHeight(1f).FontSize(10).AlignCenter();
                        });
                        
                        foreach (var work in data.WorkLines)
                        {
                             table.Cell().Element(CellStyle).Text(work.Job).LineHeight(1f).FontSize(10).AlignStart();
                             table.Cell().Element(CellStyle).Text($"{work.TimeSpent}").LineHeight(1f).FontSize(10).AlignEnd();
                             table.Cell().Element(CellStyle).Text($"{work.HourlyPrice}").LineHeight(1f).FontSize(10).AlignEnd();
                             table.Cell().Element(CellStyle).Text($"{work.HourlyPrice * work.TimeSpent}").LineHeight(1f).FontSize(10).AlignEnd();
                             workSum += work.HourlyPrice * work.TimeSpent;
                        }
                        
                        table.Footer(f =>
                        {
                            f.Cell().ColumnSpan(3).Element(CellStyle).Text("ИТОГО:").LineHeight(1f).FontSize(10).ExtraBold().AlignEnd();
                            f.Cell().Element(CellStyle).Text($"{workSum}").LineHeight(1f).FontSize(10).AlignEnd();
                        });
                    });
                     
                    col.Item().PaddingBottom(10).Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(5);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                        });
                        
                        table.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Наименование запчасти").LineHeight(1f).FontSize(10).AlignStart();
                            h.Cell().Element(CellStyle).Text("Кол-во").LineHeight(1f).FontSize(10).AlignCenter();
                            h.Cell().Element(CellStyle).Text("Цена").LineHeight(1f).FontSize(10).AlignStart();
                            h.Cell().Element(CellStyle).Text("Сумма").LineHeight(1f).FontSize(10).AlignStart();
                        });
                        
                        foreach (var part in data.PartsData)
                        {
                            table.Cell().Element(CellStyle).Text(part.Name).LineHeight(1f).FontSize(10).AlignStart();
                            table.Cell().Element(CellStyle).Text($"{part.Quantity}").LineHeight(1f).FontSize(10).AlignEnd();
                            table.Cell().Element(CellStyle).Text($"{part.SoldPrice}").LineHeight(1f).FontSize(10).AlignEnd();
                            table.Cell().Element(CellStyle).Text($"{part.Quantity * part.SoldPrice}").LineHeight(1f).FontSize(10).AlignEnd();
                            partSum += part.Quantity * part.SoldPrice;
                        }
                        
                        table.Footer(f =>
                        {
                            f.Cell().ColumnSpan(3).Element(CellStyle).Text("ИТОГО:").LineHeight(1f).FontSize(10).AlignEnd().ExtraBold();
                            f.Cell().Element(CellStyle).Text($"{partSum}").LineHeight(1f).FontSize(10).AlignEnd();
                        });
                    });

                    col.Item().PaddingBottom(10).Table(table =>
                    {
                        table.ColumnsDefinition(cd =>
                        {
                            cd.RelativeColumn(2);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(1);
                            cd.RelativeColumn(2);
                        });
                        
                        table.Header(h =>
                        {
                            h.Cell().Element(CellStyle).Text("Стоимость без скидки:").LineHeight(1f).FontSize(10).AlignCenter().ExtraBold();
                            h.Cell().Element(CellStyle).Text($"{partSum + workSum}").LineHeight(1f).FontSize(10).AlignEnd();
                            h.Cell().Element(CellStyle).Text("Скидка:").LineHeight(1f).FontSize(10).ExtraBold().AlignEnd();
                            h.Cell().Element(CellStyle).Text($"{(partSum + workSum) * saleValue}").LineHeight(1f).FontSize(10).AlignEnd();
                            h.Cell().Element(CellStyle).Text("ИТОГО:").LineHeight(1f).FontSize(10).AlignEnd().ExtraBold();
                            h.Cell().Element(CellStyle).Text($"{(partSum + workSum) - ((partSum + workSum) * saleValue)}").LineHeight(1f).FontSize(12).AlignEnd().ExtraBold().Underline();
                        });
                    });
                    
                    col.Item().AlignLeft().Text($"Гарантийные условия: ").LineHeight(1f).Bold();
                    col.Item().SemanticParagraph().PaddingTop(2).Text($"{string.Join(", ", data.GuaranteeTerms)}");
                    col.Item().PaddingTop(10).AlignLeft().Text($"Гарантийные обязательства: ").LineHeight(1f).Bold();
                    // col.Item().SemanticParagraph().Text($"{string.Join(", ", data.GuaranteeData)}");
                    foreach (var g in data.GuaranteeData)
                    {
                        col.Item().SemanticParagraph().PaddingTop(2).Text($"{g}");
                    }
                });
                
                page.Footer().Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"{data.ClientData.Name} {data.ClientData.Surname} ________________ (Заказчик)").AlignStart();
                        row.RelativeItem().Text("М.П. ________________ (Мастер)");
                    });
                    
                    col.Item().PaddingTop(10).Row(row =>
                    {
                        row.RelativeItem().Width(240).AlignLeft()
                            .Text("CTO \"Севрюк\" аг. Новогородейский,\n ул. Терешковой 2Г, корп. 4\n тел: +375(44)757-05-85");
                        
                        row.RelativeItem().AlignRight().Text(x =>
                        {
                            x.Span("Страница ").FontSize(8).Italic().LineHeight(1f);
                            x.CurrentPageNumber().FontSize(8).Italic().LineHeight(1f);
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