using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using UiPath.OCR.Contracts.Activities;
using UiPath.OCR.Contracts.DataContracts;

namespace SampleActivities.Basic.OCR
{
    public class SimpleOCREngine : OCRCodeActivity
    {
        [Category("Input")]
        [Browsable(true)]
        public override InArgument<Image> Image { get => base.Image; set => base.Image = value; }

        [Category("Output")]
        [Browsable(true)]
        public override OutArgument<string> Text { get => base.Text; set => base.Text = value; }

        [Category("Input")]
        public InArgument<string> CustomInput { get; set; }

        [Category("Output")]
        public OutArgument<string> CustomOutput { get; set; }

        public override Task<OCRResult> PerformOCRAsync(Image image, Dictionary<string, object> options, CancellationToken ct)
        {
            string customInput = options[nameof(CustomInput)] as string;
            string text = $"Text from {nameof(SimpleOCREngine)} with custom input: {customInput}";
            return Task.FromResult(OCRResultHelper.FromText(text));
        }

        protected override void OnSuccess(CodeActivityContext context, OCRResult result)
        {
            CustomOutput.Set(context, $"Custom output: '{result.Text}' has {result.Words.Length} words.");
        }

        protected override Dictionary<string, object> BeforeExecute(CodeActivityContext context)
        {
            return new Dictionary<string, object>
            {
                { nameof(CustomInput), CustomInput.Get(context) }
            };
        }
    }
}
