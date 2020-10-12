namespace System.ComponentModel.DataAnnotations.Schema
{
    public class PrimaryKeyAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ForeignKey : ForeignKeyAttribute
    {
        public ForeignKey(string name, string parentColumn, string childColumn) : base(name)
        {
            ParentColumn = parentColumn;
            ChildColumn = childColumn;
        }
        public string ParentColumn { get; set; }
        public string ChildColumn { get; set; }
    }
}


