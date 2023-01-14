namespace Client.Service;

public interface IProductReport
{
    public string CatalogName { get; set; }
    public string CatalogDescription { get; set; }
    public DateTime CreationDate { get; set; }
    public IEnumerable<(int id, string name, string category, decimal price)> Products { get; set; }

    public FileInfo Create(string reportTemplateFile);
}