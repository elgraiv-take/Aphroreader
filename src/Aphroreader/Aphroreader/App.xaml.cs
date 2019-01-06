using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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

            if (!CheckProjectDirectory())
            {
                Shutdown();
                return;
            }

            Start();
        }

        private void Start()
        {

            var mainModel = new Model.MainModel();
            mainModel.LoadProject(Aphroreader.Properties.Settings.Default.DefaultProjectPath);
            var mainWindow = new BaseWindow();
            mainWindow.DataContext = new BaseWindowViewModel()
            {
                Content = new Main.MainWindowViewModel(mainModel),
                Title = "Aphroreader"
            };
            MainWindow = mainWindow;
            mainWindow.Show();
            mainWindow.Activate();
        }

        private bool CheckProjectDirectory()
        {
            var path=Aphroreader.Properties.Settings.Default.DefaultProjectPath;
            if (File.Exists(path))
            {
                return true;
            }
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dataDirectory = Path.Combine(appData, "Elgraiv", "Aphroreader");
            var newProjectPath = Path.Combine(dataDirectory, "default.aphproj");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
            using (var saveDialog=new System.Windows.Forms.SaveFileDialog() {
                FileName=newProjectPath,
                Filter="proj|*.aphproj"
            })
            {
                var result=saveDialog.ShowDialog();
                if(result!= System.Windows.Forms.DialogResult.OK)
                {
                    return false;
                }
                var newPath = saveDialog.FileName;
                using (var writer = new StreamWriter(newPath))
                {
                    writer.Write(JsonConvert.SerializeObject(new Model.AphroreaderProject()));
                }
                Aphroreader.Properties.Settings.Default.DefaultProjectPath = newPath;
                Aphroreader.Properties.Settings.Default.Save();
            }
            return true;
        }

        private void InitializeDataTemplate()
        {
            Util.DataTemplateUtil.RegisterDataTemplate(Resources, ResourceAssembly);
        }


    }
}
