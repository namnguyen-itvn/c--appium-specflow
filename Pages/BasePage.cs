using System;
using c_sharp_appium_specflow;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace c_sharp_appium_specflow
{
	public class BasePage
	{
        public AndroidDriver<AndroidElement> Driver;
        public WebDriverWait wait;
        public BasePage()
		{
            Driver = MyTestApp.Driver;
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

	}
}

