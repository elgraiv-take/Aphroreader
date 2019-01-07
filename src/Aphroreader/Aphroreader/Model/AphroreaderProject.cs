using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Aphroreader.Model
{
    [JsonObject("project", MemberSerialization = MemberSerialization.OptIn)]
    public class AphroreaderProject : BindableBase
    {
        [JsonProperty("contents")]
        public ObservableCollection<AphContent> Contents { get; }

        public string FilePath { get; set; }

        public AphroreaderProject()
        {
            Contents = new ObservableCollection<AphContent>();
        }

        public void OnLoaded()
        {
            foreach(var content in Contents)
            {
                content.PropertyChanged += NewContent_PropertyChanged;
            }
        }

        public void AddContent(AphContent newContent)
        {
            newContent.PropertyChanged += NewContent_PropertyChanged;
            Contents.Add(newContent);
            Save();
        }

        private void NewContent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Save();
        }

        public void Save()
        {
            using (var writer = new StreamWriter(FilePath))
            {
                writer.Write(JsonConvert.SerializeObject(this));
            }
        }
    }
}
