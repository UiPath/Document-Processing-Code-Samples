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
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
