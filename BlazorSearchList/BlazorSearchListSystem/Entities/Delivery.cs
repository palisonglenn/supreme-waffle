﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorSearchListSystem.Entities;

internal partial class Delivery
{
    [Key]
    public int DeliveryID { get; set; }

    public int OrderID { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(12)]
    public string Phone { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ShippedDate { get; set; }
}