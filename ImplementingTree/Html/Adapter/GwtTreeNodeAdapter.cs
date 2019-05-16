using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;
using Tricentis.Automation.Simulation;

namespace ImplementingTree.Html.Adapter
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class GwtTreeNodeAdapter : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITreeNodeAdapter
    {
        #region Fields
        private bool hasExpander;
        private IHtmlAdapter expander;
        private IHtmlAdapter nameNode;
        private IHtmlCellTechnical expanderCell;
        private IHtmlDivTechnical nameDiv;
        private IHtmlElementTechnical subNodeContainer;
        #endregion

        #region Constructors and Destructors
        public GwtTreeNodeAdapter(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            if (!ProcessTreeNode(technical))
            {
                validator.Fail();
                return;
            }
        }

        #endregion

        #region Public Properties
        public bool Selected => false; // nameDiv.ClassName != null && nameDiv.ClassName.Contains("gwt-TreeItem-selected");

        public string Name => nameDiv.InnerText;

        public override string DefaultName => Name == null ? base.DefaultName : Name;
        public System.Drawing.PointF? ExpandCollapsePoint => Expander.ActionPoint;
        public bool Expanded
        {
            get
            {
                if (subNodeContainer != null)
                {
                    string display = subNodeContainer.CurrentStyle.Get<IHtmlStyleTechnical>().Display;
                    return display != "none";
                }
                return false;
            }
        }
        #endregion

        #region Properties
        private IHtmlAdapter Expander
        {
            get
            {
                if (expander == null && hasExpander)
                {
                    expander = AdapterFactory.CreateAdapters<IHtmlAdapter>(expanderCell, "Html").Single();
                    expander.Context = this;
                }

                return expander;
            }
        }

        private IHtmlAdapter NameNode
        {
            get
            {
                if (nameNode == null)
                {
                    nameNode = AdapterFactory.CreateAdapters<IHtmlAdapter>(nameDiv, "Html").Single();
                    nameNode.Context = this;
                }

                return nameNode;
            }
        }


        #endregion

        #region Public Methods and Operators
        public void Collapse()
        {
            Mouse.PerformMouseAction(MouseOperation.Click, Expander.ActionPoint);
        }

        public void Expand()
        {
            Mouse.PerformMouseAction(MouseOperation.Click, Expander.ActionPoint);
        }

        public void Select()
        {
            Mouse.PerformMouseAction(MouseOperation.Click, NameNode.ActionPoint);

        }
        #endregion

        #region Methods
        private bool ProcessTable(IHtmlElementTechnical elementTechnical)
        {
            try
            {
                IHtmlElementTechnical tBody = elementTechnical.Children.Get<IHtmlElementTechnical>().Single();
                IHtmlRowTechnical tableRow = tBody.Children.Get<IHtmlRowTechnical>().First();
                IEnumerable<IHtmlCellTechnical> cellTechnicals = tableRow.Children.Get<IHtmlCellTechnical>().ToList();

                expanderCell = cellTechnicals.First();
                IHtmlCellTechnical nodeNameCell = cellTechnicals.Last();
                nameDiv = nodeNameCell.Children.Get<IHtmlDivTechnical>().Single();
                return true;
            }
            catch (InvalidOperationException)
            {
                expanderCell = null;
                nameDiv = null;
                return false;
            }
        }

        private bool ProcessTreeNode(IHtmlDivTechnical technical)
        {
            IEnumerable<IHtmlElementTechnical> children = technical.Children.Get<IHtmlElementTechnical>().ToList();

            if (!children.Any())
            {
                return false;
            }

            IHtmlElementTechnical elementTechnical = children.First();
            if (elementTechnical is IHtmlTableTechnical)
            {
                hasExpander = true;
                if (!ProcessTable(elementTechnical))
                {
                    return false;
                }

                subNodeContainer = children.Last();
            }
            else if (elementTechnical is IHtmlDivTechnical && elementTechnical.ClassName != null && elementTechnical.ClassName.Contains("gwt-TreeItem"))
            {
                nameDiv = (IHtmlDivTechnical)elementTechnical;
            }
            else
            {
                return false;
            }

            return true;
        }
        #endregion









    }
}
