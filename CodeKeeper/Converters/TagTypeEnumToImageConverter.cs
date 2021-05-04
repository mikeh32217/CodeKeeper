using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using static CodeKeeper.Model.TagInfo;

namespace CodeKeeper.Converters
{
    [ValueConversion(typeof(TokenType), typeof(Image))]
    public class TagTypeEnumToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string token = value.ToString();

            switch((TokenType)value)
            {
                case TokenType.Snippet:
                    return new BitmapImage(new Uri("pack://application:,,,/Images/snippet.png"));
                case TokenType.Token:
                    return new BitmapImage(new Uri("pack://application:,,,/Images/tag.png"));
                default:
                    return new BitmapImage(new Uri("pack://application:,,,/Images/undefined.png"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
