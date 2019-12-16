using System.Collections.Generic;
using System.Linq;

namespace System.Threading.Tasks
{
    public static class TaskUtil
    {
        public static async Task<IEnumerable<TResult>> Concurrence<TResult, T1, T2>(
           Task<IEnumerable<T1>> task1,
           Task<IEnumerable<T2>> task2,
           Func<IEnumerable<T1>, IEnumerable<TResult>> convertor1,
           Func<IEnumerable<T2>, IEnumerable<TResult>> convertor2,
           bool continueOnCapturedContext = false)
        {
            IList<TResult> results = new List<TResult>();

            IList<Task> tasks = new List<Task> { task1, task2 };

            while (tasks.Any())
            {
                Task finished = await Task.WhenAny(tasks).ConfigureAwait(continueOnCapturedContext);
                tasks.Remove(finished);

                if (finished == task1)
                {
                    IEnumerable<T1> t1s = await task1.ConfigureAwait(continueOnCapturedContext);

                    results.Add(convertor1(t1s));
                }

                if (finished == task2)
                {
                    IEnumerable<T2> t2s = await task2.ConfigureAwait(continueOnCapturedContext);

                    results.Add(convertor2(t2s));
                }
            }

            return results;
        }

        public static async Task<IEnumerable<TResult>> Concurrence<TResult, T1, T2, T3>(
           Task<IEnumerable<T1>> task1,
           Task<IEnumerable<T2>> task2,
           Task<IEnumerable<T3>> task3,
           Func<IEnumerable<T1>, IEnumerable<TResult>> convertor1,
           Func<IEnumerable<T2>, IEnumerable<TResult>> convertor2,
           Func<IEnumerable<T3>, IEnumerable<TResult>> convertor3,
           bool continueOnCapturedContext = false)
        {
            IList<TResult> results = new List<TResult>();

            IList<Task> tasks = new List<Task> { task1, task2, task3 };

            while (tasks.Any())
            {
                Task finished = await Task.WhenAny(tasks).ConfigureAwait(continueOnCapturedContext);
                tasks.Remove(finished);

                if (finished == task1)
                {
                    IEnumerable<T1> t1s = await task1.ConfigureAwait(continueOnCapturedContext);

                    results.Add(convertor1(t1s));
                }

                if (finished == task2)
                {
                    IEnumerable<T2> t2s = await task2.ConfigureAwait(continueOnCapturedContext);

                    results.Add(convertor2(t2s));
                }

                if (finished == task3)
                {
                    IEnumerable<T3> t3s = await task3.ConfigureAwait(continueOnCapturedContext);

                    results.Add(convertor3(t3s));
                }
            }

            return results;
        }

        public static Task Concurrence(IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks);
        }

        public static Task<TResult[]> Concurrence<TResult>(IEnumerable<Task<TResult>> tasks)
        {
            return Task.WhenAll(tasks);
        }

        public static async Task<IEnumerable<TResult>> Concurrence<TResult, T>(IEnumerable<Task<T>> tasks, Func<T, TResult> convertor, bool continueOnCapturedContext = false)
        {
            List<TResult> results = new List<TResult>();

            foreach (Task<Task<T>> bucket in OrderByFinishedSequence(tasks))
            {
                Task<T> t = await bucket.ConfigureAwait(continueOnCapturedContext);

                T finishedT = await t.ConfigureAwait(continueOnCapturedContext);

                results.Add(convertor(finishedT));
            }

            return results;
        }

        public static async Task<IEnumerable<TResult>> Concurrence<TResult, T>(IEnumerable<Task<IEnumerable<T>>> tasks, Func<T, TResult> convertor, bool continueOnCapturedContext = false)
        {
            List<TResult> results = new List<TResult>();

            foreach (Task<Task<IEnumerable<T>>> bucket in OrderByFinishedSequence(tasks))
            {
                Task<IEnumerable<T>> t = await bucket.ConfigureAwait(continueOnCapturedContext);

                IEnumerable<T> finishedTs = await t.ConfigureAwait(continueOnCapturedContext);

                finishedTs.ForEach(item => results.Add(convertor(item)));
            }

            return results;
        }

        /// <summary>
        /// https://devblogs.microsoft.com/pfxteam/processing-tasks-as-they-complete/
        /// 按谁先执行完的顺序返回任务数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static Task<Task<T>>[] OrderByFinishedSequence<T>(IEnumerable<Task<T>> tasks)
        {
            List<Task<T>> inputTasks = tasks.ToList();

            TaskCompletionSource<Task<T>>[] buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
            Task<Task<T>>[] results = new Task<Task<T>>[buckets.Length];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new TaskCompletionSource<Task<T>>();
                results[i] = buckets[i].Task;
            }

            int nextTaskIndex = -1;

            foreach (Task<T> inputTask in inputTasks)
            {
                inputTask.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            }

            return results;

            void continuation(Task<T> completed)
            {
                TaskCompletionSource<Task<T>> bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
                bucket.TrySetResult(completed);
            }
        }
    }
}
