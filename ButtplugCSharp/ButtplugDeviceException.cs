using System;

namespace ButtplugManaged
{
    public class ButtplugDeviceException : ButtplugException
    {
        /// <inheritdoc />
        public ButtplugDeviceException()
        {
        }

        /// <inheritdoc />
        public ButtplugDeviceException(string aMessage) : base(aMessage)
        {
        }

        /// <inheritdoc />
        public ButtplugDeviceException(string aMessage, Exception aInner) : base(aMessage, aInner)
        {
        }
    }
}