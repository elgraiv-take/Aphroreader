using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Elgraiv.Aphroreader
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeDataTemplate();

            var mainWindow = new BaseWindow();
            mainWindow.DataContext = new BaseWindowViewModel()
            {
                Content=new Main.MainWindowViewModel(),
            };
            MainWindow = mainWindow;
            mainWindow.Show();
        }


        private void InitializeDataTemplate()
        {
            Util.DataTemplateUtil.RegisterDataTemplate(Resources, ResourceAssembly);
        }

        
    }
}
