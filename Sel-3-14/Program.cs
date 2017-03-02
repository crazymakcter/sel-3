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

namespace Sel_3_14
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeDriver driver = new ChromeDriver(@"C:\web_config");
            Test01_VerifyOpenLinkInNewTab(driver);

            driver.Quit();
            Console.ReadKey();
        }

        public static void Test01_VerifyOpenLinkInNewTab(IWebDriver driver)
        {
            var nameProducCategory = "Countries";

            MyWebDriver.Admin_OpenAndLogin(driver, "admin", "admin");
            MyWebDriver.Admin_OpenCategory(driver, nameProducCategory);
            driver.FindElement(By.CssSelector(".fa.fa-pencil")).Click();
            driver.FindElement(By.CssSelector(".fa.fa-external-link")).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Console.WriteLine(driver.Url);
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
