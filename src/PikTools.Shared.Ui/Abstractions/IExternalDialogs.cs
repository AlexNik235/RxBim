﻿namespace PikTools.Shared.Ui.Abstractions
{
    using CSharpFunctionalExtensions;

    /// <summary>
    /// Работа с внешними окнами и диалогами
    /// </summary>
    public interface IExternalDialogs
    {
        /// <summary>
        /// Показать диалог сохранения файла
        /// </summary>
        /// <param name="title">Заголовок</param>
        /// <param name="filter">Фильтр по расширениям</param>
        /// <param name="initialDirectory">Открываемая папка по-умолчанию</param>
        /// <param name="fileName">Предлагаемое имя файла</param>
        /// <returns>Путь к выбранному для сохранения файлу</returns>
        Result<string> ShowSaveFileDialog(
            string title,
            string filter,
            string initialDirectory = "",
            string fileName = "");

        /// <summary>
        /// Показать диалог открытия файлов
        /// </summary>
        /// <param name="filter">Фильтр файлов</param>
        /// <param name="addExtension">Добавлять ли расширение к файлу автоматически</param>
        /// <param name="multiSelect">Допускать выбор нескольких файлов</param>
        /// <returns>Массив путей к выбранным файлам</returns>
        Result<string[]> ShowOpenFileDialog(
            string filter,
            bool addExtension,
            bool multiSelect);

        /// <summary>
        /// Открывает проводник на заданной папке
        /// </summary>
        /// <param name="path">Путь к заданной папке</param>
        void OpenFileExplorer(string path);
    }
}
