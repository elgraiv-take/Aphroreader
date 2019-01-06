using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elgraiv.Aphroreader.Model
{
    public class MainModel : BindableBase
    {
        private AphroreaderProject _project;
        public AphroreaderProject Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
    }
}
