var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.tutoriaBE_Web>("web");

builder.Build().Run();
