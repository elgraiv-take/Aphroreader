using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Elgraiv.Aphroreader.Util
{
    public class PageFeedBehavior : Behavior<UserControl>
    {
        public ICommand PageNextCommand
        {
            get { return GetValue(PageNextCommandProperty) as ICommand; }
            set { SetValue(PageNextCommandProperty, value); }
        }

        public static readonly DependencyProperty PageNextCommandProperty =
            DependencyProperty.Register("PageNextCommand", typeof(ICommand), typeof(PageFeedBehavior), new UIPropertyMetadata(null));

        public ICommand PageBackCommand
        {
            get { return GetValue(PageBackCommandProperty) as ICommand; }
            set { SetValue(PageBackCommandProperty, value); }
        }

        public static readonly DependencyProperty PageBackCommandProperty =
            DependencyProperty.Register("PageBackCommand", typeof(ICommand), typeof(PageFeedBehavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;

        }

        private void AssociatedObject_Loaded(object sender, EventArgs e)
        {
            var window=Window.GetWindow(AssociatedObject);
            window.KeyDown += AssociatedObject_KeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            var window = Window.GetWindow(AssociatedObject);
            if (window != null)
            {
                window.KeyDown -= AssociatedObject_KeyDown;
            }
        }

        private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    if (PageNextCommand?.CanExecute(null) == true)
                    {
                        PageNextCommand?.Execute(null);
                    }
                    e.Handled = true;
                    break;
                case Key.Left:
                    if (PageBackCommand?.CanExecute(null) == true)
                    {
                        PageBackCommand?.Execute(null);
                    }
                    e.Handled = true;
                    break;
            }
        }
    }
}
