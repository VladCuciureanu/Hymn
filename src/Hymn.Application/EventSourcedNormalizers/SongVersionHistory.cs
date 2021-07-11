using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Hymn.Domain.Core.Events;

namespace Hymn.Application.EventSourcedNormalizers
{
    public static class SongVersionHistory
    {
        public static IList<SongVersionHistoryData> HistoryData { get; set; }

        public static IList<SongVersionHistoryData> ToJavaScriptSongVersionHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<SongVersionHistoryData>();
            SongVersionHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<SongVersionHistoryData>();
            var last = new SongVersionHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new SongVersionHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    AlbumId = change.AlbumId == Guid.Empty.ToString() || change.AlbumId == last.AlbumId
                        ? ""
                        : change.AlbumId,
                    ArtistId = change.ArtistId == Guid.Empty.ToString() || change.ArtistId == last.ArtistId
                        ? ""
                        : change.ArtistId,
                    SongId = change.SongId == Guid.Empty.ToString() || change.SongId == last.SongId
                        ? ""
                        : change.SongId,
                    Content = string.IsNullOrWhiteSpace(change.Content) || change.Content == last.Content
                        ? ""
                        : change.Content,
                    DefaultKey = string.IsNullOrWhiteSpace(change.DefaultKey) || change.DefaultKey == last.DefaultKey
                        ? ""
                        : change.DefaultKey,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Views = string.IsNullOrWhiteSpace(change.Views) || change.Views == last.Views
                        ? ""
                        : change.Views,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }

            return list;
        }

        private static void SongVersionHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<SongVersionHistoryData>(e.Data);
                historyData.Timestamp =
                    DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "SongVersionAddedEvent":
                        historyData.Action = "Added";
                        historyData.Who = e.User;
                        break;
                    case "SongVersionUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "SongVersionRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.Who = e.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = e.User ?? "Anonymous";
                        break;
                }

                HistoryData.Add(historyData);
            }
        }
    }
}