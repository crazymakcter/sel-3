using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Sel_3_11
{
    class Program
    {
        static void Main(string[] args)
        {
            Test01_VerifiregistartionAndReloginNewUser();
        }

        static void Test01_VerifiregistartionAndReloginNewUser()
        {
            UserData newUser = new UserData();
            newUser.LastName = UserData.GenerateText(10);
            newUser.FirstName = UserData.GenerateText(10);
            newUser.Adres = UserData.GenerateText(20);
            newUser.PosCode = UserData.GenerateNumber(5);
            newUser.Country = "United States";
            newUser.City = UserData.GenerateText(10);
            newUser.Email = UserData.GenerateText(8) + "@mail.ru";
            newUser.Mobile = UserData.GenerateNumber(8);
            newUser.Pass = "Qwerty123!";

            ChromeDriver driver = new ChromeDriver(@"C:\web_config");
            driver.Navigate().GoToUrl("http://localhost:8082/litecart/");
            driver.FindElement(By.CssSelector(".content>form>table>tbody>tr>td>a")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys(newUser.FirstName);
            driver.FindElement(By.Name("lastname")).SendKeys(newUser.LastName);
            driver.FindElement(By.Name("address1")).SendKeys(newUser.Adres);
            driver.FindElement(By.Name("postcode")).SendKeys(newUser.PosCode);
            driver.FindElement(By.Name("city")).SendKeys(newUser.City);
            driver.FindElement(By.ClassName("select2-selection__rendered")).Click();
            driver.FindElement(By.ClassName("select2-search__field")).SendKeys(newUser.Country + "\n");
            driver.FindElement(By.Name("email")).SendKeys(newUser.Email);
            driver.FindElement(By.Name("phone")).SendKeys("+380" + newUser.Mobile);
            driver.FindElement(By.Name("password")).SendKeys(newUser.Pass);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(newUser.Pass);
            driver.FindElement(By.Name("create_account")).Click();


            if (driver.FindElement(By.CssSelector(".notice.success")).Text == "Your customer account has been created.")
            {
                Console.WriteLine("User is SignUp");
            }

            //driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            if (driver.FindElement(By.CssSelector(".notice.success")).Text == "You are now logged out.")
            {
                Console.WriteLine("User is Logout");
            }

            driver.FindElement(By.Name("email")).SendKeys(newUser.Email);
            driver.FindElement(By.Name("password")).SendKeys(newUser.Pass);
            driver.FindElement(By.Name("login")).Click();

            if (driver.FindElement(By.CssSelector(".notice.success")).Text == String.Format("You are now logged in as {0} {1}.", newUser.FirstName, newUser.LastName))
            {
                Console.WriteLine("User is SignIn");
            }

             
            Thread.Sleep(5000);
            driver.Quit();
        }


    }

    class UserData
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Adres { get; set; }
        public string PosCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Pass { set; get; }

        private static Random random = new Random();
        public static string GenerateText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string GenerateNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
