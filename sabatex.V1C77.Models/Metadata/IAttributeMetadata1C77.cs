// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public interface IAttributeMetadata1C77
    {
        /// <summary>
        /// Тип - тип реквизита справочника (Cтрока)
        /// </summary>
        string Тип { get; set; }
        /// <summary>
        /// Вид - вид реквизита справочника (Cтрока), (если тип - Справочник)
        /// </summary>
        string Вид { get; set; }
        /// <summary>
        /// Длина - длина (Число), (если тип - Строка или Число)
        /// </summary>
        double Длина { get; set; }
        /// <summary>
        /// Точность - точность (если тип - Число)
        /// </summary>
        double Точность { get; set; }
        /// <summary>
        /// НеОтрицательный - запрет отрицательных значений (Число [0 / 1])
        /// </summary>
        bool НеОтрицательный { get; set; }
        /// <summary>
        /// РазделятьТриады - разделять триады (Число [0 / 1])
        /// </summary>
        bool РазделятьТриады { get; set; }
    }
}
