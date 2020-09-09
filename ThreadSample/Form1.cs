using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 创建线程
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => PrintNumbers(false));
            t.Name = "thrad1";
            t.Start();//线程开始执行
            t.Join();//线程等待  类似 await
            t.Abort();
            Thread.CurrentThread.Name = "main";
            PrintNumbers(true);
        }
        static void PrintNumbers(bool ismain)
        {
            string threadname = Thread.CurrentThread.Name;
            Console.WriteLine("Starting..." + threadname);
            for (int i = 1; i < 1000; i++)
            {
                Console.WriteLine(i + threadname);
                if (ismain)
                {
                    //Thread.Sleep(2000);
                }
            }
        }


        #endregion

        #region 线程状态
        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting program...");
            Thread t = new Thread(PrintNumbersWithStatus);
            Thread t2 = new Thread(DoNothing);
            Console.WriteLine(t.ThreadState.ToString());
            t2.Start();
            t.Start();
            for (int i = 1; i < 30; i++)
            {
                Console.WriteLine(t.ThreadState.ToString());
            }
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A thread has been aborted");
            Console.WriteLine(t.ThreadState.ToString());
            Console.WriteLine(t2.ThreadState.ToString());

            Console.ReadKey();
        }
        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumbersWithStatus()
        {
            Console.WriteLine("Starting...");
            Console.WriteLine(Thread.CurrentThread.ThreadState.ToString());
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
        #endregion

        #region 线程优先级
        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Current thread priority: {0}", Thread.CurrentThread.Priority);
            Console.WriteLine("Running on all cores available");
            RunThreads();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Running on a single core");
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            RunThreads();
        }

        static void RunThreads()
        {
            //var sample = new ThreadSample();

            //var threadOne = new Thread(sample.CountNumbers);
            //threadOne.Name = "ThreadOne";
            //var threadTwo = new Thread(sample.CountNumbers);
            //threadTwo.Name = "ThreadTwo";

            //threadOne.Priority = ThreadPriority.Highest;
            //threadTwo.Priority = ThreadPriority.Lowest;
            //threadOne.Start();
            //threadTwo.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //sample.Stop();

            //Console.ReadKey();
        }

        //class ThreadSample
        //{
        //    private bool _isStopped = false;

        //    public void Stop()
        //    {
        //        _isStopped = true;
        //    }

        //    public void CountNumbers()
        //    {
        //        long counter = 0;

        //        while (!_isStopped)
        //        {
        //            counter++;
        //        }

        //        Console.WriteLine("{0} with {1,11} priority " +
        //                    "has a count = {2,13}", Thread.CurrentThread.Name,
        //                    Thread.CurrentThread.Priority,
        //                    counter.ToString("N0"));
        //    }
        #endregion

        #region 前后台线程
        private void button4_Click(object sender, EventArgs e)
        {
            var sampleForeground = new ThreadSample(10);
            var sampleBackground = new ThreadSample(20);

            var threadOne = new Thread(sampleForeground.CountNumbers);
            threadOne.Name = "ForegroundThread";
            var threadTwo = new Thread(sampleBackground.CountNumbers);
            threadTwo.Name = "BackgroundThread";
            threadTwo.IsBackground = true;
            //前台线程执行完  进程会关闭
            threadOne.Start();
            threadTwo.Start();
        }

        class ThreadSample
        {
            private readonly int _iterations;

            public ThreadSample(int iterations)
            {
                _iterations = iterations;
            }
            public void CountNumbers()
            {
                for (int i = 0; i < _iterations; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
                }
            }
        }
        #endregion

        #region lock
        private void button5_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Incorrect counter");

            //var c = new Counter();

            //var t1 = new Thread(() => TestCounter(c));
            //var t2 = new Thread(() => TestCounter(c));
            //var t3 = new Thread(() => TestCounter(c));
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t1.Join();
            //t2.Join();
            //t3.Join();

            //Console.WriteLine("nolock Total count: {0}", c.Count);
            //Console.WriteLine("--------------------------");

            //Console.WriteLine("Correct counter");

            //var c1 = new CounterWithLock();

            //t1 = new Thread(() => TestCounter(c1));
            //t2 = new Thread(() => TestCounter(c1));
            //t3 = new Thread(() => TestCounter(c1));
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t1.Join();
            //t2.Join();
            //t3.Join();
            //Console.WriteLine("lock Total count: {0}", c1.Count);

        }
        //static void TestCounter(CounterBase c)
        //{
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        c.Increment();
        //        c.Decrement();
        //    }
        //}

        //class Counter : CounterBase
        //{
        //    public int Count { get; private set; }

        //    public override void Increment()
        //    {
        //        Count++;
        //    }

        //    public override void Decrement()
        //    {
        //        Count--;
        //    }
        //}

        //class CounterWithLock : CounterBase
        //{
        //    private readonly object _syncRoot = new Object();

        //    public int Count { get; private set; }

        //    public override void Increment()
        //    {
        //        lock (_syncRoot)
        //        {
        //            Count++;
        //        }
        //    }

        //    public override void Decrement()
        //    {
        //        lock (_syncRoot)
        //        {
        //            Count--;
        //        }
        //    }
        //}

        //abstract class CounterBase
        //{
        //    public abstract void Increment();

        //    public abstract void Decrement();
        //}

        #endregion
        #region monitorLock

        private void button6_Click(object sender, EventArgs e)
        {
            object lock1 = new object();
            object lock2 = new object();

            //new Thread(() => LockTooMuch(lock1, lock2)).Start();

            //while (true)
            //{
            //    lock (lock2)
            //    {
            //        Thread.Sleep(1000);
            //        Console.WriteLine("Monitor.TryEnter allows not to get stuck, returning false after a specified timeout is elapsed");
            //        if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(5)))
            //        {
            //            Console.WriteLine("Acquired a protected resource succesfully");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Timeout acquiring a resource!");
            //        }
            //    }
            //}

            new Thread(() => LockTooMuch(lock1, lock2)).Start();

            Console.WriteLine("----------------------------------");
            //lock (lock2)
            //{
            //    Console.WriteLine("This will be a deadlock!");
            //    Thread.Sleep(1000);
            //    lock (lock1)
            //    {
            //        Console.WriteLine("Acquired a protected resource succesfully");
            //    }
            //}
            while (true)
            {
                lock (lock2)
                {
                    Console.WriteLine("This will be a deadlock!");
                    Thread.Sleep(1000);
                    if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(5)))
                    {
                        Console.WriteLine("Acquired a protected resource succesfully");
                    }
                    //lock (lock1)
                    //{
                    //    Console.WriteLine("Acquired a protected resource succesfully");
                    //}
                }
            }

        }

        static void LockTooMuch(object lock1, object lock2)
        {

            for (int i = 0; i < 20; i++)
            {
                lock (lock1)
                {
                    //Thread.Sleep(1000);
                    lock (lock2)
                    {


                    }
                }
            }


        }


        #endregion

        #region 異常處理
        private void button7_Click(object sender, EventArgs e)
        {
            var t = new Thread(FaultyThread);
            t.Start();
            t.Join();

            //try
            //{
            //    t = new Thread(BadFaultyThread);
            //    t.Start();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("We won't get here!");
            //}
        }

        static void BadFaultyThread()
        {
            Console.WriteLine("Starting a faulty thread...");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            throw new Exception("Boom!");
        }

        static void FaultyThread()
        {
            try
            {
                Console.WriteLine("Starting a faulty thread...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("Boom!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception handled: {0}", ex.Message);
            }
        }

        #endregion
        #region 原子操作

        private void button8_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Incorrect counter");

            var c = new Counter();

            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c.Count);
            Console.WriteLine("--------------------------");

            Console.WriteLine("Correct counter");

            var c1 = new CounterNoLock();

            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c1.Count);
        }

        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }

        class Counter : CounterBase
        {
            private int _count;

            public int Count { get { return _count; } }

            public override void Increment()
            {
                _count++;
            }

            public override void Decrement()
            {
                _count--;
            }
        }

        class CounterNoLock : CounterBase
        {
            private int _count;

            public int Count { get { return _count; } }

            public override void Increment()
            {
                Interlocked.Increment(ref _count);
            }

            public override void Decrement()
            {
                Interlocked.Decrement(ref _count);
            }
        }

        abstract class CounterBase
        {
            public abstract void Increment();

            public abstract void Decrement();
        }

        #endregion
        #region 线程同步

        private void button9_Click(object sender, EventArgs e)
        {
            const string MutexName = "CSharpThreadingCookbook";

            using (var m = new Mutex(false, MutexName))
            {
                if (!m.WaitOne(TimeSpan.FromSeconds(5)))
                {
                    Console.WriteLine("Second instance is running!");
                }
                else
                {
                    Console.WriteLine("Running!");
                    //m.ReleaseMutex();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 6; i++)
            {
                string threadName = "Thread " + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
                t.Start();
            }
        }
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

        static void AccessDatabase(string name, int seconds)
        {
            _semaphore.Wait();

            Console.WriteLine("{0} waits to access a database", name);
            Console.WriteLine("{0} was granted an access to a database", name);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0} is completed", name);
            _semaphore.Release();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting two operations");
            var t1 = new Thread(() => PerformOperation("Operation 1 is completed", 4));
            var t2 = new Thread(() => PerformOperation("Operation 2 is completed", 8));
            t1.Start();
            t2.Start();
            _countdown.Wait();
            Console.WriteLine("Both operations have been completed.");
            _countdown.Dispose();
        }
        static CountdownEvent _countdown = new CountdownEvent(2);

        static void PerformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countdown.Signal();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var t1 = new Thread(() => PlayMusic("the guitarist", "play an amazing solo", 5));
            var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));

            t1.Start();
            t2.Start();

        }
        static Barrier _barrier = new Barrier(2, b => Console.WriteLine("End of phase {0}", b.CurrentPhaseNumber + 1));

        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine("----------------------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} starts to {1}", name, message);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} finishes to {1}", name, message);
                _barrier.SignalAndWait();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();

            new Thread(() => Write("Thread 1")) { IsBackground = true }.Start();
            new Thread(() => Write("Thread 2")) { IsBackground = true }.Start();

            Thread.Sleep(TimeSpan.FromSeconds(30));

        }
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _items = new Dictionary<int, int>();

        static void Read()
        {
            Console.WriteLine("Reading contents of a dictionary");
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var key in _items.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _rw.EnterUpgradeableReadLock();
                    if (!_items.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _items[newKey] = 1;
                            Console.WriteLine("New key {0} is added to a dictionary by a {1}", newKey, threadName);
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var t1 = new Thread(UserModeWait);
            var t2 = new Thread(HybridSpinWait);

            Console.WriteLine("Running user mode waiting");
            t1.Start();
            Thread.Sleep(20);
            _isCompleted = true;
            Thread.Sleep(TimeSpan.FromSeconds(1));
            _isCompleted = false;
            Console.WriteLine("Running hybrid SpinWait construct waiting");
            t2.Start();
            Thread.Sleep(5);
            _isCompleted = true;

        }
        static volatile bool _isCompleted = false;

        static void UserModeWait()
        {
            while (!_isCompleted)
            {
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine("Waiting is complete");
        }

        static void HybridSpinWait()
        {
            var w = new SpinWait();
            while (!_isCompleted)
            {
                w.SpinOnce();
                Console.WriteLine(w.NextSpinWillYield);
            }
            Console.WriteLine("Waiting is complete");
        }


        #endregion

        #region 线程池







        private void button15_Click(object sender, EventArgs e)
        {
            const int x = 1;
            const int y = 2;
            const string lambdaState = "lambda state 2";

            ThreadPool.QueueUserWorkItem(AsyncOperation);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            ThreadPool.QueueUserWorkItem(AsyncOperation, "async state");
            Thread.Sleep(TimeSpan.FromSeconds(1));

            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine("Operation state: {0}", state);
                Console.WriteLine("Worker thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }, "lambda state");

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine("Operation state: {0}, {1}", x + y, lambdaState);
                Console.WriteLine("Worker thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }, "lambda state");

            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        private static void AsyncOperation(object state)
        {
            Console.WriteLine("Operation state: {0}", state ?? "(null)");
            Console.WriteLine("Worker thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }







        #endregion

        private void button16_Click(object sender, EventArgs e)
        {
            const int numberOfOperations = 500;
            var sw = new Stopwatch();
            sw.Start();
            UseThreads(numberOfOperations);
            sw.Stop();
            Console.WriteLine("Execution time using threads: {0}", sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            UseThreadPool(numberOfOperations);
            sw.Stop();
            Console.WriteLine("Execution time using threads: {0}", sw.ElapsedMilliseconds);
        }

        static void UseThreads(int numberOfOperations)
        {
            using (var countdown = new CountdownEvent(numberOfOperations))
            {
                Console.WriteLine("Scheduling work by creating threads");
                for (int i = 0; i < numberOfOperations; i++)
                {
                    var thread = new Thread(() =>
                    {
                        Console.Write("{0},", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        countdown.Signal();
                    });
                    thread.Start();
                }
                countdown.Wait();
                Console.WriteLine();
            }
        }

        static void UseThreadPool(int numberOfOperations)
        {
            using (var countdown = new CountdownEvent(numberOfOperations))
            {
                Console.WriteLine("Starting work on a threadpool");
                for (int i = 0; i < numberOfOperations; i++)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        Console.Write("{0},", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                        countdown.Signal();
                    });
                }
                countdown.Wait();
                Console.WriteLine();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }
        async static Task AsynchronousProcessing()
        {
            Console.WriteLine("1. Single exception");

            try
            {
                string result = await GetInfoAsync("Task 1", 2);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception details: {0}", ex);
            }

            Console.WriteLine();
            Console.WriteLine("2. Multiple exceptions");

            Task<string> t1 = GetInfoAsync("Task 1", 3);
            Task<string> t2 = GetInfoAsync("Task 2", 2);
            try
            {
                string[] results = await Task.WhenAll(t1, t2);
                Console.WriteLine(results.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception details: {0}", ex);
            }

            Console.WriteLine();
            Console.WriteLine("2. Multiple exceptions with AggregateException");

            t1 = GetInfoAsync("Task 1", 3);
            t2 = GetInfoAsync("Task 2", 2);
            Task<string[]> t3 = Task.WhenAll(t1, t2);
            try
            {
                string[] results = await t3;
                Console.WriteLine(results.Length);
            }
            catch
            {
                var ae = t3.Exception.Flatten();
                var exceptions = ae.InnerExceptions;
                Console.WriteLine("Exceptions caught: {0}", exceptions.Count);
                foreach (var e in exceptions)
                {
                    Console.WriteLine("Exception details: {0}", e);
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        async static Task<string> GetInfoAsync(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            throw new Exception(string.Format("Boom from {0}!", name));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            while (true)
            {
                //Task.Run(() =>
                //{
                //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                //});

                Parallel.For(0, 100000, (i) => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); });
                
                //Parallel.Invoke(() =>
                //{
                //    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                //});
            }
        }
    }



}
