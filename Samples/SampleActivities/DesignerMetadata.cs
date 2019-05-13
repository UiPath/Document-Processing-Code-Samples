using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using SampleActivities.Basic.DataExtraction;
using SampleActivities.Basic.DocumentClassification;

namespace SampleActivities
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();

            // Designers
            var simpleClassifierDesigner = new DesignerAttribute(typeof(SimpleClassifierDesigner));
            var simpleExtractorDesigner = new DesignerAttribute(typeof(SimpleExtractorDesigner));

            //Categories
            var classifierCategoryAttribute = new CategoryAttribute("Sample Classifiers");
            var extractorCategoryAttribute = new CategoryAttribute("Sample Extractors");

            builder.AddCustomAttributes(typeof(SimpleClassifier), classifierCategoryAttribute);
            builder.AddCustomAttributes(typeof(SimpleClassifier), simpleClassifierDesigner);

            builder.AddCustomAttributes(typeof(SimpleExtractor), extractorCategoryAttribute);
            builder.AddCustomAttributes(typeof(SimpleExtractor), simpleExtractorDesigner);

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
