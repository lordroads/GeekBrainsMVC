using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.DAL.Entities;

[Table("OrderItems")]
public class OrderItem : Entity
{
    public Product Product { get; set; } = null;
    public int Quantity { get; set; }

    [Required]
    public Order Order { get; set; } = null;
}