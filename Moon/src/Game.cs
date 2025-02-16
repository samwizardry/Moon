using OpenTK.Compute.OpenCL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Moon;

public class Game : GameWindow
{
    private readonly LayerStack _layerStack = new LayerStack();

    public Game(NativeWindowSettings settings)
        : base(GameWindowSettings.Default, settings)
    {
        Log.CoreInformation("Initializing");
    }

    public void PushLayer(Layer layer)
    {
        _layerStack.PushLayer(layer);
    }

    public void PushOverlay(Layer overlay)
    {
        _layerStack.PushOverlay(overlay);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            _layerStack.Layers[i].OnUpdate();
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnKeyDown(e))
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
        }
    }
}
