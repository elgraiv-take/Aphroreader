using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public AphroreaderProject()
        {
            Contents = new ObservableCollection<AphContent>();
        }

        public void AddContent(AphContent newContent)
        {
            Contents.Add(newContent);
        }
    }
}
