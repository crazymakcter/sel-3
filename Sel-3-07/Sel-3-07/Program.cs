using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace Sel_3_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Test01_VerifyMenuItemsAndGetH1FromPage();
        }

        [Test]
        static void Test01_VerifyMenuItemsAndGetH1FromPage()
        {
            ChromeDriver driver = new ChromeDriver(@"C:\web_config");
            driver.Navigate().GoToUrl("http://localhost:8082/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(2000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("success")));
            List<IWebElement> listMenu = new List<IWebElement> {};
            for (int i = 0; i < driver.FindElementsByXPath(".//*[@id='app-']/a/span[2]").Count(); i++ )
            {
                driver.FindElementsByXPath(".//*[@id='app-']/a/span[2]")[i].Click();
                Console.WriteLine(String.Format("Menu {0}:", driver.FindElementsByXPath(".//*[@id='app-']/a/span[2]")[i]));
                for (int j = 0; j < driver.FindElementsByXPath(".//*[@id='app-']/ul/li").Count(); j++)
                {
                    driver.FindElementsByXPath(".//*[@id='app-']/ul/li")[j].Click();
                    Console.WriteLine(String.Format("Sub-menu: {0}", driver.FindElementsByXPath(".//*[@id='app-']/ul/li")[j].Text));
                    if (driver.FindElementByTagName("h1").Displayed)
                    {
                        Console.WriteLine(String.Format("h1 tag found, text:", driver.FindElementByTagName("h1").Text));
                    }
                    else
                    {
                        Console.WriteLine("h1 tag not found");
                    }
                }
            }

            Thread.Sleep(5000);
            driver.Quit();

        }
    }
}
