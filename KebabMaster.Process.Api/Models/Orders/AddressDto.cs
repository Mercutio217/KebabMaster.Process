﻿namespace KebabMaster.Process.Api.Models.Orders;

public class AddressDto
{
    public string StreetName { get; set; }
    public int? StreetNumber { get; set; }
    public int? FlatNumber { get; set;  }
}