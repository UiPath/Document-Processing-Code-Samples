using System.Activities.Presentation.Metadata;
using System.ComponentModel;
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

            //Categories
            var classifierCategoryAttribute = new CategoryAttribute("Sample Classifiers");

            builder.AddCustomAttributes(typeof(SimpleClassifier), classifierCategoryAttribute);
            builder.AddCustomAttributes(typeof(SimpleClassifier), simpleClassifierDesigner);

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
