using System;
using System.Linq;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html;
using Tricentis.Automation.Engines.Technicals.Html;
using Tricentis.Automation.Engines.Technicals.References;

namespace IdentifyByLabel.Properties
{
    [SupportedTechnical(typeof(IHtmlInputElementTechnical))]
    internal class HtmlLabelIdTextBoxAdapter : HtmlTextBoxAdapter
    { 
        public HtmlLabelIdTextBoxAdapter(IHtmlInputElementTechnical htmlTechnical, Validator validator) : base(htmlTechnical, validator)
        {
            try
            {
                IHtmlDivTechnical parentDiv = GetParentNode<IHtmlDivTechnical>(htmlTechnical);

                parentDiv = GetParentNode<IHtmlDivTechnical>(parentDiv);

                IHtmlDivTechnical child = parentDiv.Children.Get<IHtmlDivTechnical>().FirstOrDefault();
                IHtmlLabelTechnical label = child.Children.Get<IHtmlLabelTechnical>().FirstOrDefault();
                LabelId = label.Title;
            }
            catch (NullReferenceException)
            {
                validator.Fail();
            }
        }

        private static T GetParentNode<T>(IHtmlElementTechnical element) where T : class, IHtmlElementTechnical
        {
            return element.GetTechnicalType().GetProperty("parentNode").Get<IObjectReference>(element).Get<T>();
        }
        public string LabelId { get; private set; }
    }
}
