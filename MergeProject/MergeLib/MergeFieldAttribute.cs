using System;
using System.Collections.Generic;
using System.Text;

namespace MergeLib
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MergeFieldAttribute : Attribute
    {
    }

    public class MergedFieldAttribute : MergeFieldAttribute
    {
    }

    public class NewMergeFieldAttribute : MergeFieldAttribute
    {
    }
}
