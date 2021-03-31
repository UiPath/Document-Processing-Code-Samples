using UiPath.OCR.Contracts.Scrape;

namespace SampleActivities.Basic.OCR
{
    public class SampleOCRScrapeFactory : OCRScrapeFactory
    {
        public override OCRScrapeBase CreateEngine(ScrapeEngineUsages usage)
        {
            return new SampleOCRScrape(new SimpleOCREngine(), usage);
        }
    }
}
