using TemplateEngine.Docx;

namespace Client.Service.Impl;

public class OrderReportWord : IOrderReport
{
    private const string INVOICE_NUMBER = "InvoiceNumber";
    private const string DATE = "Date";
    private const string BUYER_NAME = "BuyerName";
    private const string BUYER_ADDRESS = "BuyerAddress";
    private const string BUYER_PHONE = "BuyerPhone";
    private const string PRODUCT_ROW = "ProductRow";
    private const string COUNT_NUMBER = "CountNumber";
    private const string PRODUCT_NAME = "ProductName";
    private const string QUANTITY = "Quantity";
    private const string PRODUCT_PRICE = "ProductPrice";
    private const string PRODUCT_TOTAL = "ProductTotal";
    private const string SUB_TOTAL = "SubTotal";
    private const string TAX = "Tax";
    private const string LABOR = "Labor";
    private const string TOTAL = "Total";

    private readonly FileInfo _templateFile;

    public int ReportId { get; set; }
    public DateTime CreationDate { get; set; }
    public string BuyerName { get; set; }
    public string BuyerAddress { get; set; }
    public string BuyerPhone { get; set; }
    public IEnumerable<(string name, int quantity, decimal price)> Products { get; set; }
    public decimal Tax { get; set; }
    public decimal LaborCompany { get; set; }

    public OrderReportWord(string templateFile)
    {
        _templateFile = new FileInfo(templateFile);
    }

    public FileInfo Create(string reportTemplateFile)
    {
        if (!_templateFile.Exists)
        {
            throw new NotImplementedException();
        }

        var reportFile = new FileInfo(reportTemplateFile);
        reportFile.Delete();
        _templateFile.CopyTo(reportFile.FullName);

        var rowsProducts = Products.Select((product, i) => new TableRowContent(
            new List<FieldContent>
            {
                new FieldContent(COUNT_NUMBER, (i+1).ToString()),
                new FieldContent(PRODUCT_NAME, product.name),
                new FieldContent(QUANTITY, product.quantity.ToString()),
                new FieldContent(PRODUCT_PRICE, product.price.ToString("c")),
                new FieldContent(PRODUCT_TOTAL, (product.price * product.quantity).ToString("c"))
            }
            )).ToArray();

        var subTotal = Products.Sum(product => product.price * product.quantity);
        Tax = subTotal * 0.21M;
        LaborCompany = subTotal * 0.1M;

        var content = new Content(
            new FieldContent(INVOICE_NUMBER, ReportId.ToString()),
            new FieldContent(DATE, CreationDate.ToString("dd.MM.yyyy HH:mm:ss")),
            new FieldContent(BUYER_NAME, BuyerName),
            new FieldContent(BUYER_ADDRESS, BuyerAddress),
            new FieldContent(BUYER_PHONE, BuyerPhone),
            new TableContent(PRODUCT_ROW, rowsProducts),
            new FieldContent(SUB_TOTAL, subTotal.ToString("c")),
            new FieldContent(TAX , Tax.ToString("c")),
            new FieldContent(LABOR, LaborCompany.ToString("c")),
            new FieldContent(TOTAL, (Products.Sum(product => product.price * product.quantity) + Tax + LaborCompany).ToString("c"))
            );

        var templateProcessor = new TemplateProcessor(reportFile.FullName)
            .SetRemoveContentControls(true);

        templateProcessor.FillContent(content);
        templateProcessor.SaveChanges();

        reportFile.Refresh();

        return reportFile;
    }
}