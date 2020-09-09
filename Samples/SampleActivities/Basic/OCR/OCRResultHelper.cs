using System.Drawing;
using System.Linq;
using UiPath.OCR.Contracts.DataContracts;

namespace SampleActivities.Basic.OCR
{
    internal static class OCRResultHelper
    {
        internal static OCRResult FromText(string text)
        {
            return new OCRResult
            {
                Text = text,
                Words = text.Split(' ').Select(word => new Word
                {
                    Text = word,
                    Characters = word.Select(ch => new Character
                    {
                        Char = ch,
                        PolygonPoints = new[] { new PointF(100, 100), new PointF(200, 100), new PointF(100, 200), new PointF(200, 200), }
                    }).ToArray()
                }).ToArray(),
                Confidence = 0,
                SkewAngle = 0
            };
        }
    }
}
