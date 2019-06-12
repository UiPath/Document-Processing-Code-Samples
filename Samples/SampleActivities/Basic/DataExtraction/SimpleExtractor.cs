using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UiPath.DocumentProcessing.Contracts;
using UiPath.DocumentProcessing.Contracts.DataExtraction;
using UiPath.DocumentProcessing.Contracts.Dom;
using UiPath.DocumentProcessing.Contracts.Results;
using UiPath.DocumentProcessing.Contracts.Taxonomy;

namespace SampleActivities.Basic.DataExtraction
{
    public class SimpleExtractor : ExtractorCodeActivity
    {
        public override Task<ExtractorDocumentTypeCapabilities[]> GetCapabilities()
        {
            return Task.FromResult(new ExtractorDocumentTypeCapabilities[0]);
        }

        protected override void Execute(CodeActivityContext context)
        {
            ExtractorDocumentType documentType = ExtractorDocumentType.Get(context);
            ResultsDocumentBounds documentBounds = DocumentBounds.Get(context);
            string text = DocumentText.Get(context);
            Document document = DocumentObjectModel.Get(context);
            string documentPath = DocumentPath.Get(context);

            ExtractorResult.Set(context, ComputeResult(documentType, documentBounds, text, document, documentPath));
        }

        private ExtractorResult ComputeResult(ExtractorDocumentType documentType, ResultsDocumentBounds documentBounds, string text, Document document, string documentPath)
        {
            var extractorResult = new ExtractorResult();
            var resultsDataPoints = new List<ResultsDataPoint>();

            // example of reporting a value with derived parts
            Field firstDateField = documentType.Fields.FirstOrDefault(f => f.Type == FieldType.Date);
            if (firstDateField != null)
            {
                // if any field is of type Date return first word on first page as reference and report Jan 1st 2002 as value
                resultsDataPoints.Add(CreateDateFieldDataPoint(firstDateField, document));
            }

            // example of report a value with no textual reference (only visual reference)
            Field firstBooleanField = documentType.Fields.FirstOrDefault(f => f.Type == FieldType.Boolean);
            if (firstBooleanField != null)
            {
                // if any field is of type Boolean return "true" with a visual reference from pixel position (50, 100) and width 200 and height 300.
                resultsDataPoints.Add(CreateBooleanFieldDataPoint(firstBooleanField, document));
            }

            // example of table value
            Field firstTableField = documentType.Fields.FirstOrDefault(f => f.Type == FieldType.Table);
            if (firstTableField != null)
            {
                // if any field is of type Table return a table with headers referencing the first N words and 2 rows referencing the next N * 2 words.
                // N will be the number of columns in the table field.
                resultsDataPoints.Add(CreateTableFieldDataPoint(firstTableField, document));
            }

            extractorResult.DataPoints = resultsDataPoints.ToArray();
            return extractorResult;
        }

        private static ResultsDataPoint CreateDateFieldDataPoint(Field firstDateField, Document document)
        {
            // TODO
            var derivedFields = ResultsDerivedField.CreateDerivedFieldsForDate(1, 1, 2002);
            var firstDateValue = CreateResultsValue(0, document, "Jan 1st 2002");
            firstDateValue.DerivedFields = derivedFields;

            return new ResultsDataPoint(
                firstDateField.FieldId,
                firstDateField.FieldName,
                firstDateField.Type,
                new[] { firstDateValue });
        }

        private static ResultsDataPoint CreateBooleanFieldDataPoint(Field firstBooleanField, Document document)
        {
            var booleanToken = new ResultsValueTokens(0, (float)document.Pages[0].Size.Width, (float)document.Pages[0].Size.Height, new[] { Box.CreateChecked(50, 100, 200, 300) });
            var reference = new ResultsContentReference(0, 0, new[] { booleanToken });
            var firstBooleanValue = new ResultsValue("Yes", reference, 0.9f, 1f);

            return new ResultsDataPoint(
                firstBooleanField.FieldId,
                firstBooleanField.FieldName,
                firstBooleanField.Type,
                new[] { firstBooleanValue });
        }

        private static ResultsDataPoint CreateTableFieldDataPoint(Field firstTableField, Document document)
        {
            int i = 0;
            var headerCells = firstTableField.Components.Select(c => new ResultsDataPoint(c.FieldId, c.FieldName, c.Type, new[] { CreateResultsValue(i++, document) }));

            var firstRowCells = firstTableField.Components.Select(c => new ResultsDataPoint(c.FieldId, c.FieldName, c.Type, new[] { CreateResultsValue(i++, document) }));
            var secondRowCells = firstTableField.Components.Select(c => new ResultsDataPoint(c.FieldId, c.FieldName, c.Type, new[] { CreateResultsValue(i++, document) }));

            var tableValue = ResultsValue.CreateTableValue(firstTableField, headerCells, new[] { firstRowCells, secondRowCells }, 0.9f, 1f);

            return new ResultsDataPoint(
                firstTableField.FieldId,
                firstTableField.FieldName,
                firstTableField.Type,
                new[] { tableValue });
        }

        private static ResultsValue CreateResultsValue(int wordIndex, Document document, string value = null)
        {
            var word = document.Pages[0].Sections.SelectMany(s => s.WordGroups).SelectMany(w => w.Words).ToArray()[wordIndex];
            var wordValueToken = new ResultsValueTokens(word.IndexInText, word.Text.Length, 0, (float)document.Pages[0].Size.Width, (float)document.Pages[0].Size.Height, new[] { word.Box });
            var reference = new ResultsContentReference(word.IndexInText, word.Text.Length, new[] { wordValueToken });

            return new ResultsValue(value ?? word.Text, reference, 0.9f, word.OcrConfidence);
        }
    }
}
