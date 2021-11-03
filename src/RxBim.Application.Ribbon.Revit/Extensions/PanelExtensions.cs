﻿namespace RxBim.Application.Ribbon.Revit.Extensions
{
    using System;
    using Abstractions;
    using Autodesk.Revit.UI;

    /// <summary>
    /// Расширения панели
    /// </summary>
    public static class PanelExtensions
    {
        /// <summary>
        /// Create push button on the panel
        /// </summary>
        /// <param name="panel">Panel</param>
        /// <param name="name">Internal name of the button</param>
        /// <param name="text">Text user will see</param>
        /// <param name="action">Additional action with whe button</param>
        /// <returns>Panel where button were created</returns>
        public static IPanel Button<TExternalCommandClass>(
            this IPanel panel,
            string name,
            string text,
            Action<IButton> action = null)
            where TExternalCommandClass : class, IExternalCommand
        {
            var commandClassType = typeof(TExternalCommandClass);
            return panel.Button(name, text, commandClassType, action);
        }
    }
}