using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace i4o
{
    public static class IndexSetExtensions
    {
        public static T FirstOrDefaultByIndexedProperty<T, TProperty>(this IndexSet<T> indexSet, Expression<Func<T, TProperty>> property, object value)
        {
            return WhereByIndexedProperty(indexSet, property, value).FirstOrDefault();
        }

        public static IEnumerable<T> WhereUsingIndex<T>(IndexSet<T> indexSet, Expression<Func<T, bool>> predicate)
        {
            if (!(predicate.Body is BinaryExpression)) throw new NotSupportedException();
            var binaryBody = (BinaryExpression) predicate.Body;
            switch (binaryBody.NodeType)
            {
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    {
                        var leftResults = WhereUsingIndex(indexSet, Expression.Lambda<Func<T,bool>>(binaryBody.Left, predicate.Parameters));
                        var rightResults = WhereUsingIndex(indexSet, Expression.Lambda<Func<T, bool>>(binaryBody.Right, predicate.Parameters));
                        return leftResults.Union(rightResults);
                    }
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    {
                        var leftResults = WhereUsingIndex(indexSet, Expression.Lambda<Func<T, bool>>(binaryBody.Left, predicate.Parameters ));
                        var rightResults = WhereUsingIndex(indexSet, Expression.Lambda<Func<T, bool>>(binaryBody.Right, predicate.Parameters));
                        return leftResults.Intersect(rightResults);                
                    }
                case ExpressionType.Equal:
                    return indexSet.WhereUsingIndex(predicate);
                default:
                    return Enumerable.Where(indexSet, predicate.Compile());
            }
        }
        
        public static IEnumerable<T> WhereIndexed<T>(this IndexSet<T> indexSet, Expression<Func<T,bool>> predicate)
        {
            return WhereUsingIndex(indexSet, predicate);
        }

        public static IEnumerable<T> WhereByIndexedProperty<T, TProperty>(this IndexSet<T> indexSet,
           Expression<Func<T, TProperty>> property,  object value)
        {
            return indexSet.WhereUsingIndex(property.GetMemberName(), value);
        }

        public static IEnumerable<T> WhereByIndexedProperty<T>(this IndexSet<T> indexSet,
        string propertyName, object value)
        {
            return indexSet.WhereUsingIndex(propertyName, value);
        }
    }
}