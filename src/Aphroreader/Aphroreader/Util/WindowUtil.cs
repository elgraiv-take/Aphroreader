using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Elgraiv.Aphroreader.Util
{
    public static class WindowUtil
    {
        public static readonly DependencyProperty IsCloseRequestedProperty = DependencyProperty.RegisterAttached(
            "IsCloseRequested",
            typeof(bool),
            typeof(WindowUtil),
            new PropertyMetadata(false, OnCloseRequestedChanged));
        public static bool GetIsCloseRequested(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseRequestedProperty);
        }
        public static void SetIsCloseRequested(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseRequestedProperty, value);
        }

        private static void OnCloseRequestedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if(sender is Window window)
            {
                if (GetIsCloseRequested(window))
                {
                    window.Close();
                }
            }
        }

        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof(bool?),
            typeof(WindowUtil),
            new PropertyMetadata(null, OnDialogResultChanged));

        public static bool GetDialogResult(DependencyObject obj)
        {
            return (bool)obj.GetValue(DialogResultProperty);
        }
        public static void SetDialogResult(DependencyObject obj, bool value)
        {
            obj.SetValue(DialogResultProperty, value);
        }

        private static void OnDialogResultChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Window window)
            {
                window.DialogResult = GetDialogResult(window);
            }
        }
    }
}
