using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sel_3_12
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Product
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public string Keywords { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string HeadTitle { get; set; }
        public string MetaDescription { get; set; }
        public int PurchasePrice { get; set; }
        public string CurrencyCode { get; set; }
        public string TaxClass { get; set; }
        public int PriceUSD { get; set; }

        static Product GenerateProducet()
        {
            Product NewProduct = new Product();
            NewProduct.Name = "BlackDuck" + GenerateText(5);
            NewProduct.Code = "bd0001" + GenerateNumber();
            NewProduct.Quantity = GenerateNumber();
            NewProduct.Image = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "black_duck.jpg";
            //NewProduct.ValidFrom = "2017-01-01";
            //NewProduct.ValidTo = "2017-03-03";
            NewProduct.Manufacturer = "ACME Corp.";
            NewProduct.ShortDescription = "Test Product";
            NewProduct.Description = "Description Test Product";
            NewProduct.PurchasePrice = 11;
            NewProduct.PriceUSD = GenerateNumber();

            return NewProduct;
        }

        private static Random random = new Random();
        public static string GenerateText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static int GenerateNumber()
        {
            return random.Next(999);
        }
    }
}
