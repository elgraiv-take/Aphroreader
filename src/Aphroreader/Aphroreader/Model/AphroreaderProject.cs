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
        private ObservableCollection<AphContent> _contents;
        public ReadOnlyObservableCollection<AphContent> Contents { get; }

        public AphroreaderProject()
        {
            _contents = new ObservableCollection<AphContent>();
            Contents = new ReadOnlyObservableCollection<AphContent>(_contents);
        }

        public void AddContent(AphContent newContent)
        {
            _contents.Add(newContent);
        }
    }
}
