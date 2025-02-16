using ImGuiNET;

using Moon;

using OpenTK.Windowing.Common;

namespace Sandbox;

class Program
{
    static void Main(string[] args)
    {
        using var game = new Application(new OpenTK.Windowing.Desktop.NativeWindowSettings
        {
            Title = "Sandbox",
            ClientSize = (1280, 720),
            Vsync = VSyncMode.Off
        });

        game.PushLayer(new ExampleLayer());

        game.Run();
    }

    public class ExampleLayer : Layer
    {
        public ExampleLayer()
            : base("ExampleLayer")
        { }

        public override void OnUpdate()
        {
        }

        public override void OnImGuiRender()
        {
            ImGui.DockSpaceOverViewport();
            ImGui.ShowDemoWindow();
        }

        public override bool OnKeyDown(KeyboardKeyEventArgs e)
        {
            Log.Information("{name}: {key}", Name, e.Key.ToString());

            return false;
        }
    }
}
