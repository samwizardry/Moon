using Moon;

using OpenTK.Windowing.Common;

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

    protected override void OnLoad()
    {
        base.OnLoad();


    }

    protected override void OnUnload()
    {
        base.OnUnload();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
    }
}
