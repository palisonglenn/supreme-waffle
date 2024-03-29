﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorSearchListSystem.Entities;

[Table("OrderList")]
internal partial class OrderList
{
    [Key]
    public int OrderListID { get; set; }

    public int OrderID { get; set; }

    public int ProductID { get; set; }

    public double QtyOrdered { get; set; }

    public double QtyPicked { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "money")]
    public decimal Discount { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CustomerComment { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string PickIssue { get; set; }

    [ForeignKey("OrderID")]
    [InverseProperty("OrderLists")]
    public virtual Order Order { get; set; }

    [ForeignKey("ProductID")]
    [InverseProperty("OrderLists")]
    public virtual Product Product { get; set; }
}