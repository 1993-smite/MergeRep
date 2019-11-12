using MergeLib;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MergeLibTest
{
    class Model
    {
        public int x;
        public int y;
        public int k;
        public int z { get; set; }

        public static List<MergeFieldNotCombineModel> MergeTemplate()
        {
            Type type = typeof(Model);
            List<MergeFieldNotCombineModel> mergeFieldNotCombineModels = new List<MergeFieldNotCombineModel>();
            mergeFieldNotCombineModels.Add(
                new MergeFieldNotCombineModel()
                {
                    FieldLink = type.GetRuntimeField("x")
                    , FieldAction = MergeFieldNotCombineAction.TakeBase
                });
            mergeFieldNotCombineModels.Add(
                new MergeFieldNotCombineModel()
                {
                    FieldLink = type.GetRuntimeField("y")
                    ,
                    FieldAction = MergeFieldNotCombineAction.TakeLoad
                });
            mergeFieldNotCombineModels.Add(
                new MergeFieldNotCombineModel()
                {
                    FieldLink = type.GetRuntimeField("k")
                    ,
                    FieldAction = MergeFieldNotCombineAction.TakeLoad
                });
            mergeFieldNotCombineModels.Add(
                new MergeFieldNotCombineModel()
                {
                    FieldLink = type.GetRuntimeField("z")
                    ,
                    FieldAction = MergeFieldNotCombineAction.TakeLoad
                });
            return mergeFieldNotCombineModels;
        }

        public bool Equal(Model model)
        {
            return
            this.y == model.y &&
            this.k == model.k;
        }
    }

    public class TestMergeModelByAuto
    {
        [Test]
        public void TestMergeModelByAutoCombine()
        {
            Model m = new Model()  { x = 3, y = 4, z = 5 };
            Model m1 = new Model() { x = 6, y = 7, z = 8 };
            Model m2 = new Model() { x = 9, y = 0, z = 1 };

            MergeModelByAuto<Model> mergeModelByAuto = 
                new MergeModelByAuto<Model>(Model.MergeTemplate());
            m = mergeModelByAuto.Combine(m, m1);
            Assert.AreEqual(m.Equal(m1),true);
            m1 = mergeModelByAuto.Combine(m1, m2);
            Assert.AreEqual(m1.Equal(m2),true);
        }
    }
}
