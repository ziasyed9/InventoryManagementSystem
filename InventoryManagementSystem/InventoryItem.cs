using System;

namespace InventoryManagementSystem
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public InventoryItem(int id, string name, int quantity, decimal price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, Quantity: {Quantity}, Price: {Price:C}";
        }
    }
}
