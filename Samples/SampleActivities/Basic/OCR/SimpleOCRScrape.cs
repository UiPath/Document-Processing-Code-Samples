using System.Collections.Generic;
using UiPath.OCR.Contracts.Activities;
using UiPath.OCR.Contracts.Scrape;

namespace SampleActivities.Basic.OCR
{
    // Extend OCRScrapeBase to allow your OCR engine to display custom user controls when integrating
    // with wizards such as Screen Scraping or Template Manager.
    internal class SampleOCRScrape : OCRScrapeBase
    {
        private readonly SimpleScrapeControl _sampleScrapeControl;

        public override ScrapeEngineUsages Usage { get; } = ScrapeEngineUsages.Document | ScrapeEngineUsages.Screen;

        public SampleOCRScrape(IOCRActivity ocrEngineActivity, ScrapeEngineUsages usage) : base(ocrEngineActivity)
        {
            _sampleScrapeControl = new SimpleScrapeControl(usage);
        }

        public override ScrapeControlBase GetScrapeControl()
        {
            return _sampleScrapeControl;
        }

        public override Dictionary<string, object> GetScrapeArguments()
        {
            return new Dictionary<string, object>
            {
                { nameof(SimpleOCREngine.CustomInput), _sampleScrapeControl.SampleInput }
            };
        }
    }
}
