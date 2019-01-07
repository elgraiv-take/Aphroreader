using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Elgraiv.Aphroreader.Dialog
{
    [Util.DataTemplateTarget(typeof(TextEditDialogView))]
    public class TitleEditDialogViewModel:BindableBase
    {
        public string DialogTitle { get; } = "タイトルを入力";
        public ICommand CloseCommand { get; }
        public ICommand OkCommand { get; }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _selectedCandidate = null;
        public string SelectedCandidate
        {
            get => _selectedCandidate;
            set
            {
                SetProperty(ref _selectedCandidate, value);
                OnCandidateSelected(value);
            }
        }

        public string[] TitleCandidate { get; }

        public TitleEditDialogViewModel(string currentTitle,string path)
        {
            Title = currentTitle;
            CloseCommand = new DelegateCommand(Cancel);
            OkCommand = new DelegateCommand(() => DialogResult = true);
            var dir = Path.GetFullPath(path);
            TitleCandidate = dir.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Reverse().ToArray();
        }

        private void Cancel()
        {
            DialogResult = false;
        }

        private void OnCandidateSelected(string candidate)
        {
            Title = candidate;
        }
    }
}
