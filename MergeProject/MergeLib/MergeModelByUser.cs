namespace MergeLib
{
    /// <summary>
    /// модель для объединения моделей, решает пользователь как объединять
    /// </summary>
    /// <typeparam name="TYpeOfMerge"></typeparam>
    public class MergeModelByUser<TYpeOfMerge> : BaseListMergeModel<TYpeOfMerge>
    {
        public MergeModelByUser():base()
        {

        }
    }

    public interface IDiffMergedUser<T>
    {
        MergeModelByUser<T> Diff(T mdl);
    }
}
