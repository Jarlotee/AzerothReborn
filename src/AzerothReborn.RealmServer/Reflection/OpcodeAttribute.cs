using AzerothReborn.RealmServer.Network;

namespace AzerothReborn.RealmServer.Reflection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    internal class OpcodeAttribute : Attribute
    {
        private readonly Opcodes _opcode;

        public virtual Opcodes OpCode
        {
            get { return _opcode; }
        }

        public OpcodeAttribute(Opcodes opcode)
        {
            _opcode = opcode;
        }
    }
}