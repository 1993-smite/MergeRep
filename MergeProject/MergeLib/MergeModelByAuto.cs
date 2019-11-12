using System.Collections.Generic;

namespace MergeLib
{
    /// <summary>
    /// объединение моделей по шаблону
    /// </summary>
    /// <typeparam name="TYpeOfMerge"></typeparam>
    public class MergeModelByAuto<TYpeOfMerge> : BaseListMergeModel<TYpeOfMerge>
    {
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

        public TYpeOfMerge Combine(TYpeOfMerge baseModel, TYpeOfMerge newModel)
        {
            BaseModel = baseModel;
            NewModel = newModel;
            return this.Combine();
        }
    }
}
