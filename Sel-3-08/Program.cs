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
            IList<IWebElement> ListProducts = driver.FindElementsByCssSelector(".product.column.shadow.hover-light");
            for(int i =0; i< ListProducts.Count; i++)
            {

                try
                {
                    if (ListProducts[i].FindElement(By.CssSelector(".sticker")).Displayed)
                    {
                        if (ListProducts[i].FindElements(By.CssSelector(".sticker")).Count > 1)
                        {
                            Console.WriteLine(String.Format("{0} - have more than 1 sticker", 
                                ListProducts[i].Text));
                        }
                        else
                        {
                            Console.WriteLine(String.Format("{0} - have sticker {1}",
                                ListProducts[i].Text,
                                ListProducts[i].FindElement(By.CssSelector(".sticker")).Text));
                        }
                    }
                }
                catch
                {
                    Console.WriteLine(String.Format("{0} - not have sticker", 
                        ListProducts[i].Text));
                }
            }
            Thread.Sleep(5000);
            driver.Quit();

        }
    }
}
