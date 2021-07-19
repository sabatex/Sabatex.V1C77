// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public abstract class ObjectMetadata1C77
    {
        /// <summary>
        /// object id 
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
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
    }
}
