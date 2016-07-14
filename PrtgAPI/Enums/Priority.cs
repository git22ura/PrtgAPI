﻿using System.Xml.Serialization;

namespace Prtg
{
    /// <summary>
    /// Specifies the order in which objects are displayed in PRTG table lists, with objects with higher priorities displayed first.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Priority 1 (lowest priority).
        /// </summary>
        [XmlEnum("1")]
        One = 1,

        /// <summary>
        /// Priority 2.
        /// </summary>
        [XmlEnum("2")]
        Two = 2,

        /// <summary>
        /// Prioroty 3.
        /// </summary>
        [XmlEnum("3")]
        Three = 3,

        /// <summary>
        /// Priority 4.
        /// </summary>
        [XmlEnum("4")]
        Four = 4,

        /// <summary>
        /// Priority 5 (highest priority).
        /// </summary>
        [XmlEnum("5")]
        Five = 5
    }
}