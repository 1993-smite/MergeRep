using System.Reflection;
using System.Collections.Generic;

namespace MergeLib
{
    /// <summary>
    /// объединение моделей по шаблону
    /// </summary>
    /// <typeparam name="TYpeOfMerge"></typeparam>
    public class MergeModelByAuto<TYpeOfMerge> : BaseListMergeModel<TYpeOfMerge>
    {
        public MergeModelByAuto()
        {
            MergeFieldModels = new List<MergeFieldModel>();
            MergeFieldNotCombineModels = new List<MergeFieldNotCombineModel>();
        }
        public MergeModelByAuto(List<MergeFieldModel> mergeFieldModels
            , List<MergeFieldNotCombineModel> mergeFieldNotCombineModels)
        {
            MergeFieldModels = mergeFieldModels;
            MergeFieldNotCombineModels = mergeFieldNotCombineModels;
        }

        public MergeModelByAuto(List<MergeFieldNotCombineModel> mergeFieldNotCombineModels)
        {
            MergeFieldModels = new List<MergeFieldModel>();
            MergeFieldNotCombineModels = mergeFieldNotCombineModels;
        }

        public MergeModelByAuto(List<MergeFieldModel> mergeFieldModels)
        {
            MergeFieldModels = mergeFieldModels;
            MergeFieldNotCombineModels = new List<MergeFieldNotCombineModel>();
        }

        private void DiffField(FieldInfo field)
        {
            var attributes = field.GetCustomAttributes(typeof(MergedFieldAttribute), true);
            if (attributes.Length > 0)
            {
                MergeFieldModels.Add(new MergeFieldModel()
                {
                    FieldAction = MergeFieldAction.Combine,
                    FieldLink = field
                });
            }
            attributes = field.GetCustomAttributes(typeof(NewMergeFieldAttribute), true);
            if (attributes.Length > 0)
            {
                MergeFieldNotCombineModels.Add(new MergeFieldNotCombineModel()
                {
                    FieldAction = MergeFieldNotCombineAction.TakeLoad,
                    FieldLink = field
                });
            }
        }

        private void Diff()
        {
            var type = typeof(TYpeOfMerge);
            var fff = type.GetRuntimeFields();
            object[] attributes;
            MemberInfo runTimeField;
            foreach (var field in type.GetRuntimeFields())
            {
                if (field.Name.Contains("k__BackingField"))
                {
                    var propName = field.Name.Replace("<", "").Replace(">k__BackingField", "");
                    runTimeField = type.GetRuntimeProperty(propName);
                }
                else runTimeField = field;
                attributes = runTimeField.GetCustomAttributes(typeof(MergedFieldAttribute), true);
                if (attributes.Length > 0)
                {
                    MergeFieldModels.Add(new MergeFieldModel()
                    {
                        FieldAction = MergeFieldAction.Combine,
                        FieldLink = field
                    });
                }
                attributes = runTimeField.GetCustomAttributes(typeof(NewMergeFieldAttribute), true);
                if (attributes.Length > 0)
                {
                    MergeFieldNotCombineModels.Add(new MergeFieldNotCombineModel()
                    {
                        FieldAction = MergeFieldNotCombineAction.TakeLoad,
                        FieldLink = field
                    });
                }
            }
        }

        public TYpeOfMerge Combine(TYpeOfMerge baseModel, TYpeOfMerge newModel)
        {
            BaseModel = baseModel;
            NewModel = newModel;
            this.Diff();
            return this.Combine();
        }
    }
}
