using OpenTK.Windowing.Desktop;

namespace Moon;

public class Application : GameWindow
{
    public Application(int width, int height, string title = "Moon")
        : base(GameWindowSettings.Default,
            new NativeWindowSettings
            {
                Title = title,
                ClientSize = (width, height)
            })
    { }
}
