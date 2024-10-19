using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InventoryManagementSystem
{
    class Program
    {
        private static List<InventoryItem> inventory = new List<InventoryItem>();
        private static string filePath = "inventory.txt";

        static void Main(string[] args)
        {
            LoadInventory();
            string option;

            do
            {
                Console.Clear();
                Console.WriteLine("Inventory Management System");
                Console.WriteLine("1. View Inventory");
                Console.WriteLine("2. Add Item");
                Console.WriteLine("3. Update Item");
                Console.WriteLine("4. Delete Item");
                Console.WriteLine("5. Search Item");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewInventory();
                        break;
                    case "2":
                        AddItem();
                        break;
                    case "3":
                        UpdateItem();
                        break;
                    case "4":
                        DeleteItem();
                        break;
                    case "5":
                        SearchItem();
                        break;
                    case "6":
                        SaveInventory();
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            } while (option != "6");
        }

        private static void ViewInventory()
        {
            Console.Clear();
            Console.WriteLine("Current Inventory:");
            foreach (var item in inventory)
            {
                Console.WriteLine(item);
            }
        }

        private static void AddItem()
        {
            Console.Clear();
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            int id = inventory.Count > 0 ? inventory.Max(i => i.Id) + 1 : 1; // Simple ID assignment
            inventory.Add(new InventoryItem(id, name, quantity, price));
            Console.WriteLine("Item added.");
        }

        private static void UpdateItem()
        {
            Console.Clear();
            ViewInventory();
            Console.Write("Enter the ID of the item to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            InventoryItem item = inventory.Find(i => i.Id == id);
            if (item != null)
            {
                Console.Write("Enter new name (leave blank to keep current): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    item.Name = newName;
                }

                Console.Write("Enter new quantity (leave blank to keep current): ");
                string newQuantityInput = Console.ReadLine();
                if (int.TryParse(newQuantityInput, out int newQuantity))
                {
                    item.Quantity = newQuantity;
                }

                Console.Write("Enter new price (leave blank to keep current): ");
                string newPriceInput = Console.ReadLine();
                if (decimal.TryParse(newPriceInput, out decimal newPrice))
                {
                    item.Price = newPrice;
                }
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        private static void DeleteItem()
        {
            Console.Clear();
            ViewInventory();
            Console.Write("Enter the ID of the item to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            InventoryItem item = inventory.Find(i => i.Id == id);
            if (item != null)
            {
                inventory.Remove(item);
                Console.WriteLine("Item deleted.");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        private static void SearchItem()
        {
            Console.Clear();
            Console.Write("Enter item name to search: ");
            string searchTerm = Console.ReadLine();
            var foundItems = inventory.Where(i => i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            Console.WriteLine("Search Results:");
            if (foundItems.Any())
            {
                foreach (var item in foundItems)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("No items found.");
            }
        }

        private static void LoadInventory()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        int id = Convert.ToInt32(parts[0]);
                        string name = parts[1];
                        int quantity = Convert.ToInt32(parts[2]);
                        decimal price = Convert.ToDecimal(parts[3]);
                        InventoryItem item = new InventoryItem(id, name, quantity, price);
                        inventory.Add(item);
                    }
                }
            }
        }

        private static void SaveInventory()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in inventory)
                {
                    writer.WriteLine($"{item.Id},{item.Name},{item.Quantity},{item.Price}");
                }
            }
        }
    }
}
