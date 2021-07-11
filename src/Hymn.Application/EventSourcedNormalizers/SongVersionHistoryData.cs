namespace Hymn.Application.EventSourcedNormalizers
{
    public class SongVersionHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string AlbumId { get; set; }
        public string ArtistId { get; set; }
        public string SongId { get; set; }
        public string Content { get; set; }
        public string DefaultKey { get; set; }
        public string Name { get; set; }
        public string Views { get; set; }
        public string Timestamp { get; set; }
        public string Who { get; set; }
    }
}