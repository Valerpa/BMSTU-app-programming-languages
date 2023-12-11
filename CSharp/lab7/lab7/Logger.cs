namespace lab7;

public static class Logger
{
    public static void Log(string message) 
    {
        Console.WriteLine(message);
    }

    public static void LogProcessStart()
    {
        Console.WriteLine("Процесс начался...");
        Console.WriteLine("Нажмите любую клавишу для принудительного заверщения процесса.\n");
    }

    public static void LogProcessFinish()
    {
        Console.WriteLine("Процесс завершён...\n");
        Console.WriteLine("-----------------------------");
    }

    public static void LogMetrics(BaseEventEmitter emitter, IEnumerable<BaseEventHandler> handlers)
    {
        Log($"Поставщиком было сгенерировано {emitter.GetTotalGeneratedItems()} партий");
        Log($"Длина очереди поставщика: {emitter.GetQueueLength()}\n");
        foreach (var handler in handlers)
        {
            int itemsProcessedSorting = handler.EventStartTime.Count;
            TimeSpan totalProcessingHandleTime = handler.GetTotalProcessingTime();
            double averageProcessingTimeSorting = 
                MetricsCalculator.CalculateAverageProcessingTime(totalProcessingHandleTime, handler.ProcessingTime);
            
            int queueLength = handler.OutputQueue.Count;
            double averageQueueLength = MetricsCalculator.CalculateAverageQueueLength(handler.OutputQueue.Count,
                handler.ProcessingTime);
            LogMetricsForHandler($"{handler.Name}", itemsProcessedSorting, averageProcessingTimeSorting, queueLength, 
                averageQueueLength);
        }
    }

    private static void LogMetricsForHandler(string name, int itemsProcessed, double averageProcessingTime, int queueLength, 
        double averageQueueLength)
    {
        Console.WriteLine($"Метрики для обработчика - {name}:");
        Console.WriteLine($"Обработано партий: {itemsProcessed}");
        Console.WriteLine($"Среднее время обработки: {averageProcessingTime} мс");
        Console.WriteLine($"Длина выходной очереди: {queueLength}");
        Console.WriteLine($"Средняя длина очереди: {averageQueueLength}\n");
    }
}