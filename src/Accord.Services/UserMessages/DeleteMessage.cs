﻿using System.Threading;
using System.Threading.Tasks;
using Accord.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Accord.Services.UserMessages
{
    public sealed record DeleteMessageRequest(ulong DiscordMessageId) : IRequest<ServiceResponse>;

    public class DeleteMessageHandler : IRequestHandler<DeleteMessageRequest, ServiceResponse>
    {
        private readonly AccordContext _db;

        public DeleteMessageHandler(AccordContext db)
        {
            _db = db;
        }

        public async Task<ServiceResponse> Handle(DeleteMessageRequest request, CancellationToken cancellationToken)
        {
            var message = await _db.UserMessages.SingleOrDefaultAsync(x => x.Id == request.DiscordMessageId, cancellationToken: cancellationToken);

            if (message is not null)
            {
                _db.Remove(message);
                await _db.SaveChangesAsync(cancellationToken);
            }

            return ServiceResponse.Ok();
        }
    }
}
