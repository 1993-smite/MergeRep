using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MergeLib;

namespace MergeProject
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
            diff.MergeFieldNotCombineModels= new List<MergeFieldNotCombineModel>();
            foreach(var field in this.GetType().GetRuntimeFields())
            {
                diff.MergeFieldNotCombineModels.Add(new MergeFieldNotCombineModel() { FieldLink=field });
            }
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
            foreach (var field in m.GetType().GetRuntimeFields())
            {
                Console.WriteLine($"{field.Name} {field.GetValue(m)} {field.GetValue(m1)}");
            }
            var ttt = "".GetType().GetMembers().ToList().OrderBy(x=>x.Name);
            var tt = typeof(string).GetDefaultMembers().ToList().OrderBy(x => x.Name);
            var rer = typeof(string).GetDefaultMembers().ToList().OrderBy(x => x.Name);
            var rrr = typeof(string).GetDefaultMembers().ToList().FirstOrDefault(x=>x.Name.Contains("Merge"));
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
