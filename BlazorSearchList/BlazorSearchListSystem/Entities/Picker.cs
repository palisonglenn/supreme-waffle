﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorSearchListSystem.Entities;

internal partial class Picker
{
    [Key]
    public int PickerID { get; set; }

    [Required]
    [StringLength(35)]
    public string LastName { get; set; }

    [Required]
    [StringLength(25)]
    public string FirstName { get; set; }

    public bool Active { get; set; }

    public int StoreID { get; set; }

    [ForeignKey("StoreID")]
    [InverseProperty("Pickers")]
    public virtual Store Store { get; set; }
}