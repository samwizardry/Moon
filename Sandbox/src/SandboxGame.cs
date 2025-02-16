using Moon;

namespace Sandbox;


public class SandboxGame : Game
{
    public SandboxGame()
        : base(new OpenTK.Windowing.Desktop.NativeWindowSettings
        {
            Title = "Sandbox",
            ClientSize = (1280, 720)
        })
    {
    }
}
