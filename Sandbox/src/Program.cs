using Moon;

using OpenTK.Windowing.Common;

namespace Sandbox;

class Program
{
    static void Main(string[] args)
    {
        using var game = new Game(new OpenTK.Windowing.Desktop.NativeWindowSettings
        {
            Title = "Sandbox",
            ClientSize = (800, 600),
            Vsync = VSyncMode.On
        });

        game.PushLayer(new ExampleLayer());

        game.Run();
    }

    public class ExampleLayer : Layer
    {
        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override bool OnKeyDown(KeyboardKeyEventArgs e)
        {
            Log.Information(e.Key.ToString());

            return false;
        }
    }
}
