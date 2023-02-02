using System;
using System.Collections.Generic;

namespace Catalog.Repository.Entities;

public partial class Stock
{
    public string ProductId { get; set; } = null!;

    public string ProductStoreId { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Unit { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Productstore ProductStore { get; set; } = null!;
}
