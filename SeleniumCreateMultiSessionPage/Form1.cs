using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumCreateMultiSessionPage
{
    /// <summary>
    /// 使用 C# Selenium 創建多個Session 獨立頁面 
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Selenium 本身已經處理該問題 ，每個 Object都是獨立的Session Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxMSG.Text = ("建立一個登入頁面" + "\r\n");
            for (int i = 1; i <= 2; i++)
            {
                Task<string> taskCreateChrome = new Task<string>(() =>
                     CreateChrome()
                );
                taskCreateChrome.Start();//開始
                taskCreateChrome.Wait();//等待
                textBoxMSG.Text += ($@"執行次數:{i} 建立一個登入頁面 {taskCreateChrome.Result}" + "\r\n");//取值
            }
        }

        public string CreateChrome()
        {
            IWebDriver driver = new ChromeDriver();
            //開啟網頁
            string url = "https://accounts.google.com/signin/v2/identifier?hl=zh-TW&passive=true&continue=https%3A%2F%2Fwww.google.com.tw%2F&flowName=GlifWebSignIn&flowEntry=ServiceLogin";
            driver.Navigate().GoToUrl(url);
            //隱式等待
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10000);
            Thread.Sleep(2000);
            //輸入帳號
            driver.FindElement(By.Name("identifier")).SendKeys("cap8826@gmail.com");
            Thread.Sleep(2000);
            //繼續下一步
            driver.FindElement(By.Id("identifierNext")).Click();
            Thread.Sleep(2000);
            //輸入密碼
            driver.FindElement(By.Name("password")).SendKeys("XXXXXXXXXXX");
            Thread.Sleep(2000);
            //繼續下一步
            driver.FindElement(By.Id("passwordNext")).Click();
            Thread.Sleep(2000);

            return "登入Google帳戶-結束";
        }
    }
}
