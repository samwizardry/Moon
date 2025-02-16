using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Moon;

public class Application : GameWindow
{
    private readonly LayerStack _layerStack = new LayerStack();
    private ImGuiLayer _imGuiLayer = null!;

    public Application(NativeWindowSettings settings)
        : base(GameWindowSettings.Default, settings)
    {
        Log.CoreInformation("Initializing app {application_name}.", settings.Title);
    }

    public void PushLayer(Layer layer)
    {
        _layerStack.PushLayer(layer);
        layer.OnAttach(this);
    }

    public void PushOverlay(Layer overlay)
    {
        _layerStack.PushOverlay(overlay);
        overlay.OnAttach(this);
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        _imGuiLayer = new ImGuiLayer();
        PushOverlay(_imGuiLayer);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
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

        GL.ClearColor(0.25f, 0.25f, 0.25f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            _layerStack.Layers[i].OnRender();
        }

        _imGuiLayer.Begin();
        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            _layerStack.Layers[i].OnImGuiRender();
        }
        _imGuiLayer.End();

        SwapBuffers();
    }

    #region Events

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            _layerStack.Layers[i].OnResize(e);
        }
    }

    #endregion

    #region Input Events

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnMouseDown(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnMouseUp(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnMouseEnter()
    {
        base.OnMouseEnter();

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnMouseEnter())
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnMouseLeave()
    {
        base.OnMouseLeave();

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnMouseLeave())
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnMouseMove(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnMouseWheel(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e)
    {
        base.OnKeyDown(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnKeyDown(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnKeyUp(KeyboardKeyEventArgs e)
    {
        base.OnKeyUp(e);

        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnKeyUp(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    protected override void OnTextInput(TextInputEventArgs e)
    {
        base.OnTextInput(e);


        for (int i = 0; i < _layerStack.Layers.Count; i++)
        {
            if (_layerStack.Layers[i].OnTextInput(e))
            {
                // Cлой обработал событие и мы не хотим распространять это событие дальше
                break;
            }
        }
    }

    #endregion
}
