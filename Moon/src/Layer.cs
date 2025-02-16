using OpenTK.Windowing.Common;

namespace Moon;

public abstract class Layer
{
    protected Game? _game;

    public string DebugName { get; init; }

    protected Layer(string name)
    {
        DebugName = name;
    }

    public virtual void OnAttach(Game game)
    {
        _game = game;
    }

    public virtual void OnDetach()
    {
        _game = null;
    }

    public virtual void OnUpdate(FrameEventArgs args) { }

    public virtual void OnRender(FrameEventArgs args) { }

    public virtual void OnResize(ResizeEventArgs e) { }

    #region Input events

    public virtual bool OnMouseDown(MouseButtonEventArgs e) => false;

    public virtual bool OnMouseUp(MouseButtonEventArgs e) => false;

    public virtual bool OnMouseEnter() => false;

    public virtual bool OnMouseLeave() => false;

    public virtual bool OnMouseMove(MouseMoveEventArgs e) => false;

    public virtual bool OnMouseWheel(MouseWheelEventArgs e) => false;

    public virtual bool OnKeyDown(KeyboardKeyEventArgs e) => false;

    public virtual bool OnKeyUp(KeyboardKeyEventArgs e) => false;

    public virtual bool OnTextInput(TextInputEventArgs e) => false;

    #endregion
}
