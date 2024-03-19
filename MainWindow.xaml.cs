using IPC_default_base_NET4;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.TeamFoundation.Work.WebApi;
using System.Threading;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using System.Net.NetworkInformation;
using static Microsoft.TeamFoundation.Client.CommandLine.Options;
using System.IO;
using System.Net;

namespace IPC_default
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CMDGetIP();
           // CMDGetNetCardName();
            //ReadFile();

        }
        #region 寫入檔案路徑(已棄用)
        //string path = "C:\\NetCardComboList.ini";
        #endregion
        #region 初始化CMD(非視窗)
        static Process cmd = new Process();
        static void SetCMD()
        {
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
        }
        #endregion
        #region 初始化GUIcmd(視窗)
        static Process GUIcmd = new Process();
        static void SetGUIcmd()
        {
            GUIcmd.StartInfo.FileName = "cmd.exe";
            GUIcmd.StartInfo.RedirectStandardInput = false;
            GUIcmd.StartInfo.RedirectStandardOutput = false;
            GUIcmd.StartInfo.RedirectStandardError = false;
            GUIcmd.StartInfo.CreateNoWindow = false;
            GUIcmd.StartInfo.UseShellExecute = true;
        }
        #endregion
        #region Tag 預設功能表
        #region 按鈕 關閉防火牆
        //TODO:OK 關閉防火牆 邏輯判斷
        private void OFF_Firewall(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "/c netsh advfirewall set currentprofile state off" +
                "&netsh advfirewall set Domainprofile state off" +
                "&netsh advfirewall set Privateprofile state off" +
                "&netsh advfirewall set Publicprofile state off";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            bool a = po[0].Contains("確定。");
            bool b = po[2].Contains("確定。");
            bool c = po[4].Contains("確定。");
            bool d = po[6].Contains("確定。");
            if (a == true && b == true && c == true && d == true)
            {
                MessageBox.Show("關閉防火牆\n執行成功", "執行結果");
            }
            else
                MessageBox.Show("關閉防火牆\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 開啟防火牆
        private void ON_Firewall(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "/c netsh advfirewall set currentprofile state on" +
                "&netsh advfirewall set Domainprofile state on" +
                "&netsh advfirewall set Privateprofile state on" +
                "&netsh advfirewall set Publicprofile state on";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            bool a = po[0].Contains("確定。");
            bool b = po[2].Contains("確定。");
            bool c = po[4].Contains("確定。");
            bool d = po[6].Contains("確定。");
            if (a == true && b == true && c == true && d == true)
            {
                MessageBox.Show("開啟防火牆\n執行成功", "執行結果");
            }
            else
                MessageBox.Show("開啟防火牆\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 關閉windows update
        private void OFF_WU(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
            //@"/c sc stop WaasMedicSvc&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\WaasMedicSvc / v Start / f / t REG_DWORD / d 4&sc stop wuauserv&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\wuauserv / v Start / f / t REG_DWORD / d 4&sc stop UsoSvc&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\UsoSvc / v Start / f / t REG_DWORD / d 4";
            @"/c REG ADD HKLM\SYSTEM\CurrentControlSet\Services\WaasMedicSvc /v Start /f /t REG_DWORD /d 4&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\wuauserv /v Start /f /t REG_DWORD /d 4&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\UsoSvc /v Start /f /t REG_DWORD /d 4";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            //bool a = po[0].Contains("操作順利完成。");
            //if (a == true)
            //{
            //    MessageBox.Show("開啟遠端桌面\n執行成功", "執行結果");
            //}
            //else
            //    MessageBox.Show("開啟遠端桌面\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            MessageBox.Show("關閉更新\n執行成功\n" + _out, "執行結果");
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 開啟windows update
        private void ON_WU(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
            //@"/c sc stop WaasMedicSvc&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\WaasMedicSvc / v Start / f / t REG_DWORD / d 4&sc stop wuauserv&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\wuauserv / v Start / f / t REG_DWORD / d 4&sc stop UsoSvc&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\UsoSvc / v Start / f / t REG_DWORD / d 4";
            @"/c REG ADD HKLM\SYSTEM\CurrentControlSet\Services\WaasMedicSvc /v Start /f /t REG_DWORD /d 2&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\wuauserv /v Start /f /t REG_DWORD /d 2&REG ADD HKLM\SYSTEM\CurrentControlSet\Services\UsoSvc /v Start /f /t REG_DWORD /d 2";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            //bool a = po[0].Contains("操作順利完成。");
            //if (a == true)
            //{
            //    MessageBox.Show("開啟遠端桌面\n執行成功", "執行結果");
            //}
            //else
            //    MessageBox.Show("開啟遠端桌面\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            MessageBox.Show("開啟更新\n執行成功\n" + _out, "執行結果");
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 按鈕 開啟網路探索(包含資料處理範例)
        //TODO:OK 開啟網路探索 邏輯判斷
        private void OPEN_NET_DISCOVERY(object sender, RoutedEventArgs e)
        {
            //string True_log = " 1&2> C:\\True.txt";
            //string False_log = " 2> C:\\False.txt";
            //string strCmdText = "/K netsh advfirewall firewall set rule group = \"網路探索\" new enable= Yes" + True_log + False_log +
            //"&netsh advfirewall firewall set rule group = \"檔案及印表機共用\" new enable= Yes" + True_log + False_log;
            {
                //System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                //string scan = "確定。";
                // string po =_out.Substring(0, 4);//string顯示指定範圍
                //cmd.StandardInput.Close();
                //MessageBox.Show(po[6].Substring(0, 2) + po[2]);
                //String out2 = Convert.ToString(po);//強制轉型  
                #region 後台執行CMD程序
                SetCMD();
                #endregion
                #region CMD 指令
                cmd.StartInfo.Arguments =
                    "/c netsh advfirewall firewall set rule group = \"網路探索\" new enable= Yes" +
                    "&netsh advfirewall firewall set rule group = \"檔案及印表機共用\" new enable= Yes";
                cmd.Start();
                #endregion
                #region 資料處理與判斷
                string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
                string[] po = _out.Split('\n');//檢測換行並分割
                #region 字串比對
                bool a = po[2].Contains("確定。");
                bool b = po[6].Contains("確定。");
                if (a == true && b == true)
                {
                    MessageBox.Show("開啟網路探索\n執行成功", "執行結果");
                }
                else
                    MessageBox.Show("開啟網路探索\n執行失敗\n" + _out, "執行結果");
                #endregion
                Debug.Print(_out);//debug視窗輸出
                cmd.StandardInput.Flush();//清除資源
                cmd.WaitForExit();//等待運行結束
                #endregion
            }
        }
        #endregion
        #region 按鈕 關閉網路探索(包含資料處理範例)
        //TODO:OK 開啟網路探索 邏輯判斷
        private void OFF_NET_DISCOVERY(object sender, RoutedEventArgs e)
        {
            //string True_log = " 1&2> C:\\True.txt";
            //string False_log = " 2> C:\\False.txt";
            //string strCmdText = "/K netsh advfirewall firewall set rule group = \"網路探索\" new enable= Yes" + True_log + False_log +
            //"&netsh advfirewall firewall set rule group = \"檔案及印表機共用\" new enable= Yes" + True_log + False_log;
            {
                //System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                //string scan = "確定。";
                // string po =_out.Substring(0, 4);//string顯示指定範圍
                //cmd.StandardInput.Close();
                //MessageBox.Show(po[6].Substring(0, 2) + po[2]);
                //String out2 = Convert.ToString(po);//強制轉型  
                #region 後台執行CMD程序
                SetCMD();
                #endregion
                #region CMD 指令
                cmd.StartInfo.Arguments =
                    "/c netsh advfirewall firewall set rule group = \"網路探索\" new enable= No" +
                    "&netsh advfirewall firewall set rule group = \"檔案及印表機共用\" new enable= No";
                cmd.Start();
                #endregion
                #region 資料處理與判斷
                string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
                string[] po = _out.Split('\n');//檢測換行並分割
                #region 字串比對
                bool a = po[2].Contains("確定。");
                bool b = po[6].Contains("確定。");
                if (a == true && b == true)
                {
                    MessageBox.Show("關閉網路探索\n執行成功", "執行結果");
                }
                else
                    MessageBox.Show("關閉網路探索\n執行失敗\n" + _out, "執行結果");
                #endregion
                Debug.Print(_out);//debug視窗輸出
                cmd.StandardInput.Flush();//清除資源
                cmd.WaitForExit();//等待運行結束
                #endregion
            }
        }
        #endregion
        #region 按鈕 開啟遠端桌面
        //TODO:OK 開啟遠端桌面 邏輯判斷
        private void OPEN_RM(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "/C netsh advfirewall firewall set rule group = \"遠端桌面\" new enable= Yes&reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\" /v fDenyTSConnections /t REG_DWORD /d 0 /f \r\n&";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            MessageBox.Show("開啟遠端桌面\n已執行\n" + "執行結果:\n" + _out);
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 按鈕 關閉遠端桌面
        //TODO:OK 開啟遠端桌面 邏輯判斷
        private void OFF_RM(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "/C netsh advfirewall firewall set rule group = \"遠端桌面\" new enable= No&reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\" /v fDenyTSConnections /t REG_DWORD /d 4 /f \r\n&";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            MessageBox.Show("關閉遠端桌面\n已執行\n" + "執行結果:\n" + _out);
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
        }
        #endregion
        #region 按鈕 關閉休眠/睡眠
        //TODO:NOK 關閉休眠/睡眠 
        private void OFF_SLEEP(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "powercfg /change disk-timeout-dc 0" +
                "&powercfg /change monitor-timeout-dc 0" +
                "&powercfg /change standby-timeout-dc 0" +
                "&powercfg /change hibernate-timeout-dc 0" +
                "&powercfg /change disk-timeout-ac 0" +
                "&powercfg /change monitor-timeout-ac 0" +
                "&powercfg /change standby-timeout-ac 0" +
                "&ppowercfg /change hibernate-timeout-ac 0" +
                "&powercfg -h off";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            //bool a = po[0].Contains("操作順利完成。");
            //if (a == true)
            //{
            //    MessageBox.Show("開啟遠端桌面\n執行成功", "執行結果");
            //}
            //else
            //    MessageBox.Show("開啟遠端桌面\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            MessageBox.Show("開啟睡眠/休眠\n已執行\n" + "執行結果:\n" + _out);
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 按鈕 開啟休眠/睡眠
        private void ON_SLEEP(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "powercfg /change disk-timeout-dc 120" +
                "&powercfg /change monitor-timeout-dc 15" +
                "&powercfg /change standby-timeout-dc 30" +
                "&powercfg /change hibernate-timeout-dc 60" +
                "&powercfg /change disk-timeout-ac 120" +
                "&powercfg /change monitor-timeout-ac 15" +
                "&powercfg /change standby-timeout-ac 30" +
                "&ppowercfg /change hibernate-timeout-ac 60" +
                "&powercfg -h on";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            //bool a = po[0].Contains("操作順利完成。");
            //if (a == true)
            //{
            //    MessageBox.Show("開啟遠端桌面\n執行成功", "執行結果");
            //}
            //else
            //    MessageBox.Show("開啟遠端桌面\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            MessageBox.Show("開啟休眠/睡眠\n已執行\n" + "執行結果:\n" + _out);
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 按鈕 關閉限制空白密碼登入
        //TODO:NOK 關閉限制空白密碼登入 
        private void OFF_White_PW(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c REG ADD ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa"" /f /v LimitBlankPasswordUse /t ""REG_DWORD"" /d 0";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            //bool a = po[0].Contains("操作順利完成。");
            //if (a == true)
            //{
            //    MessageBox.Show("開啟遠端桌面\n執行成功", "執行結果");
            //}
            //else
            //    MessageBox.Show("開啟遠端桌面\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            MessageBox.Show("關閉密碼\n執行成功" + _out, "執行結果");
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 按鈕 開啟限制空白密碼登入
        //TODO:NOK 關閉限制空白密碼登入 
        private void ON_White_PW(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c REG ADD ""HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa"" /f /v LimitBlankPasswordUse /t ""REG_DWORD"" /d 1";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            #region 字串比對
            //bool a = po[0].Contains("操作順利完成。");
            //if (a == true)
            //{
            //    MessageBox.Show("開啟遠端桌面\n執行成功", "執行結果");
            //}
            //else
            //    MessageBox.Show("開啟遠端桌面\n執行失敗\n" + _out, "執行結果");
            #endregion
            Debug.Print(_out);
            //MessageBox.Show(_out, "執行結果");
            // Debug.Print(Convert.ToString(c)+ "\n"+Convert.ToString(d));//debug視窗輸出
            MessageBox.Show("執行成功" + _out, "執行結果");
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 網卡設定
        #region 按鈕 開啟限制空白密碼登入
        //TODO:NOK 關閉限制空白密碼登入 
        private void Netcard_Config(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c start control Ncpa.cpl";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #endregion
        #region 按鈕 一鍵執行(未完成)
        //TODO:OK 一鍵執行
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //OFF_Firewall(sender, e);
            //OFF_SLEEP(sender, e);
            //OPEN_NET_DISCOVERY(sender, e);
            //OFF_White_PW(sender, e);
            ////OPEN_IIS(sender, e);
            //OPEN_RM(sender, e);
        }
        #endregion
        #endregion
        #region Tag 網路工具
        #region IP Config
        private void IP_Config(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "/c ipconfig -all";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            //MessageBox.Show("開啟網路探索\n執行完成\n" + _out, "執行結果");
            TextBox.Text = _out;
            Debug.Print(_out);//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 遠端桌面連線
        private void Remote_Start(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                "/c mstsc";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            //MessageBox.Show("開啟網路探索\n執行完成\n" + _out, "執行結果");
            TextBox.Text = _out;
            Debug.Print(_out);//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region Ping IP
        private async void Start_Ping(object sender, RoutedEventArgs e)
        {
            TextBox.Clear();
            try
            {
                
                   
                for (int i = 1; i < 5; i++)
                {

                    Ping pingSender = new Ping();
                    PingReply reply = pingSender.Send(Ping_Input1.Text.Trim(), 120);//第一個引數為ip地址,第二個引數為ping的時間
                    //TextBox.Text = Convert.ToString(reply.Status);
                    Debug.Print(Convert.ToString(reply.Status));
                    async void Delay()
                    {
                       if (Convert.ToString(reply.Status) == "Success")
                    {
                        TextBox.Text = TextBox.Text+"連線正常" + i+"\n";

                    }
                    else { TextBox.Text = TextBox.Text + "連線異常" + i + "\n"; }
                    }
                    Delay();
                    await Task.Delay(1000);
                   
        
                }
            }
            catch (Exception ex)
            {
                TextBox.Text = Convert.ToString("處理異常，請檢察設定\n\n" + ex);
            }
            }
        
        #endregion
        #region IP介面卡配置
        #region 讀取網卡寫入List
        string NicName = string.Empty;
        string CardName = string.Empty;
        string value = string.Empty;
        string CardListIn = string.Empty;
        string CardListOut = string.Empty;
        string NowSelectCard = string.Empty;
        string IPV4 = string.Empty;
        int FindPCIeCard;
        int FindUSBCard;
        int FindGigabitCard;
        int FindIntelCard;
        List<string> SaveNicList = new List<string>();
        public void ReadNetCard()
        {
            //List<string> CardList = new List<string>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // bug in your original code right here is `=`
                // you proably meant to do something like value += ", " + nic.Name
                // which would not work well with listbox Items collection
                NicName = nic.Name;
                CardName = nic.Description;
                // var getIP = Dns.GetHostEntry( Dns.GetHostAddresses());
                //  Debug.Print(PrintIP.ToString());
                //IPV4 = nic.GetIPv4Statistics().BytesReceived.ToString();
                FindUSBCard = CardName.IndexOf("USB");
                FindPCIeCard = CardName.IndexOf("PCIe");
                FindGigabitCard = CardName.IndexOf("Gigabit");
                FindIntelCard = CardName.IndexOf("Intel(R)");

                if (FindUSBCard != -1 | FindPCIeCard != -1 | FindGigabitCard != -1 | FindIntelCard != -1)
                {
                    NetCardCombo.Items.Add(">" + NicName + "\n" + CardName);
                    SaveNicList.Add(NicName);
                }
            }
        }
        #endregion
        #region 網卡Combo讀取選擇數值並發送
        private void NetCardCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string NowSelectValue = Convert.ToString(NetCardCombo.SelectedValue).Trim();
            foreach (var NicListToString in SaveNicList.ToArray())
            {
                if (NowSelectValue != null)
                {
                    int SearchNic = NowSelectValue.IndexOf(NicListToString);
                    // Debug.Print(SearchNic.ToString());
                    if (SearchNic != -1)
                    {

                        NowSelectCard = NicListToString.Trim();
                        break;
                    }
                }
            }
        }
        #endregion
        #region 寫入ComboList(自動刷新)
        private void NetCardFresh(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                NetCardCombo.Items.Clear();
                ReadNetCard();
                //NetCardCombo.Items.Add(CardListIn);
                //Debug.Print(NicName + "\n" + CardName
            }
        }
        #endregion
        #region DHCP設定
        private void Set_DHCP_Click(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c netsh interface ip set address """ + NowSelectCard + @""" dhcp";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            //MessageBox.Show("開啟網路探索\n執行完成\n" + _out, "執行結果");
            TextBox.Text = "執行成功\n" + _out;
            Debug.Print(_out);//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region IP設定
        private void Set_IP(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c netsh interface ip set address """ + NowSelectCard + @""" static " + SetIP_Input0.Text.Trim();
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            //MessageBox.Show("開啟網路探索\n執行完成\n" + _out, "執行結果");
            TextBox.Text = "執行成功\n" + _out;
            Debug.Print(_out);//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region 取得網卡目前IP並顯示在TextBlock
        private void Get_Card_IP(object sender, RoutedEventArgs e)
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c netsh interface ipv4 show config name= " + @"""" + NowSelectCard.Trim() + @"""";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            //MessageBox.Show("開啟網路探索\n執行完成\n" + _out, "執行結果");
            TextBox.Text = "執行成功\n" + _out;
            Debug.Print(_out);//debug視窗輸出
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
            #endregion
        }
        #endregion
        #region CMD使用IP取得網卡名稱(暫不使用)
        public void CMDGetNetCardName()
        {
            #region 後台執行CMD程序
            SetCMD();
            #endregion
            #region CMD 指令
            cmd.StartInfo.Arguments =
                @"/c ipconfig -all";
            cmd.Start();
            #endregion
            #region 資料處理與判斷
            string _out = cmd.StandardOutput.ReadToEnd();//獲取CMD輸出返回值
            string[] po = _out.Split('\n');//檢測換行並分割
            List<string> NetCardList = new List<string>();
            int CMDFindIntelName = 0;
            int CMDFindUSBCardName = 0;
            int CMDFindGigabitCardName = 0;
            int CMDFindPCIeName = 0;
            int CMDFindInfo = 0;
            for (int i = 0; i < po.Length; i++)
            {
                CMDFindIntelName = po[i].IndexOf("Intel(R)");
                CMDFindUSBCardName = po[i].IndexOf("USB");
                CMDFindGigabitCardName = po[i].IndexOf("Gigabit");
                CMDFindPCIeName = po[i].IndexOf("PCIe");

                //po[i].Substring(37);
                if (CMDFindIntelName != -1 | CMDFindGigabitCardName != -1 | CMDFindPCIeName != -1 | CMDFindUSBCardName != -1)
                {
                    for (int ii = i; i < po.Length; ii++)
                    {
                        CMDFindInfo = po[ii].IndexOf("IPv4");
                        //Console.WriteLine(CMDFindInfo);
                        if (CMDFindInfo != -1)
                        {

                            int FindInfo = po[i].IndexOf(":");
                            NetCardList.Add(po[i].Substring(FindInfo + 1).Trim());
                            int FindLike = po[ii].IndexOf(":");
                            int FindKey = po[ii].IndexOf("(");
                            NetCardList.Add(po[ii].Substring(FindLike + 1, FindKey - FindLike - 1).Trim());
                            //NetCardList.Add(po[i].Substring(37));
                            break;
                        }
                    }
                }

                //foreach (var print in NetCardList.ToArray())
                //{
                //    Console.WriteLine(print);
                //}

            }
            cmd.StandardInput.Flush();//清除資源
            cmd.WaitForExit();//等待運行結束
        }
        //Debug.Print(_out);

        #endregion
        #endregion
        #endregion
        #region 打開CMD
        private void OPEN_CMD(object sender, RoutedEventArgs e)
        {
            Process Cmd = new Process();

            Cmd.StartInfo.FileName = "C:\\windows\\system32\\cmd.exe";
            Cmd.Start();

        }
        #endregion


        #endregion
        #region Tag SSH連線
        #region SSH連線(CMD版本)
        private void OPEN_SSH(object sender, RoutedEventArgs e)
        {
            if (SSH_Input_IP != null & SSH_Input_Port != null)
            {
                SetGUIcmd();
                GUIcmd.StartInfo.Arguments =
                   "/k c:\\windows\\sysnative\\openssh\\ssh.exe root@" + SSH_Input_IP.Text + " -p " + SSH_Input_Port.Text;
                GUIcmd.Start();
                //GUIcmd.WaitForExit();
            }
        }
        #region 清除SSH金鑰衝突
        private void Fix_SSH_Error(object sender, RoutedEventArgs e)
        {
            string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            try
            {
                Directory.Delete(MyDocumentsPath + "\\.ssh", true);//刪除.ssh金鑰目錄
                TextBox.Text = "\n------------------------\nSSH金鑰已清除\n------------------------";
            }
            catch (Exception ex)
            {
                TextBox.Text = ex + "\n------------------------\nSSH金鑰已清除\n------------------------";
            }
        }
        #endregion
        #endregion
        #endregion
        #region 空的宣告
        #region ip設定(cmd版本)
        #region 後台執行cmd程序
        //setcmd();
        #endregion
        #region cmd 指令
        #region 獲取硬體名稱
        //string ip = setip_input0.text.trim();
        //cmd.startinfo.arguments =
        //    "/c ipconfig -all ";
        //cmd.start();
        #endregion
        #endregion
        //string _out = cmd.standardoutput.readtoend().trim('\r');//獲取cmd輸出返回值
        //                                                        //messagebox.show("開啟網路探索\n執行完成\n" + _out, "執行結果");
        //string[] po = _out.split('\n');//檢測換行並分割
        //int findrows;
        //int findusbadapterrows = 0;
        //int findpcieadapterrows = 0;
        //int findcard1 = 0;
        //int findcard2 = 0;
        //int findcardnum1 = 0;
        //int findcardnum2 = 0;
        //list<string> adapterlist = new list<string>();
        //for (findrows = 0; findrows < po.length; findrows++)
        //{
        //    //debug.print(findrows);
        //    findusbadapterrows = po[findrows].indexof("usb");
        //    findpcieadapterrows = po[findrows].indexof("pcie");
        //    //debug.print(findusbadapterrows.tostring());
        //    if (findusbadapterrows != -1)
        //    {
        //        string usbout = po[findrows].substring(findusbadapterrows);
        //        //debug.print(usbout);
        //        for (findcardnum1 = findrows; findcardnum1 >0; findcardnum1--)
        //        {
        //            findcard1 = po[findcardnum1].lastindexof("卡");
        //            debug.print(findcard1.tostring());
        //            if (findcard1 != -1& findcard1 ==4  )
        //            {
        //                 adapterlist.add(po[findrows-(findrows- findcardnum1)]+usbout);
        //                break;
        //            } 
        //        }
        //    }
        //   else if (findpcieadapterrows != -1)
        //    {
        //        string pcieout = po[findrows].substring(findpcieadapterrows);
        //        for (findcardnum2 = findrows; findcardnum2 > 0; findcardnum2--)
        //        {
        //            findcard2 = po[findcardnum2].lastindexof("");
        //            if (findcard2 != -1)
        //            {
        //                adapterlist.add(po[findrows - (findrows - findcardnum2)]+pcieout);
        //               break;
        //            }
        //        }
        //    }
        //}
        //foreach (var printnum in adapterlist.toarray())
        //{
        //    textbox.text = textbox.text+ printnum;
        //}
        ////debug.print(po[findadapterrows]);
        ////textbox.text = "執行成功\n" + _out;
        ////debug.print(_out);//debug視窗輸出
        //cmd.standardinput.flush();//清除資源
        //cmd.waitforexit();//等待運行結束
        #endregion
        private void Ping_Input(object sender, TextChangedEventArgs e)
        {
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void SetIP_Input(object sender, TextChangedEventArgs e)
        {
        }
        private void TextBox0(object sender, TextChangedEventArgs e)
        {
        }
        #endregion


    }
}

