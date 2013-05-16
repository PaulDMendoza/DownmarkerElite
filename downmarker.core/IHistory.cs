
namespace DownMarker.Core
{
    public interface IHistory<T> 
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        T Current { get; }

        void Add(T value);
        void Clear();
        T Back();
        T Forward();
    }
}
