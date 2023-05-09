using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispatchedTasksRunner
{
    public class TasksDispatcher<TOut>
    {
        private readonly List<Func<Task<TOut>>> _jobs;
        private readonly int _limit;
        private int _offset;

        public TasksDispatcher(List<Func<Task<TOut>>> jobs, int maxParallelJobsCount)
        {
            _limit = maxParallelJobsCount;
            _jobs = jobs;
        }

        public async IAsyncEnumerable<TOut> RunAll()
        {
            await foreach (var jobResults in RunAllBatches())
            {
                foreach (var jobResult in jobResults)
                {
                    yield return jobResult;
                }
            }
        }

        private async IAsyncEnumerable<TOut[]> RunAllBatches()
        {
            while (_offset <= _jobs.Count)
            {
                var batch = _jobs.Skip(_offset).Take(_limit);
                yield return await RunBatch(batch);
                _offset += _limit;
            }
        }

        private async Task<TOut[]> RunBatch(IEnumerable<Func<Task<TOut>>> jobs)
        {
            var tasks = new List<Task<TOut>>();
            foreach (var job in jobs)
            {
                tasks.Add(job.Invoke());
            }

            return await Task.WhenAll(tasks);
        }
    }
}