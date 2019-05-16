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

namespace ImplementingTree.Html.Adapter.Controller
{
    [SupportedAdapter(typeof(GwtTreeNodeAdapter))]
    public class GwtTreeNodeAdapterController : TreeNodeContextAdapterController<GwtTreeNodeAdapter>
    {
        public GwtTreeNodeAdapterController(GwtTreeNodeAdapter contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
        {
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(TreeNodeBusinessAssociation businessAssociation)
        {
            yield return new AlgorithmicAssociation("SubNodes");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(DescendantsBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("All");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ParentBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("ParentNode");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ChildrenBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("Children");
        }

        protected override IEnumerable<ITechnical> SearchTechnicals(IAlgorithmicAssociation algorithmicAssociation)
        {
            return algorithmicAssociation.AlgorithmName == "SubNodes" ? GetSubNodes() : base.SearchTechnicals(algorithmicAssociation);
        }

        private IEnumerable<ITechnical> GetSubNodes()
        {
            IHtmlDivTechnical child = ContextAdapter.Technical.Children.Get<IHtmlDivTechnical>().FirstOrDefault();

            return child != null ? child.Children.Get<ITechnical>() : new ITechnical[] { };
        }
    }
}
