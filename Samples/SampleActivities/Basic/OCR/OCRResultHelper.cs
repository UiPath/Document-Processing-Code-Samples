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
                Words = text.Split(' ').Select((word, i) => new Word
                {
                    Text = word,
                    Characters = word.Select(ch => new Character
                    {
                        Char = ch,
                        PolygonPoints = new[] { new PointF((i + 1) * 100, (i + 1) * 100), new PointF((i + 1) * 200, (i + 1) * 100), new PointF((i + 1) * 100, (i + 1) * 200), new PointF((i + 1) * 200, (i + 1) * 200), }
                    }).ToArray()
                }).ToArray(),
                Confidence = 0,
                SkewAngle = 0
            };
        }
    }
}
