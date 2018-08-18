using Nucleus.Game;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RLWPF
{
    public class ASCIIBlueprintConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cgt = (CellGenerationType)value;
            if (cgt == CellGenerationType.Wall || cgt == CellGenerationType.WallCorner)
            {
                return "█";
            }
            else if (cgt == CellGenerationType.Void) return ".";
            else if (cgt == CellGenerationType.Door) return ".";
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
