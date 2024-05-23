using Dapr.Client;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost("/checkout", async () =>
{
    for (int i = 1; i <= 10; i++)
    {
        var order = new Order(i);
        using var client = new DaprClientBuilder().Build();

        // Publish an event/message using Dapr PubSub
        await client.PublishEventAsync("orderpubsub", "orders", order);
        Console.WriteLine("Published data: " + order);

        await Task.Delay(TimeSpan.FromSeconds(1));
    }

    return Results.Ok();
});

await app.RunAsync();

public record Order([property: JsonPropertyName("orderId")] int OrderId);
