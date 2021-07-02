﻿namespace PikTools.Nuke.Generators.Extensions
{
    using System;
    using Models;

    /// <summary>
    /// Расширения для строк
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Имя типа в <see cref="PluginType"/>
        /// </summary>
        /// <param name="type">имя типа</param>
        public static PluginType ToPluginType(this string type)
        {
            return type switch
            {
                Constants.PikToolsCommand => PluginType.Command,
                Constants.PikToolsApplication => PluginType.Application,
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Преобразует строку в имя свойства для csproj
        /// </summary>
        /// <param name="str">исходная строка</param>
        public static string ToPropertyName(this string str)
        {
            return str.Replace(".", "_");
        }
    }
}