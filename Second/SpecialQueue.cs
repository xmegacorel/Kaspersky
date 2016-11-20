using System;
using System.Collections.Generic;
using System.Threading;

namespace Second
{
    public class SpecialQueue<T>
    {
        //виновница торжества
        private readonly Queue<T> _queue = new Queue<T>();
        
        // используем пользовательские блокировки, ибо операции очень быстрые
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        
        // предполагается, что время появления элемента в очереди несоизмеримо большое с временем тика OS
        private readonly Semaphore _semaphore = new Semaphore(0, Int32.MaxValue);

        public void Push(T value)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _queue.Enqueue(value);
                _semaphore.Release();
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public T Pop()
        {
            _semaphore.WaitOne();
            _cacheLock.EnterReadLock();
            try
            {
                if (_queue.Count > 0)
                    return _queue.Dequeue();
                else
                {
                    throw new AccessViolationException("Очередь пустая, а мы в нее зашли");
                }
            }
            finally 
            {
                _cacheLock.ExitReadLock();
            }
            
        }
        
        
    }
}