
string path = @"D:\EventExampleFolder";

PathControl pathControl = new();
pathControl.PathControlEvent += (float sizeMb) =>
{

    Console.WriteLine($"Dosyanınz 50 Mb ' aşmışmıştır, Mevcut Dosya Boyutunuz:{sizeMb.ToString("0.00")}");

};
using CancellationTokenSource cts = new();
await pathControl.Control(path, cts.Token);

class PathControl()
{
    public delegate void PathHandler(float sizeMb);

    public event PathHandler PathControlEvent;


    public async Task Control(string path, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            DirectoryInfo directory = new(path);
            var file = directory.GetFiles();

            float size = await Task.Run(() => directory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(x => x.Length));

            float sizeInMB = size / 1024 / 1024;
            if (sizeInMB > 50)
            {

                await Task.Delay(1000, cancellationToken);

                PathControlEvent?.Invoke(sizeInMB);
            }
        }
    }
}

