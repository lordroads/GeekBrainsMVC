using TemplateEngine.Docx;

namespace Client.Service.Impl;

public class ProductReportWord : IProductReport
{
    private const string _FeildCatalogName = "ProductName";
    private const string _FeildCatalogDescription = "ProductDescription";
    private const string _FeildCatalogDate = "ProductDate";

    private const string _FeildTableRow = "Product";
    private const string _FeildTableId = "ProductId";
    private const string _FeildTableName = "pName";
    private const string _FeildTableCategory = "pCategory";
    private const string _FeildTablePrice = "pPrice";
    private const string _FeildTableTotal = "Total";

    private readonly FileInfo _templateFile;

    public string CatalogName { get; set; }
    public string CatalogDescription { get; set; }
    public DateTime CreationDate { get; set; }
    public IEnumerable<(int id, string name, string category, decimal price)> Products { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateFile">Path to file template.</param>
    public ProductReportWord(string templateFile)
    {
        _templateFile = new FileInfo(templateFile);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reportTemplateFile">Path to file report.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public FileInfo Create(string reportTemplateFile)
    {
        if (!_templateFile.Exists)
        {
            throw new NotImplementedException();
        }

        var reportFile = new FileInfo(reportTemplateFile);
        reportFile.Delete();
        _templateFile.CopyTo(reportFile.FullName);

        var rowsProducts = Products.Select(product => new TableRowContent(
            new List<FieldContent>
            {
                new FieldContent(_FeildTableId, product.id.ToString()),
                new FieldContent(_FeildTableName, product.name),
                new FieldContent(_FeildTableCategory, product.category),
                new FieldContent(_FeildTablePrice, product.price.ToString("c")),
            }
            )).ToArray();

        var content = new Content(
            new FieldContent(_FeildCatalogName, CatalogName),
            new FieldContent(_FeildCatalogDescription, CatalogDescription),
            new FieldContent(_FeildCatalogDate, CreationDate.ToString("dd.MM.yyyy HH:mm:ss")),
            new TableContent(_FeildTableRow, rowsProducts),
            new FieldContent(_FeildTableTotal, Products.Sum(product => product.price).ToString("c"))
            );

        var templateProcessor = new TemplateProcessor(reportFile.FullName)
            .SetRemoveContentControls(true);

        templateProcessor.FillContent(content);
        templateProcessor.SaveChanges();

        reportFile.Refresh();

        return reportFile;
    }
}