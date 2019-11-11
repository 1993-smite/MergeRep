using MergeModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MergeProject
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
            switch (field.FieldAction)
            {
                case MergeFieldAction.TakeBase:
                    break;
                case MergeFieldAction.Combine:
                    break;
                case MergeFieldAction.TakeLoad:
                    var baseModelField = lastObj.GetType().GetRuntimeField(field.FieldLink.Name);
                    baseModelField.SetValue(lastObj, field.FieldLink.GetPropertyValue(newObj));
                    break;
            }
        }

        public static void MergePropertyValue<T>(this MergeFieldNotCombineModel field, T lastObj, T newObj)
        {
            if (field == null || field.FieldLink == null)
                return;
            switch (field.FieldAction)
            {
                case MergeFieldNotCombineAction.TakeBase:
                    break;
                case MergeFieldNotCombineAction.TakeLoad:
                    var baseModelField = lastObj.GetType().GetRuntimeField(field.FieldLink.Name);
                    baseModelField.SetValue(lastObj, field.FieldLink.GetPropertyValue(newObj));
                    break;
            }
        }
    }

    public class MergeModelByUser<TYpeOfMerge> : BaseListMergeModel<TYpeOfMerge>
    {
        public MergeModelByUser():base()
        {

        }

        public override TYpeOfMerge Combine()
        {
            foreach(var field in MergeFieldModels)
            {
                field.MergePropertyValue(this.BaseModel, this.NewModel);
            }
            foreach (var field in MergeFieldNotCombineModels)
            {
                field.MergePropertyValue(this.BaseModel, this.NewModel);
            }
            return BaseModel;
        }
    }
}
