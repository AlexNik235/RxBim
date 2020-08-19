﻿namespace PikTools.CommandExample
{
    using Di;
    using SimpleInjector;

    /// <inheritdoc />
    public class MyCfg : IPluginConfiguration
    {
        /// <inheritdoc />
        public void Configure(Container container)
        {
            container.Register<IMyService, MyService>();
        }
    }
}