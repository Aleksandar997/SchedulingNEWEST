using SQLContext.Extensions;
using SQLContext.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLContext.Models
{
    public class SaveModel
    {
        public string Table { get; set; }
        public List<string> Columns { get; set; }
        public List<string> Values { get; set; }
        public int Id { get; set; }
        public Type Type { get; set; }
        StringBuilder sb;
        public SaveModel(string table, List<string> columns, List<string> values, int id, Type type)
        {
            sb = new StringBuilder();
            Table = table;
            Columns = columns;
            Values = values;
            Id = id;
            Type = type;
        }
 
        public string ToSql()
        {
            if (sb == null)
                throw new Exception("SAVE OBJECT IS NULL");
            if (Id > 0)
            {
                return sb.Append($"UPDATE {Table} ")
                         .Append($"SET {string.Join(", ", Columns.Select((x, i) => $"{x} = {@"'" + Values[i] + @"'"}"))} ")
                         .Append($"WHERE {KeyHelper.GetPrimaryKey(Type)} = {Id} ")
                         .Append($"SELECT {Id}")
                         .ToString();
            }
            return sb.Append($"INSERT INTO {Table}({string.Join(", ", Columns)}) ")
                     .Append($"VALUES({string.Join(", ", Values.Select(x => @"'" + x + @"'"))}) ")
                     .Append("SELECT SCOPE_IDENTITY()")
                     .ToString();
        }
    }
    public class SelectModel
    {
        private StringBuilder stringBuilder = new StringBuilder();
        internal List<Column> SelectedColumns { get; set; } = new List<Column>();
        internal TableModel Table { get; set; }
        internal List<JoinModel> Joins = new List<JoinModel>();
        internal OrderByClauseModel OrderBy { get; set; } = new OrderByClauseModel();
        internal bool IsGenerated { get; set; }
        internal SplitOnModel SplitOn { get; set; } = new SplitOnModel();
        internal SelectModel Generate()
        {
            IsGenerated = true;
            return this;
        }

        internal void AddCol(string tableName, string columnName)
        {
            SelectedColumns.Add(new Column(tableName, columnName));
        }

        internal void SetSelectClause(SelectModel selectModel)
        {
            SelectedColumns = selectModel.SelectedColumns;
            SplitOn = selectModel.SplitOn;
            Joins = selectModel.Joins;
        }
        internal void SetOrderByClause(OrderByClauseModel orderByClauseModel)
        {
            OrderBy = orderByClauseModel;
        }

        internal static SelectModel Instance() => new SelectModel();

        internal string ToSql() => stringBuilder
                     .Clear()
                     .AppendLine(string.Format("SELECT {0}", string.Join(", ", SelectedColumns.Select(x => x.Value))))
                     .AppendLine(string.Format("FROM {0}", (Table.Name.QuoteName().ToString())))
                     .AppendLine($"{string.Join(" ", Joins.Select(x => x.ToSql()))} ")
                     .AppendLine(OrderBy.Value)
                     .ToString();

    }
    public class OrderByClauseModel
    {
        
        internal string Value { get; set; }
        public OrderByClauseModel()
        {
        }
        public OrderByClauseModel(string value)
        {
            Value = value;
        }
    }

    internal class TableModel
    {
        internal string Name { get; private set; }
        internal Type Type { get; set; }

        internal TableModel(Type type)
        {
            Type = type;
            Name = type.GetTableName();
        }
    }

    internal class Column
    {
        internal string Table { get; private set; }
        internal string Value { get; private set; }
        internal Type TableType { get; set; }
        internal Column(string table, string columnValue)
        {
            Table = table;
            Value = table == null ? columnValue : string.Format("{0}.{1}", table.QuoteName(), columnValue.QuoteName());
        }
    }

    internal class SplitOnModel
    {
        internal List<SplitOnType> Types { get; set; } = new List<SplitOnType>();
        public override string ToString() => string.Join(",", Types.Skip(1).Select(x => KeyHelper.GetPrimaryKey(x.Parent)));
    }

    internal class SplitOnType
    {
        internal Type Parent { get; set; }
        internal List<Type> NestedChildren { get; set; } = new List<Type>();

        internal SplitOnType(Type parent)
        {
            Parent = parent;
        }
    }
}
