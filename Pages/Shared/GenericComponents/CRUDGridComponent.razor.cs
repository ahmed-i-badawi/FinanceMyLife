using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace FinanceMyLife.Pages.Shared.GenericComponents
{
    public partial class CRUDGridComponent<DTO> where DTO : class
    {
        [Parameter]
        public bool PKVisible { get; set; } = false;
        [Parameter]
        public Query GridQuery { get; set; } = null;
        [Parameter]
        public SfGrid<DTO> GridRef { get; set; }
        [Parameter]
        public RenderFragment GridColumnsTemplate { get; set; }
        [Parameter]
        public string PKName { get; set; }
    }

}
