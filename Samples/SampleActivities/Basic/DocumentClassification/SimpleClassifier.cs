using System;
using System.Activities;
using System.Linq;
using UiPath.DocumentProcessing.Contracts.Classification;
using UiPath.DocumentProcessing.Contracts.Dom;
using UiPath.DocumentProcessing.Contracts.Results;

namespace SampleActivities.Basic.DocumentClassification
{
    /// <summary>
    /// This sample classifier takes the first word from the given document page as evidence, returning the first document type as the classification result.
    /// </summary>
    public class SimpleClassifier : ClassifierAsyncCodeActivity
    {
        // Example input argument
        public InArgument<int> EvidencePage { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            return null;
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            string text = DocumentText.Get(context);
            Document document = DocumentObjectModel.Get(context);
            string documentPath = DocumentPath.Get(context);
            ClassifierDocumentType[] documentTypes = DocumentTypes.Get(context);
            int evidencePage = EvidencePage.Get(context);

            ClassifierResult.Set(context, ComputeResult(text, document, documentPath, documentTypes, evidencePage));
        }

        private ClassifierResult ComputeResult(string text, Document document, string documentPath, ClassifierDocumentType[] documentTypes, int evidencePage)
        {
            // example of unsuccessful classification
            if (documentTypes == null || !documentTypes.Any() || document.Pages.Length <= evidencePage)
            {
                return null;
            }

            // take first word from the evidence page in the document and consider it as evidence for the classification
            var firstWord = document.Pages[evidencePage].Sections[0].WordGroups[0].Words[0];
            var firstWordValueToken = new ResultsValueTokens(0, (float)document.Pages[evidencePage].Size.Width, (float)document.Pages[evidencePage].Size.Height, new[] { firstWord.Box });

            // return first document type, with evidecing based on the first word in the document
            var classificationResult = new ClassificationResult(
                // consider the first document type requested
                documentTypes.First().DocumentTypeId,
                // fill in document id from the Document Object Model information
                document.DocumentId,
                // simulate a 85% confidence
                0.85f,
                // take OCR confidence of the words used for evidencing
                firstWord.OcrConfidence,
                // build the evidencing information
                new ResultsContentReference(firstWord.IndexInText, firstWord.Text.Length, new[] { firstWordValueToken }),
                // consider the classification applying to the entire document (all pages)
                new ResultsDocumentBounds(document.Pages.Length, document.Length));

            return new ClassifierResult
            {
                Classifications = new[] { classificationResult }
            };
        }
    }
}
