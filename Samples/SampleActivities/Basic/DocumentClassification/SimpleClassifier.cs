using System.Activities;
using System.Linq;
using UiPath.DocumentProcessing.Contracts.Classification;
using UiPath.DocumentProcessing.Contracts.Dom;
using UiPath.DocumentProcessing.Contracts.Results;

namespace SampleActivities.Basic.DocumentClassification
{
    public class SimpleClassifier : ClassifierCodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            string text = DocumentText.Get(context);
            Document document = DocumentObjectModel.Get(context);
            string documentPath = DocumentPath.Get(context);
            ClassifierDocumentType[] documentTypes = DocumentTypes.Get(context);

            ClassifierResult.Set(context, ComputeResult(text, document, documentPath, documentTypes));
        }

        private ClassifierResult ComputeResult(string text, Document document, string documentPath, ClassifierDocumentType[] documentTypes)
        {
            if (documentTypes == null || !documentTypes.Any())
            {
                return null;
            }

            // return first document type as a sample
            var classificationResult = new ClassificationResult(
                documentTypes.First().DocumentTypeId,
                document.DocumentId,
                0.85f,
                1f,
                new ResultsContentReference(0, 1000, new ResultsValueTokens[0]),
                new ResultsDocumentBounds(1, 1000));

            return new ClassifierResult
            {
                Classifications = new[] { classificationResult }
            };
        }
    }
}
