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

namespace Sel_3_17
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.Warning);
            //options.BinaryLocation(@"C:\web_config");
            ChromeDriver driver = new ChromeDriver(@"C:\web_config", options);
            Test01_Admin_VerifyConsolLogOnProductsPage(driver);


            driver.Quit();
            Console.ReadKey();

        }

        public static void Test01_Admin_VerifyConsolLogOnProductsPage(IWebDriver driver)
        {
            var nameProducCategory = "Catalog";

            MyWebDriver.Admin_OpenAndLogin(driver, "admin", "admin");
            MyWebDriver.Admin_OpenCategory(driver, nameProducCategory);
            
            string xpath;
            driver.FindElement(By.XPath(".//*[@id='content']/form/table/tbody/tr[3]/td[3]/a")).Click();
            driver.FindElement(By.XPath(".//*[@id='content']/form/table/tbody/tr[4]/td[3]/a")).Click();
            var textLabel = driver.FindElement(By.XPath(".//*[@id='content']/form/table/tbody/tr[15]/td")).Text;
            var CategoryCount = textLabel.Split(' ')[1].Split(' ')[0];
            int ProductCount = int.Parse(textLabel.Split(' ')[4]);
            for(int count=5; count < (ProductCount + 5); count++)
            {
                xpath = String.Format(".//*[@id='content']/form/table/tbody/tr[{0}]/td[3]/a", count);
                driver.FindElement(By.XPath(xpath)).Click();
                Thread.Sleep(2000);
                var entries = driver.Manage().Logs.GetLog(LogType.Browser);
                if(entries.Count > 0)
                {
                    foreach (var entry in entries) {
                        Console.WriteLine(entry.ToString());
                    }
                }
                driver.Navigate().Back();
                Thread.Sleep(2000);
            }
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

    }
}
