using Elgraiv.Aphroreader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Elgraiv.Aphroreader.Main
{
    [Util.DataTemplateTarget(typeof(MainWindowView))]
    class MainWindowViewModel:BindableBase
    {
        private ObservableCollection<ContentThumbnailViewModel> _contents;
        public ReadOnlyObservableCollection<ContentThumbnailViewModel> Contents { get; }

        public ICommand FileDroppedCommand { get; }

        private MainModel _model;

        public MainWindowViewModel(MainModel model)
        {
            _model = model;

            _contents = new ObservableCollection<ContentThumbnailViewModel>();
            Contents = new ReadOnlyObservableCollection<ContentThumbnailViewModel>(_contents);

            FileDroppedCommand = new DelegateCommand(FileDropped);
        }

        private void FileDropped(object args)
        {
        }
    }
}
