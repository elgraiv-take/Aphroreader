using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Elgraiv.Aphroreader.Util
{
    public enum ButtonCornerStyle
    {
        Default,
        System,
    }
    public static class ButtonUtil
    {
        public static readonly DependencyProperty CornerStyleProperty =
        DependencyProperty.RegisterAttached(
            "CornerStyle",
            typeof(ButtonCornerStyle),
            typeof(ButtonUtil),
            new PropertyMetadata(ButtonCornerStyle.Default));

        public static ButtonCornerStyle GetCornerStyle(Button obj)
        {
            return (ButtonCornerStyle)obj.GetValue(CornerStyleProperty);
        }

        public static void SetCornerStyle(Button obj, ButtonCornerStyle value)
        {
            obj.SetValue(CornerStyleProperty, value);
        }
    }
}
