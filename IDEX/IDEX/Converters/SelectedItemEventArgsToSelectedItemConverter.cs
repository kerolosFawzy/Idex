using System;
using System.Globalization;
using Xamarin.Forms;

namespace IDEX.Converters
{
    //i used itemTapped beacuse its better than itemSelected 
    //itemSelected could be clicked once but itemTapped could be clicked many time 
    //Note if you used itemSelected app will not work good if you navigate back 
    public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ItemTappedEventArgs;
            return eventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
