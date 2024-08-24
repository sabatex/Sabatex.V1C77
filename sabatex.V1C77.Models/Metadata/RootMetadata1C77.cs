// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public class RootMetadata1C77
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Идентификатор -идентификатор реквизита справочника (Cтрока)
        /// </summary>
        public string Идентификатор { get; set; }
        /// <summary>
        /// Синоним - синоним реквизита справочника (Cтрока)
        /// </summary>
        public string Синоним { get; set; }
        /// <summary>
        /// Комментарий - комментарий реквизита справочника (Cтрока)
        /// </summary>
        public string Комментарий { get; set; }
        public Dictionary<string, ConstantMetadata1C77> Константы { get; set; }
        public Dictionary<string, CatalogMetadata1C77> Справочники { get; set; }
        public Dictionary<string, CommonDocummentAttributeMetadata1C77> ОбщиеРеквизитыДокумента { get; set; }
        public Dictionary<string, DocummentMetadata1C77> Документы { get; set; }
        public Dictionary<string, EnumMetadata1C77> Перечисления { get; set; }
        //public Dictionary<string, PlanCountMetadata1C77> ПланСчетов { get; set; }
        public static string[] ObjectNames = new string[]
        {
            "Константы","Справочники","ОбщиеРеквизитыДокумента","Документы","Перечисления"
        };

        public object this[string name] 
        {
            get {
                switch (name)
                {
                    case "Константы":return Константы;
                    case "Справочники":return Справочники;
                    case "ОбщиеРеквизитыДокумента":return ОбщиеРеквизитыДокумента;
                    case "Документы":return Документы;
                    case "Перечисления":return Перечисления;
                    default:
                        throw new KeyNotFoundException($"He існує ключа {name}");
                }
            }
        
        }

        
    }
}
