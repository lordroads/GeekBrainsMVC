using Orders.DAL.Entities;

namespace Client.Models.Reports;

public class OrderReport
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

}