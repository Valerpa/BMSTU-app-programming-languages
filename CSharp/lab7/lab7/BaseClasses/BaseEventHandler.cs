using System.Collections.Concurrent;

namespace lab7;
public abstract class BaseEventHandler
{
    public string Name;
    public TimeSpan ProcessingTime;
    protected ConcurrentQueue<int> InputQueue;
    public readonly ConcurrentQueue<int> OutputQueue;
    protected Random Random = new Random();
    protected CancellationToken CancellationToken;
    public ConcurrentDictionary<int, DateTime> EventStartTime = new ConcurrentDictionary<int, DateTime>();
    public int QueueSum = 0;
    protected BaseEventHandler(string name, TimeSpan processingTime, ConcurrentQueue<int> inputQueue,
        ConcurrentQueue<int> outputQueue, CancellationToken cancellationToken)

    {
        Name = name;
        ProcessingTime = processingTime;
        InputQueue = inputQueue;
        OutputQueue = outputQueue;
        CancellationToken = cancellationToken;
    }
    public virtual async Task HandleEvents() {}
    public TimeSpan GetTotalProcessingTime()
    {
        TimeSpan totalProcessingTime = TimeSpan.Zero;

        foreach (var pair in EventStartTime)
        {
            var start = pair.Value;
            var end = DateTime.Now;
            TimeSpan processingTime = end - start;
            totalProcessingTime += processingTime;
        }
        
        return totalProcessingTime;
    }
}