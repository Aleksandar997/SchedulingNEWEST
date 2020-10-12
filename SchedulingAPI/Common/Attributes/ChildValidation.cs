using System;
using System.Collections.Generic;

namespace Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ChildValidation : Attribute
    {
        //public string[] RequiredProperties { get; set; }
        //public IDictionary<string, string> Condition { get; set; }
        public List<ChildValidationModel> model { get; set; }
        public ChildValidation(string[] requiredProperties)
        {
            //RequiredProperties = requiredProperties;
        }

        public ChildValidation(string requiredProp, string requiredValue, string[] requiredProperties, ValidationType validationType)
        {
            model = new List<ChildValidationModel>() { new ChildValidationModel(requiredProp, requiredValue, requiredProperties, validationType) };
            //Condition = new Dictionary<string, string>() { { requiredProp, requiredValue } };
            //RequiredProperties = requiredProperties;
        }
        public ChildValidation(string requiredProp, string requiredValue, string[] requiredProperties, ValidationType validationType,
                                string requiredProp2, string requiredValue2, string[] requiredProperties2, ValidationType validationType2)
        {
            model = new List<ChildValidationModel>() { 
                new ChildValidationModel(requiredProp, requiredValue, requiredProperties, validationType),
                new ChildValidationModel(requiredProp2, requiredValue2, requiredProperties2, validationType)
            };
            //Condition = new Dictionary<string, string>() { { requiredProp, requiredValue } };
            //RequiredProperties = requiredProperties;
        }

        public ChildValidation() { }
    }

    public class ChildValidationModel
    {
        public string RequiredProp { get; set; }
        public string RequiredValue { get; set; }
        public string[] RequiredProperties { get; set; }

        public ValidationType ValidationType { get; set; }

        public ChildValidationModel(string requiredProp, string requiredValue, string[] requiredProperties, ValidationType validationType)
        {
            RequiredProp = requiredProp;
            RequiredValue = requiredValue;
            RequiredProperties = requiredProperties;
            ValidationType = validationType;
        }

        
    }

    public enum ValidationType
    {
        IsNull,
        IsZeroOrNull
    }
}
