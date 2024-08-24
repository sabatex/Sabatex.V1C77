// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77
{
    public interface ICatalog1C77:ICOMObject1C77
    {
        string Code { get; set; }
        bool FindByCode(string code, double flag);
        bool FindByCode(string code);
        ICatalog1C77 CurrentItem { get; }
        ICatalog1C77 Owner { get; set; }
        ICatalog1C77 Parent { get; set; }
        string Description { get; set; }
        bool DeleteMark { get; }
        void ClearDeleteMark();
        void Delete(bool deleteFromDatabase);
        void Delete();
        bool IsFolder { get; }
        bool SelectItems(bool hierarchically);
        bool SelectItems();
        bool NextItem(bool hierarchically);
        bool NextItem();
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
        bool SelectGroup(bool withGroup);
        bool SelectGroup();
    }
}
