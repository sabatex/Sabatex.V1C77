// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public class AttributeMetadata1C77:ObjectMetadata1C77, IAttributeMetadata1C77
    {
        /// <summary>
        /// Тип - тип реквизита справочника (Cтрока)
        /// </summary>
        public string Тип { get; set; }
        /// <summary>
        /// Вид - вид реквизита справочника (Cтрока), (если тип - Справочник)
        /// </summary>
        public string Вид { get; set; }
        /// <summary>
        /// Длина - длина (Число), (если тип - Строка или Число)
        /// </summary>
        public double Длина { get; set; }
        /// <summary>
        /// Точность - точность (если тип - Число)
        /// </summary>
        public double Точность { get; set; }
        /// <summary>
        /// НеОтрицательный - запрет отрицательных значений (Число [0 / 1])
        /// </summary>
        public bool НеОтрицательный { get; set; }
        /// <summary>
        /// РазделятьТриады - разделять триады (Число [0 / 1])
        /// </summary>
        public bool РазделятьТриады { get; set; }
    }
}
