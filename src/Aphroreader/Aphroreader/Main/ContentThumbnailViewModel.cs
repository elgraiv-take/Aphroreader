using Elgraiv.Aphroreader.Dialog;
using Elgraiv.Aphroreader.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Elgraiv.Aphroreader.Main
{
    [Util.DataTemplateTarget(typeof(ContentThumbnailView))]
    class ContentThumbnailViewModel : BindableBase
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
        public ICommand ChooseThumbnailCommand { get; }
        public ICommand EditTitleCommand { get; }

        private AphContent _model;
        public ContentThumbnailViewModel(AphContent model)
        {
            _model = model;
            _model.PropertyChanged += Model_PropertyChanged;


            OpenViewerCommand = new DelegateCommand(OpenViewer);
            ChooseThumbnailCommand = new DelegateCommand(ChooseThumbnail);
            EditTitleCommand=new DelegateCommand(EditTitle);
        }

        private void EditTitle()
        {
            var viewModel = new TitleEditDialogViewModel(Title, _model.DirectoryPath);
            var dialog = new DialogWindow()
            {
                DataContext = viewModel
            };
            var result=dialog.ShowDialog();
            if (result == true)
            {
                Title = viewModel.Title;
            }
        }

        private void ChooseThumbnail()
        {
            using (var dialog = new OpenFileDialog()
            {
                InitialDirectory=Path.GetDirectoryName(ThumbnailPath),
                RestoreDirectory = true,
                FileName = ThumbnailPath,
                Filter = "image|*.png;*.jpg",
            })
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }
                ThumbnailPath = dialog.FileName;
            }
        }

        private void OpenViewer()
        {
            _model.CollectImages();
            var mainWindow = new BaseWindow();
            var baseWindowViewModel = new BaseWindowViewModel();
            baseWindowViewModel.Content = new Viewer.ContentViewerViewModel(_model,baseWindowViewModel);
            mainWindow.DataContext = baseWindowViewModel;
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
