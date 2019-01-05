using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Aphroreader.Main
{
    [Util.DataTemplateTarget(typeof(MainWindowView))]
    class MainWindowViewModel:ViewModelBase
    {
        private ObservableCollection<ContentThumbnailViewModel> _contents;
        public ReadOnlyObservableCollection<ContentThumbnailViewModel> Contents { get; } 

        public MainWindowViewModel()
        {
            _contents = new ObservableCollection<ContentThumbnailViewModel>();
            Contents = new ReadOnlyObservableCollection<ContentThumbnailViewModel>(_contents);


        }
    }
}
