﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using Wino.Core.Domain.Enums;
using Wino.Core.Domain.Interfaces;
using Wino.Core.Domain.Models.Accounts;
using Wino.Core.Domain.Models.MailItem;

namespace Wino.Core.Domain.Models.Synchronization
{
    public class SynchronizationResult
    {
        public SynchronizationResult() { }

        /// <summary>
        /// Gets the new downloaded messages from synchronization.
        /// Server will create notifications for these messages.
        /// It's ignored in serialization. Client should not react to this.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<IMailItem> DownloadedMessages { get; set; } = [];

        /// <summary>
        /// Gets the new downloaded events from synchronization.
        /// Server will create notifications for these events.
        /// It's ignored in serialization. Client should not react to this.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<ICalendarItem> DownloadedEvents { get; set; } = [];

        public ProfileInformation ProfileInformation { get; set; }

        public SynchronizationCompletedState CompletedState { get; set; }

        public static SynchronizationResult Empty => new() { CompletedState = SynchronizationCompletedState.Success };

        // Mail synchronization
        public static SynchronizationResult Completed(IEnumerable<IMailItem> downloadedMessages)
            => new()
            {
                DownloadedMessages = downloadedMessages,
                CompletedState = SynchronizationCompletedState.Success
            };

        // Profile synchronization
        public static SynchronizationResult Completed(ProfileInformation profileInformation)
            => new()
            {
                ProfileInformation = profileInformation,
                CompletedState = SynchronizationCompletedState.Success
            };

        public static SynchronizationResult Canceled => new() { CompletedState = SynchronizationCompletedState.Canceled };
        public static SynchronizationResult Failed => new() { CompletedState = SynchronizationCompletedState.Failed };
    }
}
