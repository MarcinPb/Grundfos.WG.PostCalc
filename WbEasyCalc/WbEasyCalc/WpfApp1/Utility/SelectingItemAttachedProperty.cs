using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Utility
{
    public class SelectingItemAttachedProperty
    {
        public static readonly DependencyProperty SelectingItemProperty = DependencyProperty.RegisterAttached(
            "SelectingItem",
            typeof(object),
            typeof(SelectingItemAttachedProperty),
            new PropertyMetadata(default(object), OnSelectingItemChanged)
            // new PropertyMetadata(null, OnSelectingItemChanged)
            );

        public static object GetSelectingItem(DependencyObject target)
        {
            return (object)target.GetValue(SelectingItemProperty);
        }
        public static void SetSelectingItem(DependencyObject target, object value)
        {
            target.SetValue(SelectingItemProperty, value);
        }

        static void OnSelectingItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            // if (grid == null || grid.SelectedItem == null) return;
            if (grid == null || e.NewValue == null || grid.SelectedItem == null) return;
            var item = e.NewValue;

            //grid.CurrentCellChanged += 

            // Works with .Net 4.5
            grid.Dispatcher.InvokeAsync(() =>
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.SelectedItem, null);
            });

            // Works with .Net 4.0
            //grid.Dispatcher.BeginInvoke((Action)(() =>
            //{
            //    grid.UpdateLayout();
            //    grid.ScrollIntoView(item);
            //}));
        }
    }
}
