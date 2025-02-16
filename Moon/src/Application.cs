using OpenTK.Windowing.Desktop;

namespace Moon;

public abstract class Application : IDisposable
{
    private bool _disposed;

    public Application()
    {
    }

    ~Application()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            // Освобождаем управляемые ресурсы
        }

        // Освобождаем неуправляемые ресурсы

        _disposed = true;
    }

    protected void CheckDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(Application));
        }
    }

    public virtual void Run()
    {
        CheckDisposed();


    }
}
