namespace Sandbox;

class Program
{
    static void Main(string[] args)
    {
        using var app = new SandboxApplication();
        app.Run();
    }
}
