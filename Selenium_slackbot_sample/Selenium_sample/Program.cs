using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SlackAPI;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DateTime lastUpdate = DateTime.Now;


            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;       //크롬 콘솔창 숨기기

            var options = new ChromeOptions();
            //options.AddArgument("--window-position=-32000,-32000"); 
            options.AddArgument("--headless");                    //윈도우창 위치값을 화면밖으로 조정

            using (var driver = new ChromeDriver(driverService, options))
            {
                for (int i = 991; i < 1042; i++)
                {
                    driver.Navigate().GoToUrl("https://www.dhlottery.co.kr/store.do?method=topStore&pageGubun=L645");

                    var no = driver.FindElement(By.Id("drwNo"));

                    var opts = no.FindElements(By.TagName("option"));

                    var opt = opts.FirstOrDefault(x => x.Text == i.ToString());
                    if (opt != null)
                        opt.Click();

                    var schVal = driver.FindElement(By.Id("schVal"));
                    schVal.SendKeys($@"동탄");
                    schVal.SendKeys(Keys.Enter);

                    var btn = driver.FindElement(By.XPath("/html/body/div[3]/section/div/div[2]/div/div[2]/a"));
                    btn.Click();

                    var table1 = driver.FindElement(By.XPath("/html/body/div[3]/section/div/div[2]/div/div[3]/table"));
                    var tbody1 = table1.FindElement(By.TagName("tbody"));
                    var trs1 = tbody1.FindElements(By.TagName("tr"));
                    foreach (var item in trs1)
                    {
                        if (item.Text is "조회 결과가 없습니다.")
                            continue;

                        Console.WriteLine($@"{i}회차 1등 : {item.Text}");
                    }

                    var table2 = driver.FindElement(By.XPath("/html/body/div[3]/section/div/div[2]/div/div[4]/table"));
                    var tbody2 = table2.FindElement(By.TagName("tbody"));
                    var trs2 = tbody2.FindElements(By.TagName("tr"));
                    foreach (var item in trs2)
                    {
                        if (item.Text is "조회 결과가 없습니다.")
                            continue;

                        Console.WriteLine($@"{i}회차 2등 : {item.Text}");
                    }


                }
                driver.Close();
            }
            Thread.Sleep(10000000);
        }

    }
}
