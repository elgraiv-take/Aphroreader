using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Aphroreader.Model
{
    public class MainModel : BindableBase
    {
        private AphroreaderProject _project;
        public AphroreaderProject Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        public void AddContents(string[] files)
        {
            if (files.Length < 1)
            {
                return;
            }
            var isDirectory=File.GetAttributes(files[0]).HasFlag(FileAttributes.Directory);
            if (isDirectory)
            {
                var thumb = Directory.GetFiles(files[0])[0];
                AddContent(files[0], thumb);
            }
            else
            {
                var dirPath = Path.GetDirectoryName(files[0]);
                AddContent(dirPath, files[0]);
            }
        }

        private void AddContent(string dirPath,string thumbPath)
        {
            var content = new AphContent()
            {
                DirectoryPath = dirPath,
                ThumbnailPath = thumbPath,
            };
            _project.AddContent(content);
        }

        public void LoadProject(string path)
        {
            if (File.Exists(path))
            {
                using(var reader =new StreamReader(path))
                {
                    Project = JsonConvert.DeserializeObject<AphroreaderProject>(reader.ReadToEnd());
                    Project.FilePath = path;
                    Project.OnLoaded();
                }
            }
            else
            {
                Project = new AphroreaderProject()
                {
                    FilePath = path,
                };
            }
        }
    }
}
