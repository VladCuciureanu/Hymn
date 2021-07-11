using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Hymn.Domain.Core.Events;

namespace Hymn.Application.EventSourcedNormalizers
{
    public static class SongHistory
    {
        public static IList<SongHistoryData> HistoryData { get; set; }

        public static IList<SongHistoryData> ToJavaScriptSongHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<SongHistoryData>();
            SongHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<SongHistoryData>();
            var last = new SongHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new SongHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
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

        private static void SongHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<SongHistoryData>(e.Data);
                historyData.Timestamp =
                    DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "SongAddedEvent":
                        historyData.Action = "Added";
                        historyData.Who = e.User;
                        break;
                    case "SongUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "SongRemovedEvent":
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