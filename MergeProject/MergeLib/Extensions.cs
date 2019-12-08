using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MergeLib
{
    public static class Extensions
    {
        public static object GetPropertyValue(this FieldInfo field, object obj)
        {
            return field.GetValue(obj);
        }

        public static void MergePropertyValue<T>(this MergeFieldModel field, T lastObj, T newObj)
        {
            if (field == null || field.FieldLink == null)
                return;
            var fieldf = field.FieldLink;
            var fff = field.FieldLink.FieldType.GetRuntimeMethods();
            //var ff = field.FieldLink.FieldType.GetRuntimeMethod("Merge");
            switch (field.FieldAction)
            {
                case MergeFieldAction.Combine:
                    if (field.FieldLink.FieldType.GetMethod("Merge") != null || true)
                    {
                        try
                        {
                            object obj = new object();
                            var obj1 = field.FieldLink.FieldType.InvokeMember("Merge",
                                BindingFlags.DeclaredOnly |
                                BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.Instance | BindingFlags.SetProperty, null, obj, 
                                new Object[] { field.FieldLink.GetPropertyValue(lastObj), field.FieldLink.GetPropertyValue(newObj) });
                            //field.FieldLink.SetValue(lastObj,);
                        }
                        catch(Exception err)
                        {
                            throw err;
                        }
                    }
                    else
                    {
                        throw new Exception($"Необходимо реализовать метод Merge для типа {field.FieldLink.DeclaringType.Name}");
                    }
                    break;
                case MergeFieldAction.TakeLoad:
                    field.FieldLink.SetValue(lastObj, field.FieldLink.GetPropertyValue(newObj));
                    break;
            }
        }

        public static void MergePropertyValue<T>(this MergeFieldNotCombineModel field, T lastObj, T newObj)
        {
            if (field == null || field.FieldLink == null)
                return;
            switch (field.FieldAction)
            {
                case MergeFieldNotCombineAction.TakeLoad:
                    field.FieldLink.SetValue(lastObj, field.FieldLink.GetPropertyValue(newObj));
                    break;
            }
        }

        public static object Merge(this string field, object lastVal, object newVal)
        {
            return $"{lastVal},{newVal}";
        }
    }

    public class MemberHelper<T>
    {
        public string GetName<U>(Expression<Func<T, U>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;

            throw new InvalidOperationException("Member expression expected");
        }
    }
}
