namespace BoothApp.Presentation.LoopScroll
{
    public interface ILoopScrollItem<T>
    {
        void UpdateItem(T data, int index);
    }
}