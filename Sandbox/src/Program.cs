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
            Vsync = VSyncMode.Off,
            WindowState = WindowState.Maximized
        });

        //game.PushLayer(new ExampleLayer());
        game.PushOverlay(new ImGuiLayer());

        game.Run();
    }

    public class ExampleLayer : Layer
    {
        public ExampleLayer()
            : base("ExampleLayer")
        { }

        public override void OnUpdate(FrameEventArgs args)
        {
        }

        public override bool OnKeyDown(KeyboardKeyEventArgs e)
        {
            Log.Information(e.Key.ToString());

            return false;
        }
    }
}
