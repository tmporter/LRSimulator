using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.Models
{
    //[Flags]
    public enum GradeEnum
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "A+")]
        A_plus,
        [Display(Name = "A")]
        A,
        [Display(Name = "A-")]
        A_min,
        [Display(Name = "B+")]
        B_plus,
        [Display(Name = "B")]
        B,
        [Display(Name = "B-")]
        B_minus,
        [Display(Name = "C+")]
        C_plus,
        [Display(Name = "C")]
        C,
        [Display(Name = "C-")]
        C_min,
        [Display(Name = "D+")]
        D_plus,
        [Display(Name = "D")]
        D,
        [Display(Name = "D-")]
        D_minus,
        [Display(Name = "F+")]
        F_plus,
        [Display(Name = "F")]
        F,
        [Display(Name = "F-")]
        F_minus,
        [Display(Name = "Sideboard")]
        Sideboard,
        [Display(Name = "Build Around")]
        BuildAround
    }
}
