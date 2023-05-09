using System.Threading.Tasks;

namespace DispatchedTasksRunner
{
    public interface ICalledJob<in TIn, TOut>
    {
        public Task<TOut> Run(TIn? input);
    }
}