using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Class
{
    public class FutureDateAttribute : ValidationAttribute
    {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // You first may want to unbox "value" here and cast to to a DateTime variable!
        DateTime CurrentTime = DateTime.Now;
        DateTime SelectedDate = (DateTime)value;
        int result = DateTime.Compare(CurrentTime, SelectedDate);
        if (result > 0)
            return new ValidationResult("Selected date must be in the future!");
        return ValidationResult.Success;
    }
    }
}