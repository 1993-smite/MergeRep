using MergeModel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MergeProject
{
    class Model
    {
        public int x;
        public int y;
        public int z { get; set; }

        public BaseListMergeModel<Model> Diff(Model mdl)
        {
            BaseListMergeModel<Model> diff = new MergeModelByUser<Model>();
            diff.BaseModel = this;
            diff.NewModel = mdl;
            diff.MergeFieldNotCombineModels= new List<MergeFieldNotCombineModel>();
            /*foreach(var field in this.GetType().GetRuntimeFields())
            {
                diff.MergeFieldNotCombineModels.Add(new MergeFieldNotCombineModel() { FieldLink=field });
            }*/
            if (x != mdl.x)
                diff.MergeFieldNotCombineModels.Add(
                    new MergeFieldNotCombineModel() { FieldLink = this.GetType().GetRuntimeField("x") });
            if (y != mdl.y) 
                diff.MergeFieldNotCombineModels.Add(
                    new MergeFieldNotCombineModel() { FieldLink = this.GetType().GetRuntimeField("y") });
            if (z != mdl.z)
                diff.MergeFieldNotCombineModels.Add(
                    new MergeFieldNotCombineModel() { FieldLink = this.GetType().GetRuntimeField("z") });
            return diff;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Model m = new Model { x = 3, y = 6, z=4 };
            Model m1 = new Model { x = 4, y = 7, z=8 };
            BaseListMergeModel<Model> res = m.Diff(m1);
            foreach(var field in res.MergeFieldNotCombineModels)
            {
                field.FieldAction = MergeFieldNotCombineAction.TakeLoad;
            }
            var m2 = res.Combine();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
