using System;

namespace ButtplugManaged
{
    public class ButtplugExceptionEventArgs : EventArgs
    {
        public ButtplugException Exception { get; }

        public ButtplugExceptionEventArgs(ButtplugException ex)
        {
            Exception = ex;
        }
    }
}
