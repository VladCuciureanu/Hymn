using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Hymn.Domain.Core.Events;

namespace Hymn.Application.EventSourcedNormalizers
{
    public static class AlbumHistory
    {
        public static IList<AlbumHistoryData> HistoryData { get; set; }

        public static IList<AlbumHistoryData> ToJavaScriptAlbumHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<AlbumHistoryData>();
            AlbumHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<AlbumHistoryData>();
            var last = new AlbumHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new AlbumHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    ArtistId = change.ArtistId == Guid.Empty.ToString() || change.ArtistId == last.ArtistId
                        ? ""
                        : change.ArtistId,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }

            return list;
        }

        private static void AlbumHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<AlbumHistoryData>(e.Data);
                historyData.Timestamp =
                    DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "AlbumAddedEvent":
                        historyData.Action = "Added";
                        historyData.Who = e.User;
                        break;
                    case "AlbumUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "AlbumRemovedEvent":
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