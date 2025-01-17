﻿namespace RxBim.Application.Revit
{
    using System;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Events;
    using Di;
    using Shared;
    using Result = Autodesk.Revit.UI.Result;

    /// <summary>
    /// Revit application
    /// </summary>
    public abstract class RxBimApplication : IExternalApplication
    {
        private bool _contextCreated;
        private UIControlledApplication _application;
        private ApplicationDiConfigurator _diConfigurator;

        /// <inheritdoc />
        public Result OnStartup(UIControlledApplication application)
        {
            _application = application;
            application.Idling += ApplicationIdling;
            return Result.Succeeded;
        }

        /// <inheritdoc />
        public Result OnShutdown(UIControlledApplication application)
        {
            var methodCaller = _diConfigurator.Container.GetService<IMethodCaller<PluginResult>>();
            var result = methodCaller.InvokeMethod(_diConfigurator.Container, Constants.ShutdownMethodName);
            return result.MapResultToRevitResult();
        }

        private void ApplicationIdling(object sender, IdlingEventArgs e)
        {
            if (sender is UIApplication uiApp && !_contextCreated)
            {
                try
                {
                    _diConfigurator = new ApplicationDiConfigurator(this, _application, uiApp);
                    _diConfigurator.Configure(GetType().Assembly);

                    var methodCaller = _diConfigurator.Container.GetService<IMethodCaller<PluginResult>>();
                    methodCaller.InvokeMethod(_diConfigurator.Container, Constants.StartMethodName);

                    _contextCreated = true;
                }
                catch (Exception exception)
                {
                    TaskDialog.Show("Error", exception.ToString());
                    throw;
                }
                finally
                {
                    _application.Idling -= ApplicationIdling;
                }
            }
        }
    }
}