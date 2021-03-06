﻿using NUnit.Framework;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Sysqa.Demo.SkyPoints.Utilities
{
    public class BaseTest
    {
        [OneTimeTearDown]
        public async Task endLogger()
        {
            var filePath = $"{Settings.TestSettingsInstance.LogFolder}logs\\TestRun@{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.txt";

            Directory.CreateDirectory($"{Settings.TestSettingsInstance.LogFolder}logs");
            var fileCreate = File.Create(filePath);
            fileCreate.Dispose();

            StreamWriter file = new StreamWriter(filePath);
            foreach (var logLine in TestLogger.Log)
            {
                await file.WriteLineAsync(logLine);
            }

            file.Close();
        }

        [TearDown]
        protected void EndTestCase()
        {
            LogErrors();
            Driver.CleanUp();
        }

        protected void LogErrors()
        {
            var TestName = $"{TestContext.CurrentContext.Test.Name}@{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
            MakeScreenShot(TestName);
        }

        protected void MakeScreenShot(string name)
        {
            Directory.CreateDirectory($"{Settings.TestSettingsInstance.LogFolder}\\Screenshots");

            var screen = Driver.Instance.GetScreenshot();
            screen.SaveAsFile($"{Settings.TestSettingsInstance.LogFolder}\\Screenshots\\{name}.png", ImageFormat.Png);
        }
    }
}
