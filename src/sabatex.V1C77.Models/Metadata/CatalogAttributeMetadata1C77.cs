// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public class CatalogAttributeMetadata1C77:AttributeMetadata1C77,IAttributeSortedMetadata1C77,IAttributePeriodicMetadata1C77
    {
        /// <summary>
        /// показывает, возможен ли отбор или поиск по данному реквизиту методами НайтиПоРеквизиту() или ВыбратьЭлементыПоРеквизиту()(Число [0 / 1])
        /// </summary>
        public bool Сортировка { get; set; }
        /// <summary>
        /// Периодический - является ли данный реквизит справочника периодическим (Число [0 / 1])
        /// </summary>
        public bool Периодический { get; set; }


    }
}
