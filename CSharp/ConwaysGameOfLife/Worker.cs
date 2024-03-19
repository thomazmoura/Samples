using System.Diagnostics;
using System.Text;

namespace ConwaysGameOfLife;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly int _height;
    private readonly int _width;
    private readonly bool?[,] _tiles;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _height = Console.WindowWidth;
        _width = Console.WindowHeight - 3;
        _tiles = new bool?[_width, _height];

        _tiles[42, 50] = true;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Shuffle();
        while (!stoppingToken.IsCancellationRequested)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Update();
            Render();
            stopwatch.Stop();
            Console.WriteLine($"The current frame took {stopwatch.Elapsed} to complete");
            await Task.Delay(5000, stoppingToken);
        }
    }

    private void Shuffle()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _tiles[x, y] = Random.Shared.NextInt64(0, 20) switch

                {
                    0 => true,
                    1 => false,
                    _ => null
                };
            }
        }
    }

    private void Update()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var currentTile = _tiles[x, y];
                if (currentTile == null)
                {
                    continue;
                }

                var amountOfNeighbours = CalculateLiveNeighbours(x, y);

                if (currentTile == true && (amountOfNeighbours < 2 || amountOfNeighbours > 3))
                {
                    _tiles[x, y] = false;
                }
                else if (currentTile == false && amountOfNeighbours == 3)
                {
                    _tiles[x, y] = true;
                }
                else if (currentTile == false)
                {
                    _tiles[x, y] = null;
                }
            }
        }
    }

    private int CalculateLiveNeighbours(int x, int y)
    {
        var count = 0;
        for (var i = Math.Max(0, x - 1); i <= Math.Min(x + 1, _width - 1); i++)
        {
            for (var j = Math.Max(0, y - 1); j <= Math.Min(y + 1, _height - 1); j++)
            {
                if (i != x || j != y) // Exclude the current cell
                {
                    count += _tiles[i, j] == true ? 1 : 0;
                }
            }
        }
        return count;
    }

    private void Render()
    {
        var stringBuilder = new StringBuilder();
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var tile = _tiles[x, y] switch
                {
                    true => "O",
                    false => "X",
                    _ => " ",
                };
                _ = stringBuilder.Append(tile);
            }
        }
        var currentFrame = stringBuilder.ToString();
        Console.Clear();
        Console.WriteLine(currentFrame);
    }
}
