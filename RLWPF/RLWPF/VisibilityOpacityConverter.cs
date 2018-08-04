using Nucleus.Game;
using Nucleus.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RLWPF
{
    public class VisibilityOpacityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 1)
            {
                MapCell cell = (MapCell)values[0];
                var mA = (ICellMap<int>)values[1];
                if (mA != null)
                {
                    return mA[cell.Index] / 10.0;
                }
            }
            return 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
