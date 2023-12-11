using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
namespace lab7;

internal static class Program
{
    private static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("/Users/valerikanasha228/Desktop/labs_3sem/CSharp/lab7/lab7/configfile.json", 
                optional: true)
            .Build();
        
        var supplierFrequencySeconds = double.Parse(configuration["EventSettings:SupplierFrequency"]);
        var sorterFrequencySeconds = double.Parse(configuration["EventSettings:SorterProcessingTime"]);
        var fitterFrequencySeconds = double.Parse(configuration["EventSettings:FitterProcessingTime"]);
        
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var supplyQueue = new ConcurrentQueue<int>();
        var sorter1Queue = new ConcurrentQueue<int>();
        var fitter1Queue = new ConcurrentQueue<int>();
        var sorter2Queue = new ConcurrentQueue<int>();
        var fitter2Queue = new ConcurrentQueue<int>();
        var fitter3Queue = new ConcurrentQueue<int>();

        var supplier = new Supplier("Поставщик", TimeSpan.FromSeconds(supplierFrequencySeconds), 
            supplyQueue, cancellationToken);
        var sorter1 = new Sorter("Сортировщик 1", TimeSpan.FromSeconds(sorterFrequencySeconds), 
            supplyQueue, sorter1Queue,
            cancellationToken);
        var sorter2 = new Sorter("Сортировщик 2", TimeSpan.FromSeconds(sorterFrequencySeconds), 
            supplyQueue, sorter2Queue,
            cancellationToken);
        var fitter1 = new Fitter("Сотрудник магазина 1", TimeSpan.FromSeconds(fitterFrequencySeconds), 
            sorter1Queue, fitter1Queue,
            cancellationToken);
        var fitter2 = new Fitter("Сотрудник магазина 2", TimeSpan.FromSeconds(fitterFrequencySeconds), 
            sorter2Queue, fitter2Queue,
            cancellationToken);
        var fitter3 = new Fitter("Сотрудник магазина 3", TimeSpan.FromSeconds(fitterFrequencySeconds), 
            sorter2Queue, fitter3Queue,
            cancellationToken);
        
        var supplierTask = Task.Run(() => supplier.GenerateEvents());
        var sorterOneTask = Task.Run(() => sorter1.HandleEvents());
        var fitterOneTask = Task.Run(() => fitter1.HandleEvents());
        var sorterTwoTask = Task.Run(() => sorter2.HandleEvents());
        var fitterTwoTask = Task.Run(() => fitter2.HandleEvents());
        var fitterThreeTask = Task.Run(() => fitter3.HandleEvents());

        var keyTask = Task.Run(() =>
        {
            Logger.LogProcessStart();
            Console.ReadKey(true);
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        });
        
        await Task.WhenAll(supplierTask, sorterOneTask, fitterOneTask, sorterTwoTask, fitterTwoTask, keyTask);
        
        Logger.LogProcessFinish();
        
        var handlers = new List<BaseEventHandler> { sorter1, sorter2, fitter1, fitter2, fitter3 };
        Logger.LogMetrics(supplier, handlers);
        Logger.Log($"Успешно обработанных товаров {fitter1.EventStartTime.Count + 
                                                   fitter2.EventStartTime.Count + fitter3.EventStartTime.Count}");
        Logger.Log($"Количество бракованных товаров: {sorter1.GetDefectCount() + sorter2.GetDefectCount()}");
    }
}
