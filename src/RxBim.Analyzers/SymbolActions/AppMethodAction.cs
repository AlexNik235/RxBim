﻿namespace RxBim.Analyzers.SymbolActions
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// Инспекция возвращаемого значения методов приложения. 
    /// </summary>
    public class AppMethodAction
    {
        /// <summary>
        /// id
        /// </summary>
        public const string DiagnosticId = Constants.DiagnosticId + "AppMethodReturType";

        private static readonly LocalizableString Title = "Method returns \"PluginResult\" method.";

        private static readonly LocalizableString MessageFormat =
            "Method '{0}' not returns \"PluginResult\" type";

        private static readonly LocalizableString Description =
            "\"ExecuteCommand\" method should return \"PluginResult\" type.";

        /// <summary>
        /// Правило
        /// </summary>
        public DiagnosticDescriptor Rule { get; } = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Constants.Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: Description);

        /// <summary>
        /// Проверка метода Start
        /// </summary>
        /// <param name="context">контекст</param>
        public void AnalyzeAppStartMethods(SymbolAnalysisContext context)
        {
            AnalyzeMethod(context, Constants.Start);
        }

        /// <summary>
        /// Проверка метода Shutdown
        /// </summary>
        /// <param name="context">контекст</param>
        public void AnalyzeAppShutdownMethods(SymbolAnalysisContext context)
        {
            AnalyzeMethod(context, Constants.Shutdown);
        }

        private void AnalyzeMethod(SymbolAnalysisContext context, string methodName)
        {
            var method = (IMethodSymbol)context.Symbol;

            if (method.ContainingType.BaseType?.Name == Constants.RxBimApplication &&
                method.Name == methodName &&
                method.ReturnType.Name != Constants.PluginResult)
            {
                var diagnostic = Diagnostic.Create(Rule, method.Locations[0], method.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}