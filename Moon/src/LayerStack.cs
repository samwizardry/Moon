namespace Moon;

public class LayerStack
{
    private readonly List<Layer> _layers = new List<Layer>();
    private int _layerInsertIndex = 0;

    public IReadOnlyList<Layer> Layers => _layers;

    public void PushLayer(Layer layer)
    {
        _layers.Insert(_layerInsertIndex++, layer);
    }

    public void PushOverlay(Layer overlay)
    {
        _layers.Add(overlay);
    }

    public void PopLayer(Layer layer)
    {
        if (_layers.Remove(layer))
        {
            _layerInsertIndex--;
        }
    }

    public void PopOverlay(Layer overlay)
    {
        _layers.Remove(overlay);
    }
}
