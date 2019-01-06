using Elgraiv.Aphroreader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Elgraiv.Aphroreader.Main
{
    [Util.DataTemplateTarget(typeof(ContentThumbnailView))]
    class ContentThumbnailViewModel:BindableBase
    {
        public string ThumbnailPath
        {
            get => _model.ThumbnailPath;
            set => _model.ThumbnailPath = value;
        }
        public string Title
        {
            get => _model.Title;
            set => _model.Title = value;
        }

        public ICommand OpenViewerCommand { get; }

        private AphContent _model;
        public ContentThumbnailViewModel(AphContent model)
        {
            _model = model;
            _model.PropertyChanged += Model_PropertyChanged;


            OpenViewerCommand = new DelegateCommand(OpenViewer);
        }

        private void OpenViewer()
        {
            _model.CollectImages();
            var mainWindow = new BaseWindow();
            mainWindow.DataContext = new BaseWindowViewModel()
            {
                Content = new Viewer.ContentViewerViewModel(_model),
                Title = Title
            };
            mainWindow.Show();
            mainWindow.Activate();
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AphContent.ThumbnailPath):
                    RaisePropertyChanged(nameof(ThumbnailPath));
                    break;
                case nameof(AphContent.Title):
                    RaisePropertyChanged(nameof(Title));
                    break;
            }
        }
    }
}
