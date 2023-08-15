namespace PhotosService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}