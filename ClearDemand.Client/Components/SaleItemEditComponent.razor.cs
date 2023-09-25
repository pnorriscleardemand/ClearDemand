using ClearDemand.Client.ViewModel;
using Microsoft.AspNetCore.Components;

namespace ClearDemand.Client.Components;

public partial class SaleItemEditComponent : ComponentBase
{
    [Parameter] public SaleItemViewModel? ProductItemViewModel { get; set; }

    [Parameter] public List<ProductViewModel>? Products { get; set; }
}