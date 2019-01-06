using Elgraiv.Aphroreader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Elgraiv.Aphroreader.Viewer
{
    [Util.DataTemplateTarget(typeof(ContentViewerView))]
    public class ContentViewerViewModel:BindableBase
    {
        private string _currentImage;
        public string CurrentImage
        {
            get => _currentImage;
            set => SetProperty(ref _currentImage, value);
        }

        public int Count { get; }

        private int _index;
        public int Index
        {
            get => _index;
            private set => SetProperty(ref _index, value);
        }

        public ICommand NextCommand { get; }
        public ICommand BackCommand { get; }

        private AphContent _model;

        public ContentViewerViewModel(AphContent content)
        {
            _model = content;
            Count = content.ImageList.Count;
            CurrentImage =content.ImageList[0];
            Index = 0;

            NextCommand = new DelegateCommand(PageNext);
            BackCommand = new DelegateCommand(PageBack);
        }

        private void PageNext()
        {
            if (Index + 1 < Count)
            {
                Index++;
                CurrentImage = _model.ImageList[Index];
            }
        }
        private void PageBack()
        {
            if (Index > 0)
            {
                Index--;
                CurrentImage = _model.ImageList[Index];
            }
        } 
        
    }
}
