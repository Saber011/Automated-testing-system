using System;
using System.Collections.Generic;

namespace Automated.Testing.System.Core.Core
{
    /// <summary>
    /// Статический вспомогательный класс для проверки значений аргументов.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Вызывает исключение <see cref="T:System.ArgumentNullException" />, если значение указанного аргумента - `NULL`.
        /// </summary>
        /// <param name="argumentValue">Значение аргумента.</param>
        /// <param name="argumentName">Имя аргумента.</param>
        public static void NotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }
        
        /// <summary>
        /// Вызывает исключение <see cref="T:System.ArgumentException" />, если значение указанного аргумента - пустая строка.
        /// </summary>
        /// <param name="argumentValue">Значение аргумента.</param>
        /// <param name="argumentName">Имя аргумента.</param>
        public static void NotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
                throw new ArgumentException("Строка не может быть пустой.", argumentName);
        }
        
        /// <summary>
        /// Вызывает исключение <see cref="T:System.ArgumentOutOfRangeException" />, если значение указанного аргумента меньше или равно нулю.
        /// </summary>
        /// <typeparam name="TArg">Тип аргумента.</typeparam>
        /// <param name="argumentValue">Значение аргумента.</param>
        /// <param name="argumentName">Имя аргумента.</param>
        public static void GreaterThanZero<TArg>(TArg argumentValue, string argumentName) where TArg : struct, IComparable
        {
            if (argumentValue.CompareTo(default (TArg)) != 1)
                throw new ArgumentOutOfRangeException(argumentName);
        }
        
        /// <summary>
        /// Вызывает исключение <see cref="ArgumentNullException"/>, если значение указанного аргумента - `NULL`.
        /// Если значение аргумента - пустой список, то вызывает исключение <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="T">Тип элементов в списке.</typeparam>
        /// <param name="argumentValue">Значение аргумента.</param>
        /// <param name="argumentName">Имя аргумента.</param>
        public static void NotNullOrEmpty<T>(IReadOnlyList<T> argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (argumentValue.Count == 0)
            {
                throw new ArgumentException(@"Список не может быть пустым.", argumentName);
            }
        }
    }

}