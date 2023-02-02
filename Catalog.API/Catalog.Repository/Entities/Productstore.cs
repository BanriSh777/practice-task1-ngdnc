using System;
using System.Collections.Generic;

namespace Catalog.Repository.Entities;

public partial class Productstore
{
    public string ProductStoreId { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? ProductStoreName { get; set; }

    public virtual ICollection<Stock> Stocks { get; } = new List<Stock>();
}
