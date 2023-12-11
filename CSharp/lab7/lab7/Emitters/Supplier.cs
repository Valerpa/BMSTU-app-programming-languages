using System.Collections.Concurrent;
namespace lab7;

public class Supplier : BaseEventEmitter
{
    public Supplier(string name, TimeSpan frequency, ConcurrentQueue<int> queue,
        CancellationToken cancellationToken) :
        base(name, frequency, queue, cancellationToken) { }

    public override async Task GenerateEvents()
    {
        int eventId = 1;
        try
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                await Task.Delay((int)_frequency.TotalMilliseconds + _random.Next(-200, 200), CancellationToken);
                
                if (CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                string message = $"{DateTime.Now:HH:mm:ss} - {_name} сгенерировал партию {eventId}";
                Logger.Log(message);
                FileManager.WriteToFile(message);
                Queue.Enqueue(eventId);
                Logger.Log($"Длина очереди эмиттера: {Queue.Count}");
                eventId++;
                TotalGeneratedItems++;
            }
        }
        catch (TaskCanceledException)
        {
            Logger.Log($"Смена для {_name.ToLower()} закончилась!");
        }
    }
}