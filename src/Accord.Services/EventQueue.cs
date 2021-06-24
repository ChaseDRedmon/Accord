﻿using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Accord.Services
{
    public interface IEventQueue
    {
        ValueTask Queue(IEvent queuedEvent);
        ValueTask<IEvent> Dequeue(CancellationToken cancellationToken);
    }

    public class EventQueue : IEventQueue
    {
        private const int QUEUE_CAPACITY = 1000;
        private readonly Channel<IEvent> _queue;

        public EventQueue()
        {
            var options = new BoundedChannelOptions(QUEUE_CAPACITY)
            {
                FullMode = BoundedChannelFullMode.DropOldest
            };

            _queue = Channel.CreateBounded<IEvent>(options);
        }

        public async ValueTask Queue(IEvent queuedEvent)
        {
            await _queue.Writer.WriteAsync(queuedEvent);
        }

        public async ValueTask<IEvent> Dequeue(
            CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }

    public interface IEvent
    {
        DateTimeOffset QueuedDateTime { get; }
    }

    public interface IUserEvent : IEvent
    {
        ulong DiscordUserId { get; }
    }

    public sealed record UserJoinedEvent(ulong DiscordGuildId, ulong DiscordUserId, DateTimeOffset QueuedDateTime, string DiscordUsername, string DiscordDiscriminator, string? DiscordNickname) : IUserEvent;
    public sealed record AddMessageEvent(ulong DiscordMessageId, ulong DiscordUserId, ulong DiscordChannelId, DateTimeOffset QueuedDateTime) : IUserEvent;
    public sealed record DeleteMessageEvent(ulong DiscordMessageId, DateTimeOffset QueuedDateTime) : IEvent;
    public sealed record CalculateXpForUserEvent(ulong DiscordUserId, ulong DiscordChannelId, DateTimeOffset QueuedDateTime) : IUserEvent;
    public sealed record VoiceConnectedEvent(ulong DiscordGuildId, ulong DiscordUserId, ulong DiscordChannelId, string DiscordSessionId, DateTimeOffset QueuedDateTime) : IUserEvent;
    public sealed record VoiceDisconnectedEvent(ulong DiscordGuildId, ulong DiscordUserId, string DiscordSessionId, DateTimeOffset QueuedDateTime) : IUserEvent;
    public sealed record RaidCalculationEvent(ulong DiscordGuildId, ulong DiscordUserId, string DiscordUsername, string DiscordUserDiscriminator, DateTimeOffset QueuedDateTime) : IUserEvent;
}
