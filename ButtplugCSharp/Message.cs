using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButtplugManaged
{


    public class Message
    {

        public static Message From(MessageBase messageBase)
        {
            // Only handle Client messages
            Message message = new Message();

            if (messageBase is Ping PingCmd)
                message.Ping = PingCmd;


            if (messageBase is RequestServerInfo RequestServerInfoCmd)
                message.RequestServerInfo = RequestServerInfoCmd;


            if (messageBase is StartScanning StartScanningCmd)
                message.StartScanning = StartScanningCmd;
            if (messageBase is StopScanning StopScanningCmd)
                message.StopScanning = StopScanningCmd;
            if (messageBase is RequestDeviceList RequestDeviceListCmd)
                message.RequestDeviceList = RequestDeviceListCmd;

            if (messageBase is RawWriteCmd RawWriteCmd)
                message.RawWriteCmd = RawWriteCmd;
            if (messageBase is RawReadCmd RawReadCmd)
                message.RawReadCmd = RawReadCmd;
            if (messageBase is RawSubscribeCmd RawSubscribeCmd)
                message.RawSubscribeCmd = RawSubscribeCmd;
            if (messageBase is RawUnsubscribeCmd RawUnsubscribeCmd)
                message.RawUnsubscribeCmd = RawUnsubscribeCmd;

            if (messageBase is StopDeviceCmd StopDeviceCmd)
                message.StopDeviceCmd = StopDeviceCmd;
            if (messageBase is StopAllDevices StopAllDevicesCmd)
                message.StopAllDevices = StopAllDevicesCmd;
            if (messageBase is VibrateCmd VibrateCmd)
                message.VibrateCmd = VibrateCmd;
            if (messageBase is LinearCmd LinearCmd)
                message.LinearCmd = LinearCmd;
            if (messageBase is RotateCmd RotateCmd)
                message.RotateCmd = RotateCmd;

            if (messageBase is BatteryLevelCmd BatteryLevelCmd)
                message.BatteryLevelCmd = BatteryLevelCmd;
            if (messageBase is RSSILevelCmd RSSILevelCmd)
                message.RSSILevelCmd = RSSILevelCmd;

            return message;

        }
        //Status
        public Ok Ok { get; set; }
        public Error Error { get; set; }
        public Ping Ping { get; set; }

        //Handshake
        public RequestServerInfo RequestServerInfo { get; set; }
        public ServerInfo ServerInfo { get; set; }

        //Enumeration
        public StartScanning StartScanning { get; set; }
        public StopScanning StopScanning { get; set; }
        public ScanningFinished ScanningFinished { get; set; }
        public RequestDeviceList RequestDeviceList { get; set; }
        public DeviceList DeviceList { get; set; }
        public DeviceAdded DeviceAdded { get; set; }
        public DeviceRemoved DeviceRemoved { get; set; }

        //Raw Messages
        public RawWriteCmd RawWriteCmd { get; set; }
        public RawReadCmd RawReadCmd { get; set; }
        public RawReading RawReading { get; set; }
        public RawSubscribeCmd RawSubscribeCmd { get; set; }
        public RawUnsubscribeCmd RawUnsubscribeCmd { get; set; }
        //not implemented

        // Generic Device Messages
        public StopDeviceCmd StopDeviceCmd { get; set; }
        public StopAllDevices StopAllDevices { get; set; }
        public VibrateCmd VibrateCmd { get; set; }
        public LinearCmd LinearCmd { get; set; }
        public RotateCmd RotateCmd { get; set; }

        //Generinc Sensor Messsages
        public BatteryLevelCmd BatteryLevelCmd { get; set; }
        public BatteryLevelReading BatteryLevelReading { get; set; }
        public RSSILevelCmd RSSILevelCmd { get; set; }
        public RSSILevelReading RSSILevelReading { get; set; }

    }

    public class MessageBase
    {
        public uint Id { get; set; }
    }

    #region Status Messages

    public class Ok : MessageBase
    {
    }

    public class Error : MessageBase
    {
        public string ErrorMessage { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }


        public enum ErrorCodeEnum
        {
            ERROR_UNKNOWN, ERROR_INIT, ERROR_PING, ERROR_MSG, ERROR_DEVICE
        }
    }

    public class Ping : MessageBase
    {
    }

    #endregion

    #region Handshake Messages
    public class ServerInfo : MessageBase
    {
        public string ServerName { get; set; }
        public uint MessageVersion { get; set; }
        public uint MaxPingTime { get; set; }
    }

    public class RequestServerInfo : MessageBase
    {
        public string ClientName { get; set; }
        public uint MessageVersion { get; set; }
    }
    #endregion

    #region Enumeration Messages

    public class StartScanning : MessageBase
    {
    }

    public class StopScanning : MessageBase
    {
    }
    public class ScanningFinished : MessageBase
    {
    }

    public class RequestDeviceList : MessageBase
    {
    }

    public class DeviceList : MessageBase
    {
        public List<DeviceAdded> Devices { get; set; }
    }

    public class DeviceAdded : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string DeviceName { get; set; }
        public Dictionary<string, DeviceMessagesDetails> DeviceMessages { get; set; }

        
    }

    public class DeviceMessages
    {
        public const string VibrateCmd = "VibrateCmd";
        public const string BatteryLevelCmd = "BatteryLevelCmd";
        public const string StopDeviceCmd = "StopDeviceCmd";
        public const string LinearCmd = "LinearCmd";
        public const string RotateCmd = "RotateCmd";
    }

    public class DeviceMessagesDetails
    {
        public uint FeatureCount { get; set; }
        public List<uint> StepCount { get; set; }
        public List<string> Endpoints { get; set; }
        public List<uint> MaxDuration { get; set; }
    }

    public class DeviceRemoved : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    #endregion

    #region Raw Messages
    public class RawWriteCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string Endpoint { get; set; }
        public List<int> Data { get; set; }
        public bool WriteWithResponse { get; set; }
    }
    public class RawReadCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string Endpoint { get; set; }
        public uint ExpectedLength { get; set; }
        public bool WaitForData { get; set; }
    }

    public class RawReading : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string Endpoint { get; set; }
        public List<int> Data { get; set; }
    }

    public class RawSubscribeCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string Endpoint { get; set; }
    }

    public class RawUnsubscribeCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string Endpoint { get; set; }
    }

    #endregion

    #region Generic Device Messages

    public class StopDeviceCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class StopAllDevices : MessageBase
    {
    }
    public class VibrateCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }

        public List<VibrateSpeed> Speeds { get; set; }
    }

    public class VibrateSpeed
    {
        public uint Index { get; set; }
        public double Speed { get; set; }
    }

    public class LinearCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }

        public List<LinearVector> Vectors { get; set; }
    }

    public class LinearVector
    {
        public uint Index { get; set; }
        public uint Duration { get; set; }
        public double Position { get; set; }
    }

    public class RotateCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }

        public List<Rotations> Rotations { get; set; }
    }

    public class Rotations
    {
        public uint Index { get; set; }
        public double Speed { get; set; }
        public bool Clockwise { get; set; }
    }
    #endregion

    #region Generic Sensor Messages

    public class BatteryLevelCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class BatteryLevelReading : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public double BatteryLevel { get; set; }
    }

    public class RSSILevelCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class RSSILevelReading : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public int RSSILevel { get; set; }
    }

    #endregion





}
