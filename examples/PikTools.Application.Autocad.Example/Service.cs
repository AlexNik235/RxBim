﻿namespace PikTools.Application.Autocad.Example
{
    using Autodesk.AutoCAD.ApplicationServices.Core;

    /// <inheritdoc />
    public class Service : IService
    {
        /// <inheritdoc/>
        public void Go()
        {
            Application.ShowAlertDialog($"PikToolsApplication example app. AutoCAD version: {Application.Version}");
        }
    }
}