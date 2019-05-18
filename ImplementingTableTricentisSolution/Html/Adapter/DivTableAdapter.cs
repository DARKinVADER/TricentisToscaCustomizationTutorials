using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

namespace HtmlTable.Html.Adapter
{
    // This class represents (or interacts with) the table itself. The class implements the ITableAdapter, allowing the framework to identify
    // this Adapter as the table. The interface creates the link between the Representation and the Adapter

    // Supported Technical describes the type of HTML element that this table is represented by in the actual HTML structure. In this case 
    // the table is represented by a div-tag. Since the DIV element is already known by the framework we can refer to it.
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class DivTableAdapter : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITableAdapter
    {
        // The class inherits common functionality from the AbstractHtmlDomNodeAdapter, so that we do not have to implement all the common 
        // methods and properties that work the same for all HtmlDomNodes. The AbstactHtmlDomNodeAdapter needs to be parametrized with
        // the technical it interacts with - IHtmlDivTechnical in this case (the same as the SupportedTechnical!).

        // A constructor is necessary since there is no default constructor. The technical that this adapter is responsible for will be
        // passed in by the framework. Additionally there is a validator. This validator should be used to determine if the adapter fits
        // this technical. In this case we need to validate that our div tag represents the table. The validator is used to 
        // tell if the technical is a proper table or not, e.g. by checking the classname for the correct value.
        public DivTableAdapter(IHtmlDivTechnical technical,
                               Validator validator) : base(technical, validator)
        {
            validator.AssertTrue(() => technical.ClassName == "div-table");
        }
        public LoadStrategy LoadStrategy { get; } = LoadStrategy.Default;
    }
}