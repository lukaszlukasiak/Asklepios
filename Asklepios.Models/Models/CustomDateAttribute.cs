using System;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Models
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
          : base(typeof(DateTimeOffset),
                  DateTimeOffset.Now.AddYears(-120).Date.ToShortDateString(),
                  DateTimeOffset.Now.Date.ToShortDateString())
        { }
    }
}