﻿using Microsoft.Playwright;

namespace PlaywrigthUITests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class UITestFixture
    {
        public IPage page { get; private set; }
        private IBrowser browser;

        [SetUp]
        public async Task Setup()
        {
            var playwrightDriver = await Playwright.CreateAsync();
            browser = await playwrightDriver.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, // Set to false to run the browser in non-headless mode
                Channel = "chrome",
                Args = new List<string>
                {
                    "--incognito",
                    "--fullscreen",
                    "--start-maximized",
                    "--window-position=0,0",
                    "--window-size=1920,1080"
                }
            });
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1900, Height = 970 },
            });
            page = await context.NewPageAsync();
        }

        [OneTimeSetUp]

        [TearDown]
        public async Task Teardown()
        {
            if (page != null)
            {
                await page.CloseAsync();
            }
            if (browser != null)
            {
                await browser.CloseAsync();
            }
        }
    }
}
