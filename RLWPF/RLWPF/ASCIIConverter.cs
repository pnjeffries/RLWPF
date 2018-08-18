using Nucleus.Game;
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
    public class ASCIIConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is ElementCollection)
            {
                bool visible = false;
                if (values.Length > 2 && values[1] is ICellMap<int> && values[2] is int)
                {
                    int index = (int)values[2];
                    var visiMap = (ICellMap<int>)values[1];
                    int vbility = visiMap[index];
                    if (vbility >= 10) visible = true;
                }
                var mC = (ElementCollection)values[0];
                Element top = null;
                for (int i = mC.Count -1; i >= 0; i--)
                {
                    Element el = mC[i];
                    if (visible || el.HasData<Memorable>())
                    {
                        top = el;
                        break;
                    }
                }
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

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
