using System;
using System.Threading;

namespace CountServer;
/// <summary>
/// Статический сервер для хранения и модификации счётчика <c>count</c> в многопоточной среде.
/// Гарантирует потокобезопасный доступ: параллельное чтение и эксклюзивную запись.
/// </summary>
public static class CountServer
{
    /// <summary>
    /// Текущее значение счётчика.
    /// </summary>
    private static int _count = 0;

    /// <summary>
    /// Примитив синхронизации, обеспечивающий разделение доступа на чтение и запись.
    /// </summary>
    private static readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

    /// <summary>
    /// Возвращает текущее значение счётчика <c>count</c>.
    /// </summary>
    /// <returns>Текущее значение <c>count</c> типа <see cref="int"/>.</returns>
    public static int GetCount()
    {
        _rwLock.EnterReadLock();
        try
        {
            return _count;
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    /// <summary>
    /// Добавляет указанное значение к счётчику <c>count</c>.
    /// </summary>
    /// <param name="value">Значение, которое необходимо прибавить к счётчику.</param>
    public static void AddToCount(int value)
    {
        _rwLock.EnterWriteLock();
        try
        {
            _count += value;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }
}