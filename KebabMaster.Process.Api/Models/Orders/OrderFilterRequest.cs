﻿namespace KebabMaster.Process.Api.Models.Orders;

public class OrderFilterRequest
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string SteetName { get; set; }
}