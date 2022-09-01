// See https://aka.ms/new-console-template for more information

var task1 = Task.Run(async () =>
{
    var timer = new PeriodicTimer(TimeSpan.FromSeconds(1)); // 1초 간격
    while (await timer.WaitForNextTickAsync())
    {
        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
    }
});

var task2 = Task.Run(async () =>
{
    var timer = new PeriodicTimer(TimeSpan.FromSeconds(2)); // 2초 간격
    while (await timer.WaitForNextTickAsync())
    {
        Console.WriteLine($"Wake Up!: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")}");

        // 1500 ms 소요되는 처리가 발생했다고 가정
        Thread.Sleep(1500);
    }
});

await task1;
await task2;