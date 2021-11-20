﻿namespace Automated.Testing.System.DataAccess.Postgres.Entities
{
    /// <summary>
    /// Элемент словаря.
    /// </summary>
    public class DictionaryItem
    {
        /// <summary>
        /// Id элемента
        /// </summary>
        public int ElementId { get; set; }
        
        /// <summary>
        /// Наименование элемента
        /// </summary>
        public string Name { get; set; }
    }
}