using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

// Page to test: http://samples.gwtproject.org/samples/Showcase/Showcase.html?hl=pl#!CwTree
namespace ImplementingTree.Html.Adapter
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class GwtTreeAdapter : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITreeAdapter
    {
        public GwtTreeAdapter(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            validator.AssertTrue(() => technical.ClassName == "gwt-Tree");
        }
    }
}
