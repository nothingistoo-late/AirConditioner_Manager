using System;
using System.Collections.Generic;

namespace AirConditionerShop.DAL.Entities;

public partial class AirConditioner
{
    public int AirConditionerId { get; set; }

    public string AirConditionerName { get; set; } = null!;

    public string? Warranty { get; set; }

    public string? SoundPressureLevel { get; set; }

    public string? FeatureFunction { get; set; }

    public int? Quantity { get; set; }

    public double? DollarPrice { get; set; }

    public string? SupplierId { get; set; }

    public virtual SupplierCompany? Supplier { get; set; }
    // vietsub: 1 máy lạnh cụ thể thì có 1 nhà cung cấp cụ thể nào đó, tạm gọi là SUplier = new SupplierCompany()
    //SUPPLIER dc gọi là NAVIGATION PATH
    //con đường đi đến object cha, object ở table cha, table 1 - N
}
