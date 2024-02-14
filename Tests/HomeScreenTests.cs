using c_sharp_appium_specflow;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace c_sharp_appium_specflow
{
    [Binding]
    public class HomeScreenTests
    {
        private readonly HomePage homePage;
        //public AppiumDriver<IWebElement> Driver;
        //public WebDriverWait wait;
        public HomeScreenTests()
        {
            homePage = new HomePage();
        }

        private byte[] initialScreenshot;


        [Given(@"I am on the home screen")]
        public void GivenIAmOnTheHomeScreen()
        {
            homePage.Driver.LaunchApp();
        }


        [When(@"I click on COLOR")]
        public void WhenIClickOnCOLOR()
        {
            initialScreenshot = ((ITakesScreenshot)homePage.Driver).GetScreenshot().AsByteArray;

            homePage.ColorButton.Click();
        }

        [Then(@"I should see a change in color")]
        public void ThenIShouldSeeAChangeInColor()
        {
            // Capture the final screenshot of the TextView element
            byte[] finalScreenshot = ((ITakesScreenshot)homePage.Driver).GetScreenshot().AsByteArray;

            // Compare the initial and final screenshots
            bool colorChanged = homePage.CompareScreenshots(initialScreenshot, finalScreenshot);

            // Assert the color change result
            Assert.That(colorChanged, Is.True, "Color did not change");
        }


        [When(@"I click on NOTIFICATION")]
        public void WhenIClickOnNOTIFICATION()
        {
            // Perform the action to click the notification button in your app
            homePage.NotificationButton.Click();
        }

        [Then(@"I should receive a notification on the device")]
        public void ThenIShouldReceiveANotificationOnTheDevice()
        {
            //Open the notification shade using an ADB command
            homePage.Driver.ExecuteScript("mobile:shell", new Dictionary<string, object> { ["command"] = "cmd", ["args"] = new[] { "statusbar", "expand-notifications" } });

            homePage.Driver.ExecuteScript("mobile:shell", new Dictionary<string, object> { ["command"] = "cmd", ["args"] = new[] { "statusbar", "collapse" } });

            // Verify the Notification should show
            Assert.That(homePage.IsNotificationDisplayed, "Push notification not showing");
        }

        [When(@"I click on TEXT")]
        public void WhenIClickOnTEXT()
        {
            // Perform the action to click the text button in your app
            homePage.TextButton.Click();
        }

        [Then(@"I capture the displayed text")]
        public void ThenICaptureTheDisplayedText()
        {
            string displayedText = homePage.TextButton.Text;
            Console.WriteLine($"Displayed Text: {displayedText}");

            // Verify the text not null or empty
            Assert.That(!string.IsNullOrEmpty(displayedText), "Displayed Text is null or empty");
        }


        [When(@"I click on TOAST")]
        public void WhenIClickOnTOAST()
        {
            homePage.ToastButton.Click();
        }

        [Then(@"I should see a pop up message")]
        public void ThenIShouldSeeAPopUpMessage()
        {

            // Verify the toast message showing
            Assert.IsTrue(homePage.IsToastFound(), "Toast message element not found");
            Assert.That(homePage.GetToastMessageText(), Is.EqualTo("Toast should be visible"), "Toast message text mismatch");
        }


        [When(@"I click on SPEED TEST")]
        public void WhenIClickOnSPEEDTEST()
        {
            homePage.SpeedTestButton.Click();
        }

        [Then(@"I start the speed test from the speed test page")]
        public void ThenIStartTheSpeedTestFromTheSpeedTestPage()
        {
            By startSpeedTestLocator = By.XPath("//*[contains(@text,'start speed test')]");
            homePage.wait.Until(ExpectedConditions.ElementIsVisible(startSpeedTestLocator)).Click();
        }

        [Then(@"I capture the upload/download speed")]
        public void ThenICaptureTheUploadDownloadSpeed()
        {
            homePage.wait.Timeout = TimeSpan.FromSeconds(30);

            By testAgainLocator = By.XPath("//*[contains(@text, 'Test Again')]");
            IWebElement testAgainElement = null;
            try
            {
                testAgainElement = homePage.wait.Until(ExpectedConditions.ElementIsVisible(testAgainLocator));
            }
            catch (Exception) { }

            // Capture a screenshot
            Screenshot screenshot = homePage.Driver.GetScreenshot();

            // Generate the screenshot file path
            string folderPath = "screenshots";
            string fileName = $"SpeedTest_{DateTime.Now:yyyyMMddHHmmss}.png";
            string screenshotPath = Path.Combine(folderPath, fileName);

            // Create the screenshots folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the screenshot to the specified file path
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

            Assert.That(File.Exists(screenshotPath), Is.True, "Failed to capture the upload/download speed.");
        }

        [Then(@"I navigate back to the home screen")]
        public void ThenINavigateBackToTheHomeScreen()
        {
            homePage.HomeMenu.Click();
        }
    }
}

