using System;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfApplication1.Controls
{
    public class TextHeaderToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var header = value as DataGridColumnHeader;
            if (header != null)
            {
                //var hasContent = ((DataGridColumnHeader) value).;
                var headerText = header.DataContext.ToString();
                //var txt = this.ToString();
                if (headerText.StartsWith("WpfApplication1")) return false;
                return true;               
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
