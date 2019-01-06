using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Elgraiv.Aphroreader.Util
{
    public class FileDropBehavior: Behavior<UIElement>
    {
        public ICommand Command
        {
            get { return GetValue(CommandProperty) as ICommand; }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(FileDropBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += AssociatedObject_Drop;
            AssociatedObject.PreviewDragOver += AssociatedObject_PreviewDragOver;

        }


        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Drop -= AssociatedObject_Drop;
            AssociatedObject.PreviewDragOver -= AssociatedObject_PreviewDragOver;
        }
        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            var command = Command;
            if (command == null)
            {
                return;
            }
            if(e.Data.GetData(DataFormats.FileDrop) is string[] files)
            {
                if (command.CanExecute(null))
                {
                    command.Execute(files);
                }
            }
        }
        private void AssociatedObject_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) != null)
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }
    }
}
