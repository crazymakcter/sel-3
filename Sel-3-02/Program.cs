using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Sel_3_02
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new ChromeDriver(@"C:\web_config"))
            {
                
                driver.Navigate().GoToUrl("http://localhost:8082/litecart/admin/");
                driver.FindElement(By.Name("username")).SendKeys("admin");
                driver.FindElement(By.Name("password")).SendKeys("admin");
                driver.FindElement(By.Name("login")).Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(By.ClassName("success")));

                // Should see: "Cheese - Google Search" (for an English locale)
                var textAboutLogin = driver.FindElement(By.XPath("//*[@id=\"notices\"]/div[3]")).Text;
                if (textAboutLogin == "You are now logged in as admin")
                    Console.WriteLine(String.Format("Check text after login: {0}", textAboutLogin));
                Console.ReadKey();
                driver.Quit();
            }
        }
    }
}
