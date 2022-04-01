using System;

namespace ButtplugManaged
{
    public class ButtplugPingException : ButtplugException
    {
        /// <inheritdoc />
        public ButtplugPingException()
        {
        }

        /// <inheritdoc />
        public ButtplugPingException(string aMessage) : base(aMessage)
        {
        }

        /// <inheritdoc />
        public ButtplugPingException(string aMessage, Exception aInner) : base(aMessage, aInner)
        {
        }
    }
}