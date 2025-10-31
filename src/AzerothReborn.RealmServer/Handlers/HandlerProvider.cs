using System.Reflection;
using AzerothReborn.RealmServer.Network;
using AzerothReborn.RealmServer.Reflection;

namespace AzerothReborn.RealmServer.Handlers;

internal class HandlerProvider
{
    private readonly Dictionary<Opcodes, IHandler> _cachedHandlers;

    public HandlerProvider(IServiceProvider provider)
    {
        _cachedHandlers = [];

        var handlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsDefined(typeof(OpcodeAttribute)))
            .Where(t => t.IsAssignableTo(typeof(IHandler)))
            .ToList();

        foreach (var handler in handlers)
        {
            var attributes = Attribute.GetCustomAttributes(handler, typeof(OpcodeAttribute)) as OpcodeAttribute[];

            if (attributes is null) throw new Exception("Attributes should not be null");

            foreach (var attribute in attributes)
            {
                var genericHandler = provider.GetService(handler) as IHandler;

                if (genericHandler is null) throw new ApplicationException($"Unable to create {handler.Name}, did you forget to register it with dependency injection?");

                _cachedHandlers.Add(attribute.OpCode, genericHandler);
            }
        }
    }

    public IHandler? GetHandlerForOpcode(Opcodes opcode)
    {
        if (!_cachedHandlers.ContainsKey(opcode))
        {
            return null;
        }

        return _cachedHandlers[opcode];
    }
}
