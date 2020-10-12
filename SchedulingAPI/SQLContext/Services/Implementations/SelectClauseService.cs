using SQLContext.Attributes;
using SQLContext.Helpers;
using SQLContext.Models;
using SQLContext.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SQLContext.Services.Implementations
{
    public class SelectClauseService : ISelectClauseService 
    {
        public SelectModel Select<T, TResult>(Expression<Func<T, TResult>> param) where T : class
        {
            var selectModel = new SelectModel();
            var body = param.Body as NewExpression;
            var group = body.Arguments.GroupBy(x => (x as MemberExpression).Expression.Type);
            foreach (var key in group)
            {
                var splitOnType = new SplitOnType(key.Key);
                var primaryKey = KeyHelper.GetPrimaryKey(key.Key);
                if (!key.Any(x => (x as MemberExpression).Member.Name == primaryKey))
                {
                    selectModel.AddCol(key.Key.Name, primaryKey);
                }
                foreach (MemberExpression item in key)
                {
                    if (item.Expression.NodeType == ExpressionType.MemberAccess)
                        MapNested(item, selectModel, splitOnType);
                    selectModel.AddCol(key.Key.Name, item.Member.Name);
                }
                selectModel.SplitOn.Types.Add(splitOnType);
            }
            return selectModel;
        }
        private void MapNested(MemberExpression item, SelectModel selectModel, SplitOnType splitOnType)
        {
            if (item.Expression.NodeType == ExpressionType.Parameter)
                return;
            var exp = item.Expression as MemberExpression;
            var child = exp.Type;
            var parent = exp.Expression.Type;
            if (!splitOnType.NestedChildren.Contains(child))
                splitOnType.NestedChildren.Insert(0, child);
            if (!selectModel.Joins.Any(x => x.ChildTable == child.Name))
            {
                var join = exp.Member.GetCustomAttributes(typeof(JoinAttribute), false).FirstOrDefault() as JoinAttribute;
                selectModel.Joins.Add(JoinModelFactory.Initialize(
                                                                join._joinType,
                                                                parent.Name,
                                                                child.Name,
                                                                join._key,
                                                                join._foreignKey
                                                               )
                );
            }
            MapNested(exp, selectModel, splitOnType);
        }
    }
}
