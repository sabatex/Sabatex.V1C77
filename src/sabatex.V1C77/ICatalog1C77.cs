using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface ICatalog1C77:ICOMObject1C77
    {
        public string Code { get => GetProperty<string>("Code").Trim(); set => SetProperty("Code", value); }
        public bool FindByCode(string code, double? flag = null) => Method<bool>("FindByCode", code, flag);
        public ICatalog1C77 CurrentItem => Method<ICatalog1C77>("ТекущийЭлемент");

        public ICatalog1C77 Owner { get => GetProperty<ICatalog1C77>("Владелец"); set => SetProperty("Владелец", value); }

        public ICatalog1C77 Parent { get => GetProperty<ICatalog1C77>("Родитель"); set => SetProperty("Родитель", value); }
        public string Description
        {
            get => GetProperty<string>("Description");
            set => SetProperty("Description", value);
        }

        public bool DeleteMark => Method<bool>("ПометкаУдаления");
        public void ClearDeleteMark() => Method<object>("ClearDeleteMark");
        public void Delete(bool deleteFromDatabase = true) => Method<object>("Delete");
 
        public bool IsFolder => Method<bool>("ЭтоГруппа");


        public bool SelectItems(bool hierarchically = true) => Method<bool>("ВыбратьЭлементы", hierarchically);


        public bool NextItem(bool hierarchically = true) => Method<bool>("ПолучитьЭлемент", hierarchically);
        /// <summary>
        ///Назначение:
        ///Установить режим выборки групп при интерактивном выборе элемента справочника.
        ///Возвращает:
        ///Текущее числовое значение режима выборки групп(на момент до исполнения метода) при интерактивном выборе элемента справочника.
        ///Замечание:
        ///Метод можно применять как объектов, созданных функцией СоздатьОбъект(интерактивный выбор осуществляется методом Выбрать), так и в диалогах для полей типа справочник.
        ///По умолчанию, выборка элементов справочников для полей диалога в формах документов, журналов и справочников установлена без выбора групп, а в форме отчета - с выбором групп.
        /// </summary>
        /// <param name="withGroup">true - выбирать группы; false - не выбирать группы</param>
        /// <returns></returns>
        public bool SelectGroup(bool withGroup = true) => Method<bool>("SelectGroup", withGroup);

    }
}
