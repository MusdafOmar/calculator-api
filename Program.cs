var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

var app = builder.Build();

app.MapGet("/", () => "Calculator API is running");

app.MapGet("/add", (int a, int b) => new { operation = "add", a, b, result = a + b });
app.MapGet("/sub", (int a, int b) => new { operation = "sub", a, b, result = a - b });
app.MapGet("/mul", (int a, int b) => new { operation = "mul", a, b, result = a * b });
app.MapGet("/div", (double a, double b) =>
{
    if (b == 0) return Results.BadRequest(new { error = "b cannot be 0" });
    return Results.Ok(new { operation = "div", a, b, result = a / b });
});

app.Run();
