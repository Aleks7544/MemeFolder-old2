namespace MemeFolder.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Models;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TextOrMediaFileRequired : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string text = (string)validationContext.ObjectType.GetProperty("Text").GetValue(validationContext.ObjectInstance, null);

            ICollection<MediaFile> mediaFiles = (ICollection<MediaFile>)validationContext.ObjectType.GetProperty("MediaFiles").GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrEmpty(text) && !mediaFiles.Any())
                return new ValidationResult("At least one media file OR some text is required!");

            return ValidationResult.Success;
        }
    }
}
