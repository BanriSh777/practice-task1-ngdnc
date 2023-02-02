using System;
using System.Collections.Generic;

namespace Catalog.Repository.Entities;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
