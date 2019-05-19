using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Adapters.Lists;
using Tricentis.Automation.Engines.Technicals.Html;

namespace ImplementingComboBoxTricentisSolution.Html.Adapter
{
    [SupportedTechnical(typeof(IHtmlInputElementTechnical))]
    public class DevExpressComboBoxAdapter : AbstractHtmlDomNodeAdapter<IHtmlInputElementTechnical>, IComboBoxAdapter
    {
        private static readonly Regex idRegex = new Regex("ContentHolder_[^_]*?_I");
        public DevExpressComboBoxAdapter(IHtmlInputElementTechnical technical, Validator validator) : base(technical, validator)
        {
            validator.AssertTrue(() => CheckTechnical(technical));
        }

        private bool CheckTechnical(IHtmlInputElementTechnical technical)
        {
            string className = technical.ClassName;
            string id = technical.Id;

            return !string.IsNullOrEmpty(className) && className.StartsWith("dxeEditArea") && idRegex.IsMatch(id);
        }
    }
}
