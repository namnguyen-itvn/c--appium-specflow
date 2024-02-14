using System;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;

namespace c_sharp_appium_specflow.Drivers
{
	class MyTestApp
	{
        public static AndroidDriver<AndroidElement> Driver;

        public static void InitializeDriver()
        {
            AppiumOptions driverOptions = SetCapabilities();
            Driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions);
            var contexts = ((IContextAware) Driver).Contexts;
            string webviewContext = null;
            for (var i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    break;
                }
            }

        ((IContextAware) Driver).Context = webviewContext;
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public static void Quit()
        {
            Driver.Quit();
        }

        private static AppiumOptions SetCapabilities()
        {
            var appPath = @"/Users/namnguyen/c--appium-specflow/c-sharp-appium-specflow/apps/AndroidTestApp.apk";
            AppiumOptions driverOptions = new AppiumOptions();
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.App, appPath);
            //set up web view context for web view app
            driverOptions.AddAdditionalCapability("chromedriverExecutable", "/Users/namnguyen/c--appium-specflow/c-sharp-appium-specflow/driver/chromedriver");

            return driverOptions;
        }

        public static AndroidElement ScrollToElementByResourceId(String id)
        {
            return Driver.FindElementByAndroidUIAutomator($"new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().resourceId(\"{id}\"))");
        }
        public static AndroidElement ScrollToElementByText(String text)
        {
            return Driver.FindElementByAndroidUIAutomator($"new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"{text}\"))");
        }
    }
}

