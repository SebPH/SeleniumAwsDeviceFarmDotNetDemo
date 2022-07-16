using canopy;
using NUnit.Framework;
using _ = canopy.csharp.canopy;
using Amazon.DeviceFarm;
using Amazon.DeviceFarm.Model;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Amazon;
using System.Threading.Tasks;
using System.Threading;

namespace DeviceFarmDotNetDemo2
{
    public class DeviceFarmTests
    {
        private const string URL1 = "https://the-internet.herokuapp.com/login";
        private const string URL2 = "https://www.saucedemo.com/";
        private const string USERNAME1 = "tomsmith";
        private const string PASSWORD1 = "SuperSecretPassword!";
        private const string USERNAME2 = "standard_user";
        private const string PASSWORD2 = "secret_sauce";
        //private WebDriver driver;
        private RemoteWebDriver driver;

        [SetUp]
        public async Task Setup()
        {
            string myProjectARN = "arn:aws:devicefarm:us-west-2:719655382608:testgrid-project:c6171a16-a730-4bdb-a724-ec10ee2b6cfe";
            var client = new AmazonDeviceFarmClient(RegionEndpoint.USWest2);
            var request = new CreateTestGridUrlRequest()
            {
                ExpiresInSeconds = 300,
                ProjectArn = myProjectARN
            };
            var response = await client.CreateTestGridUrlAsync(request);
            var testGridUrl = new Uri(response.Url);
            var options = new ChromeOptions();
            driver = new RemoteWebDriver(testGridUrl, options);
            driver.Manage().Window.Maximize();
            driver.Manage().Window.FullScreen();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
        }

        [TearDown]
        public void TearDown()
        {
            //_.quit();

            driver.Close();
        }

        [Test]
        [Parallelizable]
        public void UserLoginHerokuApp()
        {
            // =================== LOCAL DRIVER ===============================

            //_.start(types.BrowserStartMode.Chrome);
            //_.pin(types.direction.FullScreen);
            //_.url(URL1);

            //_.write("#username", USERNAME1);
            //_.write("#password", PASSWORD1);
            //_.click(".radius");
            //Assert.True(_.browser.Url.Contains("secure"));

            // =================== REMOTE DRIVER ===============================


            driver.Url = (URL1);

            driver.FindElement(By.Id("username")).SendKeys(USERNAME1);
            driver.FindElement(By.Id("password")).SendKeys(PASSWORD1);
            driver.FindElement(By.CssSelector(".radius")).Click();

            Assert.True(driver.Url.Contains("secure"));
        }

        [Test]
        [Parallelizable]
        public void LogIn()
        {
            // =================== LOCAL DRIVER ===============================

            //_.start(types.BrowserStartMode.Chrome);
            //_.pin(types.direction.FullScreen);
            //_.url(URL2);

            //_.write("#user-name", USERNAME2);
            //_.write("#password", PASSWORD2);
            //_.click(".login-box .submit-button");

            //// validations
            //_.displayed("//span[contains(text(),'Products')]");
            //_.on("https://www.saucedemo.com/inventory.html");

            // =================== REMOTE DRIVER ===============================

            driver.Url = (URL2);

            driver.FindElement(By.Id("user-name")).SendKeys(USERNAME2);
            driver.FindElement(By.Id("password")).SendKeys(PASSWORD2);
            driver.FindElement(By.CssSelector(".login-box .submit-button")).Click();

            Assert.True(driver.FindElement(By.XPath("//span[contains(text(),'Products')]")).Displayed);
            Assert.True(driver.Url.Equals("https://www.saucedemo.com/inventory.html"));
        }

        [Test]
        [Parallelizable]
        public void LeftPaneNav()
        {
            // =================== LOCAL DRIVER ===============================

            //    _.start(types.BrowserStartMode.Chrome);
            //    _.pin(types.direction.FullScreen);
            //    _.url(URL2);

            //    _.write("#user-name", USERNAME2);
            //    _.write("#password", PASSWORD2);
            //    _.click(".login-box .submit-button");

            //    // open-up left pane
            //    _.click(".bm-burger-button");

            //    // validate
            //    _.displayed(".bm-menu");
            //    _.sleep(2);
            //    _.displayed("//*[contains(text(),'All Items')]");
            //    _.displayed("//*[contains(text(),'About')]");
            //    _.displayed("//*[contains(text(),'Logout')]");
            //    _.displayed("//*[contains(text(),'Reset App State')]");

            //    // close pane
            //    _.click(".bm-cross-button");

            // =================== REMOTE DRIVER ===============================
            //driver = new ChromeDriver();

            driver.Url = (URL2);

            driver.FindElement(By.Id("user-name")).SendKeys(USERNAME2);
            driver.FindElement(By.Id("password")).SendKeys(PASSWORD2);
            driver.FindElement(By.CssSelector(".login-box .submit-button")).Click();

            driver.FindElement(By.CssSelector(".bm-burger-button")).Click();
            Assert.True(driver.FindElement(By.CssSelector(".bm-menu")).Displayed);
            Thread.Sleep(2000);
            Assert.True(driver.FindElement(By.XPath("//*[contains(text(),'All Items')]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[contains(text(),'About')]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[contains(text(),'Logout')]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[contains(text(),'Reset App State')]")).Displayed);

            driver.FindElement(By.CssSelector(".bm-cross-button")).Click();
        }

        [Test]
        [Parallelizable]
        public void AddToCart()
        {
            // =================== LOCAL DRIVER ===============================

            //_.start(types.BrowserStartMode.Chrome);
            //_.pin(types.direction.FullScreen);
            //_.url(URL2);

            //_.write("#user-name", USERNAME2);
            //_.write("#password", PASSWORD2);
            //_.click(".login-box .submit-button");

            //// add to cart
            //_.click("#add-to-cart-sauce-labs-backpack");
            //_.click("#add-to-cart-sauce-labs-bike-light");
            //_.click("#add-to-cart-sauce-labs-bolt-t-shirt");

            //// validate cart icon count
            //var cartCount = _.read(".shopping_cart_badge");
            //Assert.AreEqual("3", cartCount);

            //// go to cart
            //_.click(".shopping_cart_badge");
            //_.on("https://www.saucedemo.com/cart.html");

            //// validate items selected count
            //_.count(".cart_item", 3);

            // =================== REMOTE DRIVER ===============================

            //driver = new ChromeDriver();

            driver.Url = (URL2);

            driver.FindElement(By.Id("user-name")).SendKeys(USERNAME2);
            driver.FindElement(By.Id("password")).SendKeys(PASSWORD2);
            driver.FindElement(By.CssSelector(".login-box .submit-button")).Click();

            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Click();
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt")).Click();

            var cartCount = driver.FindElement(By.CssSelector(".shopping_cart_badge")).Text;
            Assert.AreEqual("3", cartCount);

            driver.FindElement(By.CssSelector(".shopping_cart_badge")).Click();
            driver.Manage().Window.Equals("https://www.saucedemo.com/cart.html");
            var count = driver.FindElements(By.CssSelector(".cart_item")).Count;
            Assert.AreEqual(3, count);
        }

        [Test]
        [Parallelizable]
        public void CheckOut()
        {
            // =================== LOCAL DRIVER ===============================

            //_.start(types.BrowserStartMode.Chrome);
            //_.pin(types.direction.FullScreen);
            //_.url(URL2);

            //_.write("#user-name", USERNAME2);
            //_.write("#password", PASSWORD2);
            //_.click(".login-box .submit-button");

            //// add to cart
            //_.click("#add-to-cart-sauce-labs-backpack");

            //// go to cart
            //_.click(".shopping_cart_badge");

            //// checkout
            //_.click("#checkout");

            //// validate checkout: screen 1 (information)
            //_.on("https://www.saucedemo.com/checkout-step-one.html");

            //// add info
            //_.write("[placeholder='First Name']", "Jake");
            //_.write("[placeholder='Last Name']", "Johnson");
            //_.write("[placeholder='Zip/Postal Code']", "12345-7890");
            //_.click("#continue");

            //// validate checkout: screen 2 (overview)
            //_.on("https://www.saucedemo.com/checkout-step-two.html");
            //_.displayed("//div[contains(text(),'Payment Information:')]");
            //_.displayed("//div[contains(text(),'Shipping Information:')]");
            //_.click("#finish");

            //// validate last screen
            //_.displayed("//span[contains(text(),'Checkout: Complete!')]");
            //_.displayed("//h2[contains(text(),'THANK YOU FOR YOUR ORDER')]");

            //// log out
            //_.click(".bm-burger-button");
            //_.sleep(1500);
            //_.displayed("//*[contains(text(),'Logout')]");
            //_.click("//*[contains(text(),'Logout')]");

            //// validate back to home
            //_.on("https://www.saucedemo.com/");

            // =================== REMOTE DRIVER ===============================

            //driver = new ChromeDriver();

            driver.Url = (URL2);

            driver.FindElement(By.Id("user-name")).SendKeys(USERNAME2);
            driver.FindElement(By.Id("password")).SendKeys(PASSWORD2);
            driver.FindElement(By.CssSelector(".login-box .submit-button")).Click();

            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.CssSelector(".shopping_cart_badge")).Click();
            driver.FindElement(By.Id("checkout")).Click();

            driver.Manage().Window.Equals("https://www.saucedemo.com/checkout-step-one.html");

            driver.FindElement(By.CssSelector("[placeholder='First Name']")).SendKeys("Jake");
            driver.FindElement(By.CssSelector("[placeholder='Last Name']")).SendKeys("Johnson");
            driver.FindElement(By.CssSelector("[placeholder='Zip/Postal Code']")).SendKeys("12345-7890");
            driver.FindElement(By.Id("continue")).Click();

            driver.Manage().Window.Equals("https://www.saucedemo.com/checkout-step-two.html");

            Assert.True(driver.FindElement(By.XPath("//div[contains(text(),'Payment Information:')]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//div[contains(text(),'Shipping Information:')]")).Displayed);
            driver.FindElement(By.Id("finish")).Click();

            Assert.True(driver.FindElement(By.XPath("//span[contains(text(),'Checkout: Complete!')]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//h2[contains(text(),'THANK YOU FOR YOUR ORDER')]")).Displayed);

            driver.FindElement(By.CssSelector(".bm-burger-button")).Click();
            Thread.Sleep(2000);
            Assert.True(driver.FindElement(By.XPath("//*[contains(text(),'Logout')]")).Displayed);
            driver.FindElement(By.XPath("//*[contains(text(),'Logout')]")).Click();

            driver.Manage().Window.Equals("https://www.saucedemo.com/");
        }
    }
}
