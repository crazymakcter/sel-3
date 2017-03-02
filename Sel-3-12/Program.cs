using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System.Threading;
using System.IO;

namespace Sel_3_12
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver(@"C:\web_config");
            Test01_Admin_VerifyNewProduct(driver);

        }

        public static void Test01_Admin_VerifyNewProduct(IWebDriver driver)
        {
            Product newProduct = new Product();
            newProduct = Product.GenerateProduct();
            Console.WriteLine(newProduct.Image);
            var nameProducCategory = "Catalog";

            MyWebDriver.Admin_OpenAndLogin(driver, "admin", "admin");
            MyWebDriver.Admin_OpenCategory(driver, nameProducCategory);
            MyWebDriver.AddNewProduct(driver, newProduct);
            if (MyWebDriver.Admin_CheckProductInCatalog(driver, newProduct.Name))
            {
                Console.WriteLine("Product was added");
            }
            
            

            Console.ReadKey();
            driver.Quit();
        }
    }

    public class MyWebDriver
    {
        public static void Admin_OpenAndLogin(IWebDriver driver, string username, string password)
        {
            driver.Navigate().GoToUrl("http://localhost:8082/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys(username);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(2000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("success")));
        }

        public static void Admin_OpenCategory(IWebDriver driver, string nameCategory)
        {
            foreach (var element in driver.FindElements(By.CssSelector(".name")))
            {
                if (element.Text == nameCategory)
                {
                    element.Click();
                    break;
                }
            }
            //Console.WriteLine("Category nor found");
        }

        public static void AddNewProduct(IWebDriver driver, Product product)
        {
            driver.FindElement(By.XPath(".//*[@id='content']/div[1]/a[2]")).Click();
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(d => d.FindElement(By.ClassName("#content>h1")));
            driver.FindElement(By.CssSelector(".input-wrapper>input")).SendKeys(product.Name);
            driver.FindElement(By.CssSelector("#tab-general>table>tbody>tr>td>input")).SendKeys(product.Code);
            driver.FindElement(By.XPath(".//*[@id='tab-general']/table/tbody/tr[8]/td/table/tbody/tr/td[1]/input")).SendKeys(product.Quantity.ToString());
            //change tab
            driver.FindElements(By.CssSelector(".index>li>a"))[1].Click();
            SelectElement manufacturer = new SelectElement(driver.FindElement(By.XPath(".//*[@id='tab-information']/table/tbody/tr[1]/td/select")));
            manufacturer.SelectByText(product.Manufacturer);
            //change tab
            driver.FindElements(By.CssSelector(".index>li>a"))[3].Click();
            driver.FindElement(By.XPath(".//*[@id='tab-prices']/table[1]/tbody/tr/td/input")).SendKeys(product.PurchasePrice.ToString());
            driver.FindElement(By.XPath(".//*[@id='tab-prices']/table[3]/tbody/tr[2]/td[1]/span/input")).SendKeys(product.PriceUSD.ToString());
            driver.FindElement(By.XPath(".//*[@id='content']/form/p/span/button[1]")).Click();


        }

        public static bool Admin_CheckProductInCatalog(IWebDriver driver, string nameProduct)
        {
            foreach(var element in driver.FindElements(By.CssSelector(".row.semi-transparent>td>a")))
            {
                if (element.Text == nameProduct)
                {
                    return true;
                }
            }
            return false;
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

        public static Product GenerateProduct()
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
