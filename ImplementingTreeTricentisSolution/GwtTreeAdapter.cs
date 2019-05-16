using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

namespace Customer.Sample.Html.Adapter
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class GwtTreeAdapter : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITreeAdapter
    {
        public GwtTreeAdapter(IHtmlDivTechnical technical, Validator validator)
            : base(technical, validator)
        {
            validator.AssertTrue(() => technical.ClassName == "gwt-Tree");
        }
    }
}