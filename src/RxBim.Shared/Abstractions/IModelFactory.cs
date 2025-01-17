﻿namespace RxBim.Shared.Abstractions
{
    using System;

    /// <summary>
    /// Фабрика моделей
    /// </summary>
    public interface IModelFactory
    {
        /// <summary>
        /// Создает экземпляр указанного типа, зарегистрированного в контейнере
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        T GetInstance<T>()
            where T : class;

        /// <summary>
        /// Создает экземпляр указанного типа, зарегистрированного в контейнере
        /// </summary>
        /// <param name="type">Тип объекта</param>
        object GetInstance(Type type);
    }
}
