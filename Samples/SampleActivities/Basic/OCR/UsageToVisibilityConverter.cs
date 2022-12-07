#if !NETPORTABLE_UIPATH
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using UiPath.OCR.Contracts.Scrape;

namespace SampleActivities.Basic.OCR
{
    internal class UsageToVisibilityConverter : IValueConverter
    {
        public Visibility Document { get; set; }

        public Visibility Screen { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ScrapeEngineUsages? usage = value as ScrapeEngineUsages?;
            if (usage == null)
            {
                return null;
            }

            return usage == ScrapeEngineUsages.Screen ? Screen : Document;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
#endif