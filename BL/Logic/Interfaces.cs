using System;

namespace BL
{
    /// <summary>
    /// Наклейка.
    /// </summary>
    public interface ISticker
    {
        /// <summary>
        /// Наличие наклейки.
        /// </summary>
        bool IsSticker { get; set; }
    }
}
