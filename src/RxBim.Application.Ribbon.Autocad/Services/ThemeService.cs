﻿namespace RxBim.Application.Ribbon.Autocad.Services
{
    using System;
    using Abstractions;
    using Autodesk.AutoCAD.ApplicationServices;
    using Models;
    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <inheritdoc cref="IThemeService" />
    public class ThemeService : IThemeService, IDisposable
    {
        private const string ThemeVariableName = "COLORTHEME";

        /// <inheritdoc />
        public event EventHandler? ThemeChanged;

        /// <inheritdoc />
        public void Run()
        {
            Application.SystemVariableChanged += ApplicationOnSystemVariableChanged;
        }

        /// <inheritdoc />
        public ThemeType GetCurrentTheme()
        {
            var themeTypeValue = (short)Application.GetSystemVariable(ThemeVariableName);
            return themeTypeValue == 0 ? ThemeType.Dark : ThemeType.Light;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Application.SystemVariableChanged -= ApplicationOnSystemVariableChanged;
        }

        private void ApplicationOnSystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
        {
            if (e.Name.Equals(ThemeVariableName, StringComparison.OrdinalIgnoreCase))
            {
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}