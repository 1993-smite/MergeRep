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

    /// <summary>
    /// интерфейс для определения полей, которые участвуют в объединении
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDiffMergedUser<T>
    {
        MergeModelByUser<T> Diff(T mdl);
    }
}
