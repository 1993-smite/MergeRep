using MergeLib;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MergeLibTest
{
    class Str
    {
        public string val;
        public Str(string v)
        {
            val = v;
        }
        object Merge(object val1,object val2)
        {
            return $"{val1},{val2}";
        } 
    }

    class Model
    {
        [NewMergeField]
        public int x;
        [NewMergeField]
        public int z { get; set; }
        [NewMergeField]
        public int[] z1 { get; set; }
        [NewMergeField]
        public List<int> z2 { get; set; }
        [MergedField]
        public Str m { get; set; }

        public bool Equal(Model model)
        {
            return x == model.x
                && z == model.z
                && m == model.m;
        }
    }

    public class TestMergeModelByAuto
    {
        [Test]
        public void TestMergeModelByAutoCombine()
        {
            Model m = new Model()  { x = 3, z = 5, m = new Str("1") };
            Model m1 = new Model() { x = 6, z = 8, m = new Str("2") };
            Model m2 = new Model() { x = 9, z = 1, m = new Str("3") };

            MergeModelByAuto<Model> mergeModelByAuto =
                //new MergeModelByAuto<Model>(Model.MergeTemplate());
                new MergeModelByAuto<Model>();
            m = mergeModelByAuto.Combine(m, m1);
            Assert.AreEqual(m.Equal(m1),true);
            m1 = mergeModelByAuto.Combine(m1, m2);
            Assert.AreEqual(m1.Equal(m2),true);
        }
    }
}
