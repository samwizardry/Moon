using OpenTK.Windowing.Common;

namespace Moon;

public abstract class Layer
{
    protected Application? _game;

    public string Name { get; init; }

    protected Layer(string name)
    {
        Name = name;
    }

    public virtual void OnAttach(Application game)
    {
        _game = game;
    }

    public virtual void OnDetach()
    {
        _game = null;
    }

    public virtual void OnUpdate() { }

    public virtual void OnRender() { }

    public virtual void OnImGuiRender() { }

    #region Events
    public virtual void OnResize(ResizeEventArgs e) { }

    #endregion

    #region Input Events

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
