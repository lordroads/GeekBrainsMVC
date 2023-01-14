namespace Client.Service;

public interface IOrderReport
{
    public int ReportId { get; set; }
    public DateTime CreationDate { get; set; }
    public string BuyerName { get; set; }
    public string BuyerAddress { get; set; }
    public string BuyerPhone { get; set; }
    public IEnumerable<(string name, int quantity, decimal price)> Products { get; set; }
    public decimal Tax { get; set; }
    public decimal LaborCompany { get; set; }

    public FileInfo Create(string reportTemplateFile);
}