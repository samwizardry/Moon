using OpenTK.Windowing.Common;

namespace Moon;

public abstract class Layer
{
    public virtual void OnAttach() { }

    public virtual void OnDetach() { }

    public virtual void OnUpdate() { }

    public abstract bool OnKeyDown(KeyboardKeyEventArgs e);
}
