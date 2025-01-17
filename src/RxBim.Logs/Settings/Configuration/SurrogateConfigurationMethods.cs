﻿#pragma warning disable SA1600
namespace RxBim.Logs.Settings.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Serilog;
    using Serilog.Configuration;
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Contains "fake extension" methods for the Serilog configuration API.
    /// By default the settings know how to find extension methods, but some configuration
    /// are actually "regular" method calls and would not be found otherwise.
    ///
    /// This static class contains internal methods that can be used instead.
    ///
    /// </summary>
    internal static class SurrogateConfigurationMethods
    {
        private static readonly Dictionary<Type, MethodInfo[]> SurrogateMethodCandidates = typeof(SurrogateConfigurationMethods)
            .GetTypeInfo().DeclaredMethods
            .GroupBy(m => m.GetParameters().First().ParameterType)
            .ToDictionary(g => g.Key, g => g.ToArray());

#pragma warning disable SA1202
        internal static readonly MethodInfo[] WriteTo = SurrogateMethodCandidates[typeof(LoggerSinkConfiguration)];
        internal static readonly MethodInfo[] AuditTo = SurrogateMethodCandidates[typeof(LoggerAuditSinkConfiguration)];
        internal static readonly MethodInfo[] Enrich = SurrogateMethodCandidates[typeof(LoggerEnrichmentConfiguration)];
        internal static readonly MethodInfo[] Destructure = SurrogateMethodCandidates[typeof(LoggerDestructuringConfiguration)];
        internal static readonly MethodInfo[] Filter = SurrogateMethodCandidates[typeof(LoggerFilterConfiguration)];
#pragma warning restore SA1202

        /*
        Pass-through calls to various Serilog config methods which are
        implemented as instance methods rather than extension methods.
        ConfigurationReader adds those to the already discovered extension methods
        so they can be invoked as well.
        */

        // ReSharper disable UnusedMember.Local
        // those methods are discovered through reflection by `SurrogateMethodCandidates`
        // ReSharper has no way to see that they are actually used ...

        // .WriteTo...
        // ========
        private static LoggerConfiguration Sink(
            LoggerSinkConfiguration loggerSinkConfiguration,
            ILogEventSink sink,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            => loggerSinkConfiguration.Sink(sink, restrictedToMinimumLevel, levelSwitch);

        private static LoggerConfiguration Logger(
            LoggerSinkConfiguration loggerSinkConfiguration,
            Action<LoggerConfiguration> configureLogger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            => loggerSinkConfiguration.Logger(configureLogger, restrictedToMinimumLevel, levelSwitch);

        // .AuditTo...
        // ========
        private static LoggerConfiguration Sink(
            LoggerAuditSinkConfiguration auditSinkConfiguration,
            ILogEventSink sink,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            => auditSinkConfiguration.Sink(sink, restrictedToMinimumLevel, levelSwitch);

        private static LoggerConfiguration Logger(
            LoggerAuditSinkConfiguration auditSinkConfiguration,
            Action<LoggerConfiguration> configureLogger,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            => auditSinkConfiguration.Logger(configureLogger, restrictedToMinimumLevel, levelSwitch);

        // .Filter...
        // =======
        // TODO: add overload for array argument (ILogEventEnricher[])
        // expose `With(params ILogEventFilter[] filters)` as if it was `With(ILogEventFilter filter)`
        private static LoggerConfiguration With(LoggerFilterConfiguration loggerFilterConfiguration, ILogEventFilter filter)
            => loggerFilterConfiguration.With(filter);

        // .Destructure...
        // ============
        // TODO: add overload for array argument (IDestructuringPolicy[])
        // expose `With(params IDestructuringPolicy[] destructuringPolicies)` as if it was `With(IDestructuringPolicy policy)`
        private static LoggerConfiguration With(LoggerDestructuringConfiguration loggerDestructuringConfiguration, IDestructuringPolicy policy)
            => loggerDestructuringConfiguration.With(policy);

        private static LoggerConfiguration ToMaximumDepth(LoggerDestructuringConfiguration loggerDestructuringConfiguration, int maximumDestructuringDepth)
            => loggerDestructuringConfiguration.ToMaximumDepth(maximumDestructuringDepth);

        private static LoggerConfiguration ToMaximumStringLength(LoggerDestructuringConfiguration loggerDestructuringConfiguration, int maximumStringLength)
            => loggerDestructuringConfiguration.ToMaximumStringLength(maximumStringLength);

        private static LoggerConfiguration ToMaximumCollectionCount(LoggerDestructuringConfiguration loggerDestructuringConfiguration, int maximumCollectionCount)
            => loggerDestructuringConfiguration.ToMaximumCollectionCount(maximumCollectionCount);

        private static LoggerConfiguration AsScalar(LoggerDestructuringConfiguration loggerDestructuringConfiguration, Type scalarType)
            => loggerDestructuringConfiguration.AsScalar(scalarType);

        // .Enrich...
        // =======
        // expose `With(params ILogEventEnricher[] enrichers)` as if it was `With(ILogEventEnricher enricher)`
        private static LoggerConfiguration With(
            LoggerEnrichmentConfiguration loggerEnrichmentConfiguration,
            ILogEventEnricher enricher)
            => loggerEnrichmentConfiguration.With(enricher);

        private static LoggerConfiguration AtLevel(
            LoggerEnrichmentConfiguration loggerEnrichmentConfiguration,
            Action<LoggerEnrichmentConfiguration> configureEnricher,
            LogEventLevel enrichFromLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null)
            => levelSwitch != null ? loggerEnrichmentConfiguration.AtLevel(levelSwitch, configureEnricher)
                                   : loggerEnrichmentConfiguration.AtLevel(enrichFromLevel, configureEnricher);

        private static LoggerConfiguration FromLogContext(LoggerEnrichmentConfiguration loggerEnrichmentConfiguration)
            => loggerEnrichmentConfiguration.FromLogContext();

        // ReSharper restore UnusedMember.Local
    }
}
