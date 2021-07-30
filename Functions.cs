using System;
using System.Diagnostics;
using System.Collections;
using System.Windows;
using System.IO;
//程序集包含：1，进程,2，IO操作
namespace Functions
{
    public class Functions
    {
        protected string Author { get; } = "枫(FENG)";
    }
    public class FProcess : Functions
    {
        Process process = new Process();
        public ArrayList GetProcessNames()
        {
            ArrayList t_arrayList = new ArrayList();
            foreach (Process p in Process.GetProcesses())
            {
                t_arrayList.Add(p.ProcessName);
            }
            return t_arrayList;
        }
        //return:ArrayList,need:void###返回当前进程的进程名ArrayList（return a ArrayList which includes now processes' names）
        public Process GetProcessByName(string p)//return:Process need:string###通过名字查找正在运行的进程，如果传入的进程不存在，那么返回一个空的
        {
            Process[] pr = Process.GetProcessesByName(p);
            Process b = new Process();
            foreach (Process a in pr)
            {
                b = a;
            }
            return b;
        }
        public bool IsOpen(string p)//判断输入的进程有没有被打开
        {
            ArrayList arrayList = GetProcessNames();
            int t_b = 0;
            foreach (string a in arrayList)
            {
                if (a == p)//p已经被打开
                {
                    t_b = 1;
                }
            }
            if (t_b == 1)//p处于打开的状态
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Kill(string p)//return:bool need:string###关闭选定的进程，成功关闭则true,失败则false ### Kill the process which was given .If it sucesses return true or retuen false
        {

            if (IsOpen(p))//p处于打开的状态
            {
                Process.Start(p);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool OpenProcess(string p)//return:bool need:string###打开制定的进程，成功打开则true,失败则false ###open the process which was given .If it sucesses return true or retuen false
        {
            if (IsOpen(p))//进程已经被打开，不能再打开，返回打开失败
            {
                return false;
            }
            else
            {
                Process.Start(p);
                return true;
            }
        }
        public bool OpenProcess(int p)//return:bool need:int###通过id打开制定的进程，成功打开则true,失败则false ###open the process which was given .If it sucesses return true or retuen false
        {

            var a = Process.GetProcessById(p);
            string str = a.ProcessName;
            if (IsOpen(str))
            {
                Process.Start(str);
                return true;
            }
            else
            {
                return false;
            }
        }
        public double Runtime_seconds(Process p)//return:double need:Process###传入一个进程，如果程序正在运行，返回已经运行的时间，如果程序已经被关闭，返回他的运行时间
        {
            string name = p.ProcessName;
            double a;
            if (IsOpen(name))//如果程序正在运行
            {
                DateTime st = p.StartTime;
                DateTime nt = DateTime.Now;
                double rt = Math.Round((nt - st).TotalSeconds);
                a = rt;
            }
            else
            {
                DateTime st = p.StartTime;
                DateTime et = p.ExitTime;
                double rt = Math.Round((et - st).TotalSeconds);
                a = rt;
            }
            return a;
        }
        //对上面的方法进行重载，可以传入process或者string的类型
        public double Runtime_seconds(string p)//return:double need:string###传入一个进程，如果程序正在运行，返回已经运行的时间，如果程序已经被关闭，返回他的运行时间
        {
            Process pr = GetProcessByName(p);
            double a = 0.000;
            if (IsOpen(p))//如果程序正在运行
            {

                DateTime st = pr.StartTime;
                DateTime nt = DateTime.Now;
                double rt = Math.Round((nt - st).TotalSeconds);
                a = rt;
            }
            else
            {
                try
                {
                    DateTime st = pr.StartTime;
                    DateTime et = pr.ExitTime;
                    double rt = Math.Round((et - st).TotalSeconds);
                    a = rt;
                }
                catch (System.Exception)
                { }
            }
            return a;
        }
    }
    public class FIO : Functions
    //文件的创建/写入/删除/移动/内容复制到剪切板/文件的合并
    {
        public string FILE_NAME { get; set; }
        public string FILE_NAME1 { get; set; }
        public string FILE_NAME2 { get; set; }

        public FIO(string FILE_NAME)//构造函数，传入所要操作的文件的文件名，请传入包括路径的名称！！！
        {
            this.FILE_NAME = FILE_NAME;
        }
        public FIO(string FILE_NAME1, string FILE_NAME2)
        {
            this.FILE_NAME1 = FILE_NAME1;
            this.FILE_NAME2 = FILE_NAME2;
        }
        private bool FileIsExists(string FILE_NAME_)//判断文件是否存在
        {
            if (File.Exists(FILE_NAME_))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Writetxt_Rewrite(string str)//重写文件
        {
            if (FileIsExists(FILE_NAME))//文件存在
            {
                FileStream fileStream = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.WriteLine(str);
                sw.Close();
                fileStream.Close();
            }
            else
            {
                FileStream fileStream = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fileStream);
                sw.WriteLine(str);
                sw.Close();
                fileStream.Close();
            }
        }

    }
}
