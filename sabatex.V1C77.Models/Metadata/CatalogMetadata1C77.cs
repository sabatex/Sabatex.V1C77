// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public class CatalogMetadata1C77:ObjectMetadata1C77
    {
        /// <summary>
        /// owner as Номенклатура or empty
        /// </summary>
        public string Владелец { get; set; }
        /// <summary>
        /// количество уровней в справочнике (Число)
        /// </summary>
        public int КоличествоУровней { get; set; }
        /// <summary>
        /// длина кода элемента/группы справочника (Число)
        /// </summary>
        public int ДлинаКода { get; set; }
        /// <summary>
        /// длина наименования элемента/группы справочника (Число)
        /// </summary>
        public int ДлинаНаименования { get; set; }
        /// <summary>
        /// вариант назначения кодов справочника (Cтрока ["ВПределахПодчинения" / "ВесьСправочник"])
        /// </summary>
        public string СерииКодов { get; set; }
        /// <summary>
        /// тип кода (Cтрока ["Числовой" / "Текстовый"])
        /// </summary>
        public string ТипКода { get; set; }
        /// <summary>
        /// режим представления элемента/группы справочника (Cтрока ["ВВидеКода" / "ВВидеНаименования"])
        /// </summary>
        public string ОсновноеПредставление { get; set; }
        /// <summary>
        /// контроль уникальность элементов справочника (Число [0 / 1])
        /// </summary>
        public bool КонтрольУникальности { get; set; }
        /// <summary>
        /// автоматическая нумерация элементов/групп справочника (Число [1 / 2]). 1 - нет автонумерации, 2 - есть
        /// </summary>
        public bool АвтоНумерация { get; set; }


        public IEnumerable<CatalogAttributeMetadata1C77> Attributes { get; set; }


    }
}
