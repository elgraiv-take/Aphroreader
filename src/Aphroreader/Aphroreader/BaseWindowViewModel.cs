using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Elgraiv.Aphroreader
{
    class BaseWindowViewModel:BindableBase
    {
        public ICommand CloseCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand RestoreCommand { get; }

        private object _content;
        public object Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
        private object _title;
        public object Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private WindowState _windowState=WindowState.Normal;
        public WindowState WindowState
        {
            get => _windowState;
            set => SetProperty(ref _windowState, value);
        }

        private bool _isCloseRequested = false;
        public bool IsCloseRequested
        {
            get => _isCloseRequested;
            set => SetProperty(ref _isCloseRequested, value);
        }

        public BaseWindowViewModel()
        {
            CloseCommand = new DelegateCommand(CloseWindow);
            MaximizeCommand = new DelegateCommand(MaximizeWindow);
            MinimizeCommand = new DelegateCommand(MinimizeWindow);
            RestoreCommand = new DelegateCommand(RestoreWindow);
        }

        private void RestoreWindow()
        {
            WindowState = WindowState.Normal;
        }

        private void MinimizeWindow()
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow()
        {
            WindowState = WindowState.Maximized;
        }

        private void CloseWindow()
        {
            IsCloseRequested = true;
            IsCloseRequested = false;
        }
    }
}
