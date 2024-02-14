using System;
using System.Drawing;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace c_sharp_appium_specflow.Pages
{
    public class HomePage : BasePage
    {
        //Declare Android Element 
        public AndroidElement ColorButton => Driver.FindElementById("com.lambdatest.proverbial:id/color");
        public AndroidElement TextWithColor => Driver.FindElementById("com.lambdatest.proverbial:id/Textbox");
        public AndroidElement NotificationButton => Driver.FindElementById("com.lambdatest.proverbial:id/notification");
        public AndroidElement TextButton => Driver.FindElementById("com.lambdatest.proverbial:id/Text");
        public AndroidElement GeoLocationButton => Driver.FindElementById("com.lambdatest.proverbial:id/geoLocation");
        public AndroidElement ToastButton => Driver.FindElementById("com.lambdatest.proverbial:id/toast");
        public AndroidElement SpeedTestButton => Driver.FindElementById("com.lambdatest.proverbial:id/speedTest");
        public AndroidElement HomeMenu => Driver.FindElementById("com.lambdatest.proverbial:id/buttonPage");
        public AndroidElement WebViewMenu => Driver.FindElementById("com.lambdatest.proverbial:id/webview");
        public AndroidElement NotificationText => Driver.FindElementByXPath("//*[@text='Test Notification']");

        private byte[] initialScreenshot;

        public HomePage()
        {
        }

        public void TakeScreenShot()
        {
            initialScreenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray;

        }

        public bool CompareScreenshots(byte[] screenshot1, byte[] screenshot2)
        {
            // Convert the byte arrays to Bitmap objects
            Bitmap initialBitmap = new Bitmap(new MemoryStream(screenshot1));
            Bitmap finalBitmap = new Bitmap(new MemoryStream(screenshot2));

            // Compare the two Bitmap objects to check for visual differences
            // You can use image comparison libraries or custom logic to compare the screenshots
            // Here's an example of a simple pixel-level comparison:
            bool colorChanged = false;
            for (int x = 0; x < initialBitmap.Width; x++)
            {
                for (int y = 0; y < initialBitmap.Height; y++)
                {
                    Color initialPixelColor = initialBitmap.GetPixel(x, y);
                    Color finalPixelColor = finalBitmap.GetPixel(x, y);
                    if (initialPixelColor != finalPixelColor)
                    {
                        colorChanged = true;
                        break;
                    }
                }
                if (colorChanged)
                    break;
            }

            return colorChanged;
        }

        public bool IsToastFound()
        {
            string pageSource = Driver.PageSource;
            if (pageSource.Contains("android.widget.Toast index=\"1\""))
                return true;
            else
                return false;

        }

        public string GetToastMessageText()
        {
            string pageSource = Driver.PageSource;
            string pattern = @"<android\.widget\.Toast.*?text=""(.*?)"".*?>";

            Match match = Regex.Match(pageSource, pattern);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        public bool IsNotificationDisplayed => NotificationText.Displayed;
    }
}

