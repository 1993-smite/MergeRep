using MergeLib;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace Tests
{
    class Model : IDiffMergedUser<Model>
    {
        public int x;
        public int y;
        public int z { get; set; }

        public MergeModelByUser<Model> Diff(Model mdl)
        {
            MergeModelByUser<Model> diff = new MergeModelByUser<Model>();
            diff.BaseModel = this;
            diff.NewModel = mdl;
            diff.MergeFieldNotCombineModels = new List<MergeFieldNotCombineModel>();
            foreach (var field in this.GetType().GetRuntimeFields())
            {
                diff.MergeFieldNotCombineModels.Add(new MergeFieldNotCombineModel() { FieldLink = field });
            }
            return diff;
        }

    }

    public class TestMergeModelByUser
    {
        private Model m = new Model { x = 3, y = 6, z = 4 };
        private Model m1 = new Model { x = 4, y = 7, z = 8 };

        [Test]
        public void TestDiff()
        {
            BaseListMergeModel<Model> res = m.Diff(m1);
            Assert.AreEqual(res.MergeFieldNotCombineModels.Count,3);
        }

        [Test]
        public void TestCombine()
        {
            BaseListMergeModel<Model> res = m.Diff(m1);
            foreach (var field in res.MergeFieldNotCombineModels)
            {
                field.FieldAction = MergeFieldNotCombineAction.TakeLoad;
            }
            var m2 = res.Combine();
            Assert.AreEqual(m1.x, m2.x);
            Assert.AreEqual(m1.y, m2.y);
            Assert.AreEqual(m1.z, m2.z);
        }
    }
}