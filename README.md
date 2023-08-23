# Dispatched Task Runner (Parallel Jobs Dispatcher)

The **TasksDispatcher.cs** is a C# class that allows you to efficiently dispatch and execute multiple asynchronous tasks in parallel while limiting the number of concurrently executing tasks. This can be especially useful when you have a large number of tasks to execute, and you want to control the level of parallelism.

## Table of Contents

- [Usage](#usage)
- [Example](#example)
- [Thread Safety](#thread-safety)
- [Contributing](#contributing)
- [License](#license)

## Usage

### Initialization

To use the `TasksDispatcher`, you need to create an instance of it by providing a list of asynchronous functions (`Func<Task<TOut>>`) and specifying the maximum number of parallel jobs you want to run concurrently.

```csharp
List<Func<Task<TOut>>> jobs = /* Initialize your list of tasks */;
int maxParallelJobsCount = /* Set the maximum number of parallel jobs */;
TasksDispatcher<TOut> dispatcher = new TasksDispatcher<TOut>(jobs, maxParallelJobsCount);
```
### Running Jobs

```csharp
await foreach (var result in dispatcher.RunAll())
{
    // Handle the completed task result here
}
```

The `RunAll` method returns an asynchronous enumerable (`IAsyncEnumerable<TOut>`) that allows you to iterate through the results of the dispatched tasks as they complete. This allows for efficient asynchronous processing without blocking the main thread.

## Example
Here's a simple example of how to use the `TasksDispatcher`:

```csharp
// Create a list of asynchronous tasks (Func<Task<TOut>>)
List<Func<Task<TOut>>> jobs = new List<Func<Task<TOut>>>
{
    async () => await SomeAsyncOperation1(),
    async () => await SomeAsyncOperation2(),
    // Add more tasks as needed
};

// Initialize the dispatcher with a maximum of 2 parallel jobs
int maxParallelJobsCount = 2;
TasksDispatcher<TOut> dispatcher = new TasksDispatcher<TOut>(jobs, maxParallelJobsCount);

// Execute the tasks and process their results
await foreach (var result in dispatcher.RunAll())
{
    Console.WriteLine("Result: " + result);
}
```
## Thread Safety
The `TasksDispatcher` class is designed with thread safety in mind. It uses asynchronous constructs such as Task and IAsyncEnumerable to handle concurrency. However, it's essential to ensure that the asynchronous methods you provide (`Func<Task<TOut>>`) are themselves thread-safe, especially if they involve shared resources or external state.

## Contributing
Contributions to this project are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License
This project is licensed under the MIT License
