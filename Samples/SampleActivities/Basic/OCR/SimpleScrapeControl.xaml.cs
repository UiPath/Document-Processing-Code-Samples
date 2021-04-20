using UiPath.OCR.Contracts.Scrape;

namespace SampleActivities.Basic.OCR
{
    /// <summary>
    /// Interaction logic for SimpleScrapeControl.xaml
    /// </summary>
    internal partial class SimpleScrapeControl : ScrapeControlBase
    {
        public string SampleInput { get; set; }

        public SimpleScrapeControl()
            : this (ScrapeEngineUsages.Screen)
        {
        }

        public SimpleScrapeControl(ScrapeEngineUsages usage)
        {
            Usage = usage;
            InitializeComponent();
            DataContext = this;
        }
    }
}
