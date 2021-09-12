using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace chefsndishes.Class
{
    public class FutureDateAttribute : ValidationAttribute
    {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // You first may want to unbox "value" here and cast to to a DateTime variable!
        DateTime CurrentTime = DateTime.Now;
        DateTime SelectedDate = (DateTime)value;
        int result = DateTime.Compare(CurrentTime, SelectedDate);
        if (result < 0)
            return new ValidationResult("Selected date must be before than current date!");
        return ValidationResult.Success;
    }
    }
} 