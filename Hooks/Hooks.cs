using System;
using c_sharp_appium_specflow;
using TechTalk.SpecFlow;

namespace c_sharp_appium_specflow
{

    [Binding]
    public class Hooks
    {
        [Before]
        public void Initialize()
        {
            Console.WriteLine("Initializing Driver");
            MyTestApp.InitializeDriver();
            Console.WriteLine("Driver started");
        }

        [After]
        public void Quit()
        {
            Console.WriteLine("Quitting Driver");
            MyTestApp.Quit();
        }
    }
}

