namespace Hymn.Application.EventSourcedNormalizers
{
    public class SongHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Timestamp { get; set; }
        public string Who { get; set; }
    }
}