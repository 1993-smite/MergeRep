using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web;
using System.Reflection;

namespace MergeLib
{
    /// <summary>
    /// доработанный класс с агригированными моделями merge
    /// </summary>
    /// <typeparam name="TYpeOfMerge"></typeparam>
    public abstract class BaseListMergeModel<TYpeOfMerge> : BaseMergeModel<TYpeOfMerge>
    {
        /// <summary>
        /// список полей для объединения и неоднозначным выбором действия
        /// </summary>
        public List<MergeFieldModel> MergeFieldModels { get; set; }
        /// <summary>
        /// список полей для объединения и однозначным выбором действия
        /// </summary>
        public List<MergeFieldNotCombineModel> MergeFieldNotCombineModels { get; set; }

        /// <summary>
        /// конструктор для доработанного класса объединения
        /// </summary>
        public BaseListMergeModel():base()
        {
            MergeFieldModels = new List<MergeFieldModel>();
            MergeFieldNotCombineModels = new List<MergeFieldNotCombineModel>();
        }

        /// <summary>
        /// объединение записей моделей
        /// </summary>
        /// <returns></returns>
        public override TYpeOfMerge Combine()
        {
            foreach (var field in MergeFieldModels)
            {
                field.MergePropertyValue(this.BaseModel, this.NewModel);
            }
            foreach (var field in MergeFieldNotCombineModels)
            {
                field.MergePropertyValue(this.BaseModel, this.NewModel);
            }
            return BaseModel;
        }
    }

    /// <summary>
    /// базовая модель для объединения
    /// </summary>
    /// <typeparam name="TYpeOfMerge">тип модели для объединения</typeparam>
    public class BaseMergeModel<TYpeOfMerge>
    {
        /// <summary>
        /// базовая модель для объединения
        /// </summary>
        public TYpeOfMerge BaseModel { get; set; }
        
        /// <summary>
        /// новая модель для объединения
        /// </summary>
        public TYpeOfMerge NewModel { get; set; }
        
        /// <summary>
        /// действие выбранное для объединения
        /// </summary>
        public MergeModelAction MergeActionModel { get; set; }

        public BaseMergeModel()
        {

        }

        /// <summary>
        /// метод объединения
        /// </summary>
        /// <returns></returns>
        public virtual TYpeOfMerge Combine()
        {
            return BaseModel;
        }
    }

    /// <summary>
    /// базовая модель для объединения
    /// </summary>
    /// <typeparam name="TYpeOfMerge">тип модели для объединения</typeparam>
    public class BaseMergeModelAuto<TYpeOfMerge>
    {
        public BaseMergeModelAuto()
        {

        }
        /// <summary>
        /// метод объединения
        /// </summary>
        /// <returns></returns>
        public virtual TYpeOfMerge Combine(TYpeOfMerge baseModel, TYpeOfMerge newModel)
        {
            return baseModel;
        }
    }

    /// <summary>
    /// модель merge полей
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public class MergeFieldModel
    {
        /// <summary>
        /// действие merge
        /// </summary>
        public MergeFieldAction FieldAction { get; set; }
        /// <summary>
        /// ссылка на поле
        /// </summary>
        public FieldInfo FieldLink { get; set; }
    }

    /// <summary>
    /// модель merge полей, однозначный
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public class MergeFieldNotCombineModel
    {
        /// <summary>
        /// действие merge
        /// </summary>
        public MergeFieldNotCombineAction FieldAction { get; set; }
        /// <summary>
        /// ссылка на поле
        /// </summary>
        public FieldInfo FieldLink { get; set; }
    }

    /// <summary>
    /// переключатель объединения моделей
    /// </summary>
    public enum MergeModelAction
    {
        /// <summary>
        /// Оставить существующее
        /// </summary>
        //[StringValue("Оставить существующее")]
        TakeBase = 1,

        /// <summary>
        /// Объединить
        /// </summary>
        //[StringValue("Объединить")]
        Combine,

        /// <summary>
        /// Создать новый
        /// </summary>
        //[StringValue("Создать новый")]
        TakeNew
    }

    /// <summary>
    /// переключатель merge полей без объединения, однозначный
    /// </summary>
    public enum MergeFieldNotCombineAction
    {
        /// <summary>
        /// Оставить существующее
        /// </summary>
        //[StringValue("Оставить существующее")]
        TakeBase = 1,

        /// <summary>
        /// Взять новое
        /// </summary>
        //[StringValue("Взять новое")]
        TakeLoad = 3
    }

    /// <summary>
    /// переключатель merge полей без объединения, неоднозначный
    /// </summary>
    public enum MergeFieldAction
    {
        /// <summary>
        /// Оставить существующее
        /// </summary>
        //[StringValue("Оставить существующее")]
        TakeBase = 1,

        /// <summary>
        /// Объединить
        /// </summary>
        //[StringValue("Объединить")]
        Combine,

        /// <summary>
        /// Взять новое
        /// </summary>
        //[StringValue("Взять новое")]
        TakeLoad
    }

    /// <summary>
    /// расширение методов объедения полей
    /// </summary>
    public static class MergeFieldActionExtension
    {
        /// <summary>
        /// объединение для строковых полей
        /// </summary>
        /// <param name="action"></param>
        /// <param name="lastValue"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        public static string Merge(this MergeFieldAction action, string lastValue, string currentValue)
        {
            var newValue = string.Empty;
            switch (action)
            {
                case MergeFieldAction.TakeLoad:
                    newValue = currentValue;
                    break;
                case MergeFieldAction.Combine:
                    newValue = $"{lastValue},{currentValue}";
                    break;
                case MergeFieldAction.TakeBase:
                    break;
                default:
                    newValue = lastValue;
                    break;
            }
            return newValue;
        }

        /// <summary>
        /// объединение полей, характерное для списков
        /// </summary>
        /// <param name="action"></param>
        /// <param name="lastValue"></param>
        /// <param name="currentValue"></param>
        /// <typeparam name="T"></typeparam>
        public static void Merge<T>(this MergeFieldAction action, List<T> lastValue, List<T> currentValue)
        {
            switch (action)
            {
                case MergeFieldAction.TakeLoad:
                    lastValue.Clear();
                    lastValue.AddRange(currentValue);
                    break;
                case MergeFieldAction.Combine:
                    lastValue.AddRange(currentValue);
                    break;
                case MergeFieldAction.TakeBase:
                    break;
            }
        }

        /// <summary>
        /// объединение характерное для однозначных решений по объединению
        /// </summary>
        /// <param name="action"></param>
        /// <param name="lastValue"></param>
        /// <param name="currentValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Merge<T>(this MergeFieldNotCombineAction action, T lastValue, T currentValue)
        {
            T newValue;
            switch (action)
            {
                case MergeFieldNotCombineAction.TakeBase:
                    newValue = lastValue;
                    break;
                case MergeFieldNotCombineAction.TakeLoad:
                    newValue = currentValue;
                    break;
                default:
                    newValue = lastValue;
                    break;
            }
            return newValue;
        }
    }
}