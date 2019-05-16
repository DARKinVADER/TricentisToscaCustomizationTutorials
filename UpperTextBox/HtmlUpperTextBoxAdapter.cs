using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html;
using Tricentis.Automation.Engines.Technicals.Html;

namespace UpperTextBox
{
    [SupportedTechnical(typeof(IHtmlInputElementTechnical))]
    public class HtmlUpperTextBoxAdapter : HtmlTextBoxAdapter
    {
        protected HtmlUpperTextBoxAdapter(IHtmlInputElementTechnical htmlTechnical, Validator validator) : base(htmlTechnical, validator)
        {
        }

        public override string Text { get => base.Text; set => base.Text = value?.ToUpper(); }
    }
}
