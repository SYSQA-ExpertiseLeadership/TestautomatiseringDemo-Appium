﻿using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;

namespace Sysqa.Demo.SkyPoints.Utilities
{
    public static class Driver
    {
        private static IWebDriver _driverInstance;
        private static DesiredCapabilities cap;

        public static IWebDriver Instance
        {
            get
            {
                if (_driverInstance == null)
                {

                    cap = new DesiredCapabilities();
                    cap.SetCapability("platformName", "Android");
                    cap.SetCapability("udid", "dd87c16a");
                    cap.SetCapability("deviceName", $"SysqaDemo");
                    cap.SetCapability("app", Settings.TestSettingsInstance.ApkLocation);
                    cap.SetCapability("fullReset", "false");
                    cap.SetCapability("automationName", "appium");

                    _driverInstance = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), cap);
                }
                return _driverInstance;
            }
        }

        public static void CleanUp()
        {
            _driverInstance.Dispose();
            cap = null;
        }
    }
}
