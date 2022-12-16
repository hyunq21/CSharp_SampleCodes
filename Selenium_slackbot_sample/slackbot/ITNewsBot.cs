using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SlackAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slackbot
{
    public class ITNewsBot
    {
        private DateTime _LastUpdate = DateTime.Now;

        public async void RunSlackBot()
        {
            //string platform = (IntPtr.Size == 8) ? "x64" : "x86";
            //string path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "lib", platform);

            //ChromeOptions co = new ChromeOptions();
            //co.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe";

            //var driverService = ChromeDriverService.CreateDefaultService(path, "msedgedriver.exe"); // Edge
            var driverService = ChromeDriverService.CreateDefaultService(); // Chrome

            driverService.HideCommandPromptWindow = true;       //크롬 콘솔창 숨기기

            var options = new ChromeOptions();
            //options.AddArgument("--window-position=-32000,-32000"); 
            options.AddArgument("--headless");                    //윈도우창 위치값을 화면밖으로 조정

            while (true)
            {
                using (var driver = new ChromeDriver(driverService, options))
                {
                    driver.Navigate().GoToUrl("https://www.codingworldnews.com/news/articleList.html?sc_section_code=S1N2&view_type=sm");
                    Console.WriteLine("Url 이동 : {0}", driver.Url);

                    var newslist = driver.FindElement(By.Id("section-list"));
                    var ul = newslist.FindElement(By.TagName("ul"));
                    var lis = newslist.FindElements(By.TagName("li"));

                    foreach (var li in lis)
                    {
                        var ems = li.FindElements(By.TagName("em"));

                        if (!ems.Any())
                            continue;

                        var time = ems[2].Text;
                        if (DateTime.Parse(time) > _LastUpdate)
                        {
                            var h4 = li.FindElement(By.ClassName("titles"));
                            var a = h4.FindElement(By.TagName("a"));
                            var url = a.GetAttribute("href");

                            var slackClient = new SlackTaskClient(Program.TOKEN);
                            var response = await slackClient.PostMessageAsync("#news", $"<{url}|{h4.Text}>");
                        }
                    }
                    driver.Close();
                }

                await Task.Delay(300000);
            }
        }
    }
}
