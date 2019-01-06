using Elgraiv.Aphroreader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            _model.PropertyChanged += MainModel_PropertyChanged;
            OnProjectChanged();
        }

        private void MainModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName==nameof(MainModel.Project))
            {
                OnProjectChanged();
            }
        }

        public void OnProjectChanged()
        {
            _model.Project.PropertyChanged += Project_PropertyChanged;
            _model.Project.Contents.CollectionChanged += Contents_CollectionChanged;
            _contents.Clear();
            foreach(var content in _model.Project.Contents)
            {
                _contents.Add(new ContentThumbnailViewModel(content));
            }
        }

        private void Contents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        //中途半端だけどまぁ今は良しとする
                        foreach(var item in e.NewItems)
                        {
                            if(item is AphContent newContent)
                            {
                                var vm = new ContentThumbnailViewModel(newContent);
                                _contents.Add(vm);
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _contents.Clear();
                    break;
            }
        }

        private void Project_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileDropped(object args)
        {
            if(args is string[] files)
            {
                _model.AddContents(files);
            }
        }
    }
}
