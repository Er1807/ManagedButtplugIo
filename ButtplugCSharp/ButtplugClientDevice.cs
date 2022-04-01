using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButtplugManaged
{
    public class ButtplugClientDevice
    {
        /// <summary>
        /// The device index, which uniquely identifies the device on the server.
        /// </summary>
        /// <remarks>
        /// If a device is removed, this may be the only populated field. If the same device
        /// reconnects, the index should be reused.
        /// </remarks>
        public uint Index { get; }

        /// <summary>
        /// The device name, which usually contains the device brand and model.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Buttplug Protocol messages supported by this device, with additional attributes.
        /// </summary>
        public Dictionary<string, DeviceMessagesDetails> AllowedMessages { get; }

        private readonly ButtplugMessageManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtplugClientDevice"/> class, using
        /// discrete parameters.
        /// </summary>
        /// <param name="aIndex">The device index.</param>
        /// <param name="aName">The device name.</param>
        /// <param name="aAllowedMessages">The device allowed message list, with corresponding attributes.</param>
        internal ButtplugClientDevice(ButtplugMessageManager manager,
            uint aIndex,
            string aName,
            Dictionary<string, DeviceMessagesDetails> aAllowedMessages)
        {
            _manager = manager;
            Index = aIndex;
            Name = aName;
            AllowedMessages = aAllowedMessages;
        }


        public bool Equals(ButtplugClientDevice aDevice)
        {
            return Index == aDevice.Index;
        }

        public Task SendVibrateCmd(double aSpeed)
        {
            // If the message is missing from our dict, we should still send anyways just to let the rust library throw.
            var count = 1u;
            if (AllowedMessages.ContainsKey(DeviceMessages.VibrateCmd))
            {
                count = AllowedMessages[DeviceMessages.VibrateCmd].FeatureCount;
            }

            // There is probably a cleaner, LINQyer way to do this but ugh don't care.
            var commandDict = new Dictionary<uint, double>();
            for (var i = 0u; i < count; ++i)
            {
                commandDict.Add(i, aSpeed);
            }

            return SendVibrateCmd(commandDict);
        }

        public Task SendVibrateCmd(IEnumerable<double> aCmds)
        {
            return SendVibrateCmd(aCmds.Select((cmd, index) => (cmd, index)).ToDictionary(x => (uint)x.index, x => x.cmd));
        }

        public Task SendVibrateCmd(Dictionary<uint, double> aCmds)
        {
            VibrateCmd vibrateMessage = new VibrateCmd();
            vibrateMessage.DeviceIndex = Index;
            vibrateMessage.Speeds = new List<VibrateSpeed>();
            foreach (var command in aCmds)
            {
                vibrateMessage.Speeds.Add(new VibrateSpeed() { Index = command.Key, Speed = command.Value });
            }
            return _manager.SendClientMessage(vibrateMessage);
        }

        public Task SendRotateCmd(double aSpeed, bool aClockwise)
        {
            // If the message is missing from our dict, we should still send anyways just to let the rust library throw.
            var count = 1u;
            if (AllowedMessages.ContainsKey(DeviceMessages.RotateCmd))
            {
                count = AllowedMessages[DeviceMessages.RotateCmd].FeatureCount;
            }

            // There is probably a cleaner, LINQyer way to do this but ugh don't care.
            var commandDict = new Dictionary<uint, (double, bool)>();
            for (var i = 0u; i < count; ++i)
            {
                commandDict.Add(i, (aSpeed, aClockwise));
            }

            return SendRotateCmd(commandDict);
        }

        public Task SendRotateCmd(IEnumerable<(double, bool)> aCmds)
        {
            return SendRotateCmd(aCmds.Select((cmd, index) => (cmd, index)).ToDictionary(x => (uint)x.index, x => x.cmd));
        }

        public Task SendRotateCmd(Dictionary<uint, (double, bool)> aCmds)
        {
            RotateCmd rotateMessage = new RotateCmd();
            rotateMessage.DeviceIndex = Index;
            rotateMessage.Rotations = new List<Rotations>();
            foreach (var command in aCmds)
            {
                rotateMessage.Rotations.Add(new Rotations() { Index = command.Key, Speed = command.Value.Item1, Clockwise = command.Value.Item2 });
            }
            return _manager.SendClientMessage(rotateMessage);
        }

        public Task SendLinearCmd(uint aDuration, double aPosition)
        {
            // If the message is missing from our dict, we should still send anyways just to let the rust library throw.
            var count = 1u;
            if (AllowedMessages.ContainsKey(DeviceMessages.LinearCmd))
            {
                count = AllowedMessages[DeviceMessages.LinearCmd].FeatureCount;
            }

            // There is probably a cleaner, LINQyer way to do this but ugh don't care.
            var commandDict = new Dictionary<uint, (uint, double)>();
            for (var i = 0u; i < count; ++i)
            {
                commandDict.Add(i, (aDuration, aPosition));
            }

            return SendLinearCmd(commandDict);
        }

        public Task SendLinearCmd(IEnumerable<(uint, double)> aCmds)
        {
            return SendLinearCmd(aCmds.Select((cmd, index) => (cmd, index)).ToDictionary(x => (uint)x.index, x => x.cmd));
        }

        public Task SendLinearCmd(Dictionary<uint, (uint, double)> aCmds)
        {
            LinearCmd linearMessage = new LinearCmd();
            linearMessage.DeviceIndex = Index;
            linearMessage.Vectors = new List<LinearVector>();
            foreach (var command in aCmds)
            {
                linearMessage.Vectors.Add(new LinearVector() { Index = command.Key, Duration = command.Value.Item1, Position = command.Value.Item2 });
            }
            return _manager.SendClientMessage(linearMessage);
        }

        public async Task<double> SendBatteryLevelCmd()
        {
            var result = await _manager.SendClientMessage(new BatteryLevelCmd() { DeviceIndex = Index });
            if (result is BatteryLevelReading reading)
                return reading.BatteryLevel;

            throw new ButtplugDeviceException($"Expected message type of BatteryLevelReading not received, got {result} instead.");
        }

        public async Task<int> SendRSSIBatteryLevelCmd()
        {
            var result = await _manager.SendClientMessage(new RSSILevelCmd() { DeviceIndex = Index });
            if (result is RSSILevelReading reading)
                return reading.RSSILevel;

            throw new ButtplugDeviceException($"Expected message type of RssiLevelReading not received, got {result} instead.");
        }

        public async Task<byte[]> SendRawReadCmd(string aEndpoint, uint aExpectedLength, uint aTimeout)
        {
            var task = _manager.SendClientMessage(new RawReadCmd() { DeviceIndex = Index, Endpoint = aEndpoint, ExpectedLength = aExpectedLength });
            if (await Task.WhenAny(task, Task.Delay((int)aTimeout)) == task)
            {
                var result = await task;

                if (result is RawReading reading)
                {
                    return reading.Data.Select(x => (byte)x).ToArray();
                }

                throw new ButtplugDeviceException($"Expected message type of RawReading not received, got {result} instead.");
            }
            else
            {
                throw new ButtplugDeviceException($"No Message returned");
            }

        }

        public Task SendRawWriteCmd(string aEndpoint, byte[] aData, bool aWriteWithResponse)
        {
            return _manager.SendClientMessage(new RawWriteCmd() { DeviceIndex = Index, Endpoint = aEndpoint, Data = aData.Select(x => (int)x).ToList(), WriteWithResponse = aWriteWithResponse });
        }

        public Task SendRawSubscribeCmd(string aEndpoint)
        {
            return _manager.SendClientMessage(new RawSubscribeCmd() { DeviceIndex = Index, Endpoint = aEndpoint });
        }

        public Task SendRawUnsubscribeCmd(string aEndpoint)
        {
            return _manager.SendClientMessage(new RawUnsubscribeCmd() { DeviceIndex = Index, Endpoint = aEndpoint });
        }

        public Task SendStopDeviceCmd()
        {
            return _manager.SendClientMessage(new StopDeviceCmd() { DeviceIndex = Index });
        }
    }
}
