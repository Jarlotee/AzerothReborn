//
// Copyright (C) 2013-2025 getMaNGOS <https://www.getmangos.eu>
//
// This program is free software. You can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation. either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY. Without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//

using RealmServer.Handlers;
using RealmServer.Requests;

namespace RealmServer.Network;

internal sealed class HandlerDispatcher<THandler, TRequest> : IHandlerDispatcher
    where THandler : IHandler<TRequest>
    where TRequest : IRequestMessage<TRequest>
{
    private readonly THandler handler;

    public HandlerDispatcher(THandler handler)
    {
        this.handler = handler;
    }

    public MessageOpcode Opcode => handler.MessageOpcode;

    public async Task ExectueAsync(SocketReader reader, SocketWriter writer)
    {
        var request = await TRequest.ReadAsync(reader);
        var response = await handler.ExectueAsync(request);
        await response.WriteAsync(writer);
    }
}
