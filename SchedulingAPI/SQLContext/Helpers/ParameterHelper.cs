﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;

namespace SQLContext.Helpers
{
    public static class ParameterHelper
    {
        public static SqlParameter ToUserDefinedTableType<T>(IEnumerable<T> enumerable, string typeName)
        {
            var dt = new DataTable();
            if (!enumerable.Any())
            {
                return new SqlParameter()
                {
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    TypeName = typeName
                };
            }
            foreach (var prop in enumerable.First().GetType().GetProperties())
            {
                var column = prop.PropertyType;
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(column) ?? column);
            }
            foreach (var row in enumerable)
            {
                var record = dt.NewRow();
                foreach (var prop in row.GetType().GetProperties())
                {
                    record[prop.Name] = prop.GetValue(row, null) ?? DBNull.Value;
                }
                dt.Rows.Add(record);
            }
            var param = new SqlParameter()
            {
                SqlDbType = SqlDbType.Structured,
                Direction = ParameterDirection.Input,
                TypeName = typeName,
                Value = dt
            };
            return param;
        }
    }
    public class DynamicParameter : SqlMapper.IDynamicParameters
    {
        private List<SqlParameter> _params { get; set; }
        public DynamicParameter(List<SqlParameter> param)
        {
            _params = param;
        }
        public DynamicParameter(dynamic param)
        {
            if (param == null)
                return;
            _params = new List<SqlParameter>();
            foreach (var p in param.GetType().GetProperties())
            {
                switch (p.PropertyType.Name)
                {
                    case "SqlParameter":
                        if (((SqlParameter)p.GetValue(param)).ParameterName == "null")
                            continue;
                        ((SqlParameter)p.GetValue(param)).ParameterName = p.Name;
                        _params.Add((SqlParameter)p.GetValue(param));
                        break;
                    case "DateTime":
                        var dateVal = p.GetValue(param);
                        if (dateVal == DateTime.MinValue)
                        {
                            _params.Add(new SqlParameter(p.Name, DBNull.Value));
                            continue;
                        }
                        _params.Add(new SqlParameter(p.Name, new DateTime?(dateVal)));
                        break;
                    default:
                        _params.Add(new SqlParameter(p.Name, p.GetValue(param) ?? DBNull.Value));
                        break;
                }
            }
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            if (_params != null)
                ((SqlCommand)command).Parameters.AddRange(_params.ToArray());
        }
    }
}
