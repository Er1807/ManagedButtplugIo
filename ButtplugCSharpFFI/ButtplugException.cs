using System;

namespace Buttplug
{
    public class ButtplugException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Creates a ButtplugException.
        /// </summary>
        public ButtplugException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a ButtplugException.
        /// </summary>
        /// <param name="aMessage">Exception message.</param>
        public ButtplugException(string aMessage) : base(aMessage)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a ButtplugException.
        /// </summary>
        /// <param name="aMessage">Exception message.</param>
        /// <param name="aClass">Exception class, based on Buttplug Error Message Classes. (https://buttplug-spec.docs.buttplug.io/status.html#error).</param>
        /// <param name="aInner">Inner exception.</param>
        public ButtplugException(string aMessage, Exception aInner) : base(aMessage, aInner)
        {
        }

        public static ButtplugException FromError(Error aMsg)
        {
            var err_str = aMsg.ErrorMessage;
            switch (aMsg.ErrorCode)
            {
                case Error.ErrorCodeEnum.ERROR_INIT:
                    return new ButtplugConnectorException(err_str);
                case Error.ErrorCodeEnum.ERROR_PING:
                    return new ButtplugPingException(err_str);
                case Error.ErrorCodeEnum.ERROR_MSG:
                    return new ButtplugMessageException(err_str);
                case Error.ErrorCodeEnum.ERROR_UNKNOWN:
                    return new ButtplugUnknownException(err_str);
                case Error.ErrorCodeEnum.ERROR_DEVICE:
                    return new ButtplugDeviceException(err_str);
            }

            return new ButtplugUnknownException($"Unknown error type: {aMsg.ErrorCode} | Message: {aMsg.ErrorMessage}");
        }
    }
}
