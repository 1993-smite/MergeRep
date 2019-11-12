using System.Collections.Generic;

namespace MergeLib
{
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
            NewModel = NewModel;
            return this.Combine();
        }
    }
}
