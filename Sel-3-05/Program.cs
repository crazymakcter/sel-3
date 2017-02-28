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

namespace Sel_3_05
{
    class Program
    {
        private static FirefoxDriver DriverFF()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\web_config", "geckodriver.exe");
            service.FirefoxBinaryPath = @"C:\Program Files (x86)\Firefox Developer Edition\firefox.exe";
            return new FirefoxDriver(service);
        }
        private static FirefoxDriver DriverFFold()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = true;
            options.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox ESR\firefox.exe";
            return new FirefoxDriver(options);
        }
        private static ChromeDriver DriverChrome()
        {
            return new ChromeDriver(@"C:\web_config");
        }
        private static InternetExplorerDriver DriverIE()
        {
            //Edge not work =(
            //RemoteWebDriver driver = null;
            //string serverPath = @"C:\web_config\";
            //EdgeOptions options = new EdgeOptions();
            //options.PageLoadStrategy = EdgePageLoadStrategy.Eager;
            //return new EdgeDriver(serverPath, options);
            return new InternetExplorerDriver(@"C:\web_config\");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please selected type of Driver: \n\t1 - Chrome \n\t2 - FireFox\n\t3 - FireFox ESR\n\t4 - IE");
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
                    driver = DriverFFold();
                    break;
                case "4":
                    driver = DriverIE();
                    break;
                default:
                    Console.WriteLine("Please selected other");
                    driver = DriverChrome();
                    break;


            }
            driver.Navigate().GoToUrl("http://localhost:8082/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            if (typeOfDriver == "3")
            {
                driver.FindElement(By.Name("password")).SendKeys(Keys.Enter);
            }
            else
            {
                driver.FindElement(By.Name("login")).Click();
            }
            Thread.Sleep(2000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("success")));

            var textAboutLogin = driver.FindElement(By.XPath("//*[@id=\"notices\"]/div[3]")).Text;
            if (textAboutLogin == "You are now logged in as admin")
                Console.WriteLine(String.Format("Check text after login: {0}", textAboutLogin));
            Thread.Sleep(5000);
            driver.Quit();

        }
    }
}
