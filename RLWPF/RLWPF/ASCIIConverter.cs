using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RLWPF
{
    /// <summary>
    /// Convert objects into an ASCII character representation
    /// </summary>
    public class ASCIIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ElementCollection)
            {
                var mC = (ElementCollection)value;
                var top = mC.LastOrDefault();
                if (top != null)
                {
                    ASCIIStyle style = top.GetData<ASCIIStyle>();
                    if (style != null) return style.Symbol;
                    else return "?";
                }
            }
            return ".";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
