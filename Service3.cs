using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService2
{
    public partial class Service3 : ServiceBase
    {
        Process toDoTest = new Process();
        public Service3()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                var finalpath = Path.GetDirectoryName(path) + "/ToDoMVCtutorial.exe";
                //var finalpath = "C:\\Develop\\ToDoMVCtutorial\\bin\\Release\\net6.0\\publish\\ToDoMVCtutorial.exe";
                toDoTest.StartInfo.FileName = finalpath;
                toDoTest.Start();
            }
            catch (Exception ex)
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                var finalpath = Path.GetDirectoryName(path) + "/ToDoMVCtutorial.exe";
                using (StreamWriter file = new StreamWriter(finalpath))
                {
                    file.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnStop()
        {
            try
            {
                var processes = Process.GetProcesses().Where(pr => pr.ProcessName == "ToDoMVCtutorial");
                foreach (Process proc in processes)
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                var finalpath = Path.GetDirectoryName(path) + "/ToDoMVCtutorial.exe";
                using (StreamWriter file = new StreamWriter(finalpath))
                {
                    file.WriteLine(ex.Message);
                }
            }
        }
    }
}
