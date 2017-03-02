using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Remote;
using System.Reflection;

namespace Sel_3_10
{
    class Program
    {
        private static FirefoxDriver DriverFF()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\web_config", "geckodriver.exe");
            service.FirefoxBinaryPath = @"C:\Program Files (x86)\Firefox Developer Edition\firefox.exe";
            return new FirefoxDriver(service);
        }
        private static ChromeDriver DriverChrome()
        {
            return new ChromeDriver(@"C:\web_config");
        }
        private static InternetExplorerDriver DriverIE()
        {
            return new InternetExplorerDriver(@"C:\web_config\");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please selected type of Driver: \n\t1 - Chrome \n\t2 - FireFox\n\t3 - IE");
            var typeOfDriver = Console.ReadLine();
            IWebDriver driver;
            switch (typeOfDriver)
            {
                case "1":
                    driver = DriverChrome();
                    break;
                case "2":
                    driver = DriverFF();
                    break;
                case "3":
                    driver = DriverIE();
                    break;
                default:
                    Console.WriteLine("Please selected other");
                    driver = DriverChrome();
                    break;
            }

            Test01_VerifyProductAfterAddToCart(driver);
            Thread.Sleep(5000);
            driver.Quit();

            Console.ReadKey();

        }

        static void Test01_VerifyProductAfterAddToCart(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:8082/litecart/");

            //Home page
            IWebElement Product = driver.FindElement(By.XPath(".//*[@id='box-campaigns']/div/ul/li/a[1]"));
            IWebElement productRegPriceElement = Product.FindElement(By.CssSelector(".regular-price"));
            IWebElement productCampaignPriceElement = Product.FindElement(By.CssSelector(".campaign-price"));
            Product Duck = new Product();
            Duck.PoductName = Product.FindElement(By.CssSelector(".name")).Text;
            Duck.RegPrice = productRegPriceElement.Text;
            Duck.CurrentPrice = productCampaignPriceElement.Text;

            Console.WriteLine(String.Format("Product Name - {0}", Duck.PoductName));
            Console.WriteLine(String.Format("RegPrice: {0}", Duck.RegPrice));
            Console.WriteLine(String.Format("CampaignPrice: {0}", Duck.CurrentPrice));
            
            var colorRegPrice = productRegPriceElement.GetCssValue("color");
            try
            {
                productRegPriceElement.GetAttribute("s");
            }
            catch
            {
                Console.WriteLine("RegPrice not have attribute 's'");
            }
            float sizeTextRegPrice = float.Parse(productRegPriceElement.GetCssValue("font-size").Split('p')[0]);
            Console.WriteLine("CssColor for RegPrice: " + colorRegPrice);


            
            
            var colorCampaignPrice = productCampaignPriceElement.GetCssValue("color");
            float sizeTextCampaignPrice = float.Parse(productCampaignPriceElement.GetCssValue("font-size").Split('p')[0]);
            Console.WriteLine("CssColor for CampaingPrice: " + colorCampaignPrice);
            if(sizeTextCampaignPrice >=  sizeTextRegPrice)
            {
                Console.WriteLine();
            }

            if(sizeTextRegPrice >= sizeTextCampaignPrice)
            {
                Console.WriteLine("sizeTextRegPrice >= sizeTextCampaignPrice");
            }

            //Open Prodcut page
            Product.Click();
            Thread.Sleep(3000);
            //driver.SwitchTo().Window(driver.WindowHandles[1]);
            var currentName = driver.FindElement(By.XPath(".//*[@id='box-product']/div[1]/h1")).Text;
            if (Duck.PoductName != currentName)
            {
                Console.WriteLine(String.Format("Not same name. \nFrom homepage {0}, \nFrom product page {1}", Duck.PoductName, currentName));
            }

            productRegPriceElement = driver.FindElement(By.CssSelector(".regular-price"));
            productCampaignPriceElement = driver.FindElement(By.CssSelector(".campaign-price"));

            var currentRegPrice = productRegPriceElement.Text;
            if(Duck.RegPrice != currentRegPrice)
            {
                Console.WriteLine(String.Format("Not same regular price. \nFrom homepage {0}, \nFrom product page {1}", Duck.RegPrice, currentRegPrice));
            }
            var colorRegPriceFromProductPage = productRegPriceElement.GetCssValue("color");
            if (colorRegPrice != colorRegPriceFromProductPage)
            {
                Console.WriteLine(String.Format("RegularPrice - not same color. \nFrom homepage {0}, \nFrom product page {1}", 
                    colorRegPrice, 
                    colorRegPriceFromProductPage), ConsoleColor.Red);
            }
            var currentPrice = productCampaignPriceElement.Text;
            if(Duck.CurrentPrice != currentPrice)
            {
                Console.WriteLine(String.Format("Not same current price. \nFrom homepage {0}, \nFrom product page {1}", 
                    Duck.CurrentPrice, 
                    currentPrice), ConsoleColor.Red);
            }
            var colorCampaignPriceFromProductPage = productCampaignPriceElement.GetCssValue("color");
            if (colorCampaignPrice != colorCampaignPriceFromProductPage)
            {
                Console.WriteLine(String.Format("CampaignPrice - not same color. \nFrom homepage {0}, \nFrom product page {1}", 
                    colorCampaignPrice, 
                    colorCampaignPriceFromProductPage), ConsoleColor.Red);
            }

            sizeTextRegPrice = float.Parse(productRegPriceElement.GetCssValue("font-size").Split('p')[0]);
            sizeTextCampaignPrice = float.Parse(productCampaignPriceElement.GetCssValue("font-size").Split('p')[0]);
            if (sizeTextRegPrice >= sizeTextCampaignPrice)
            {
                Console.WriteLine("sizeTextRegPrice >= sizeTextCampaignPrice");
            }

        }
        //.//*[@id='box-campaigns']/div/ul/li/a[1]


    }

    public class Product
    {
        public string PoductName {get; set;}
        public string RegPrice { get; set; }
        public string CurrentPrice { get; set; }
    }

    

}
