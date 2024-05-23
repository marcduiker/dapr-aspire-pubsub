var builder = DistributedApplication.CreateBuilder(args);

var pubsub = builder.AddDaprPubSub("orderpubsub");

builder.AddProject<Projects.Checkout>("checkout")
    .WithDaprSidecar()
    .WithReference(pubsub);

builder.AddProject<Projects.OrderProcessor>("order-processor")
    .WithDaprSidecar()
    .WithReference(pubsub);

builder.Build().Run();
