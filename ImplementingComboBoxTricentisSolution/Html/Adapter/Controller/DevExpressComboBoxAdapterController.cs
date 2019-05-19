using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.AutomationInstructions.TestActions.Associations;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Controllers;
using Tricentis.Automation.Engines.Representations.Attributes;
using Tricentis.Automation.Engines.Technicals;
using Tricentis.Automation.Engines.Technicals.Html;

namespace ImplementingComboBoxTricentisSolution.Html.Adapter.Controller
{
    [SupportedAdapter(typeof(DevExpressComboBoxAdapter))]
    public class DevExpressComboBoxAdapterController : ListAdapterController<DevExpressComboBoxAdapter>
    {
        public DevExpressComboBoxAdapterController(DevExpressComboBoxAdapter contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
        {
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ChildrenBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("Children");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ParentBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("ParentNode");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(DescendantsBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("All");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ListItemsBusinessAssociation businessAssociation)
        {
            yield return new AlgorithmicAssociation("GetListItems");
        }
        protected override IEnumerable<ITechnical> SearchTechnicals(IAlgorithmicAssociation algorithmicAssociation)
        {
            return algorithmicAssociation.AlgorithmName == "GetListItems" ? GetListItems() : base.SearchTechnicals(algorithmicAssociation);
        }

        private IEnumerable<ITechnical> GetListItems()
        {
            List<ITechnical> listItems = new List<ITechnical>();
            string comboBoxId = ContextAdapter.Technical.Id;
            string listItemsTableId = comboBoxId.Substring(0, comboBoxId.Length - 1);
            listItemsTableId += "DDD_L_LBI";
            IHtmlDocumentTechnical htmlDocumentTechnical = ContextAdapter.Technical.Document.Get<IHtmlDocumentTechnical>();

            var tableTechnical = htmlDocumentTechnical.GetById(listItemsTableId).Get<IHtmlTableTechnical>();
            if (tableTechnical == null) return listItems;

            foreach (var item in tableTechnical.Rows.Get<IHtmlRowTechnical>())
            {
                listItems.AddRange(item.Cells.Get<ITechnical>());
            }

            return listItems;
        }
    }
}
