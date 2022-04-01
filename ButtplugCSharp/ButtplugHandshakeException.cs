using System;

namespace ButtplugManaged
{
    public class ButtplugHandshakeException : ButtplugException
    {
        /// <inheritdoc />
        public ButtplugHandshakeException()
        {
        }

        /// <inheritdoc />
        public ButtplugHandshakeException(string aMessage) : base(aMessage)
        {
        }

        /// <inheritdoc />
        public ButtplugHandshakeException(string aMessage, Exception aInner) : base(aMessage, aInner)
        {
        }
    }
}