using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Sel_3_08
{
    class Program
    {
        static void Main(string[] args)
        {
            Test01_HomePage_VerifyLabelOnProduct();
        }

        static void Test01_HomePage_VerifyLabelOnProduct()
        {
            ChromeDriver driver = new ChromeDriver(@"C:\web_config");
            driver.Navigate().GoToUrl("http://localhost:8082/litecart/");
            for(int i =0; i< driver.FindElementsByCssSelector(".product.column.shadow.hover-light").Count; i++)
            {

                try
                {
                    if (driver.FindElementsByCssSelector(".product.column.shadow.hover-light")[i].FindElement(By.CssSelector(".sticker")).Displayed)
                    {
                        if (driver.FindElementsByCssSelector(".product.column.shadow.hover-light")[i].FindElements(By.CssSelector(".sticker")).Count > 1)
                        {
                            Console.WriteLine(String.Format("{0} - have more than 1 sticker", 
                                driver.FindElementsByCssSelector(".product.column.shadow.hover-light")[i].Text));
                        }
                        else
                        {
                            Console.WriteLine(String.Format("{0} - have sticker {1}",
                                driver.FindElementsByCssSelector(".product.column.shadow.hover-light")[i].Text,
                                driver.FindElementsByCssSelector(".product.column.shadow.hover-light")[i].FindElement(By.CssSelector(".sticker")).Text));
                        }
                    }
                }
                catch
                {
                    Console.WriteLine(String.Format("{0} - not have sticker", 
                        driver.FindElementsByCssSelector(".product.column.shadow.hover-light")[i].Text));
                }
            }
            Thread.Sleep(5000);
            driver.Quit();

        }
    }
}
