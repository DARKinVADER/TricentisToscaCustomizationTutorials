using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html;
using Tricentis.Automation.Engines.Technicals.Html;

namespace ImplementingComboBox.Html.Adapter
{
    [SupportedTechnical(typeof(IHtmlTableTechnical))]
    public class DevExpressComboBoxAdapter : HtmlComboBoxAdapter
    {
        protected DevExpressComboBoxAdapter(IHtmlSelectTechnical htmlTechnical, Validator validator) : base(htmlTechnical, validator)
        {
            validator.AssertTrue(() => htmlTechnical.ClassName == "dxeButtonEditSys dxeButtonEdit_MaterialCompact");
        }
    }
}
