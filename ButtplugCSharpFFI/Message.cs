using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buttplug
{


    public class Message
    {

        public static Message From(MessageBase messageBase)
        {
            // Only handle Client messages
            Message message = new Message();

            if (messageBase is PingCmd PingCmd)
                message.Ping = PingCmd;


            if (messageBase is RequestServerInfoCmd RequestServerInfoCmd)
                message.RequestServerInfo = RequestServerInfoCmd;


            if (messageBase is StartScanningCmd StartScanningCmd)
                message.StartScanning = StartScanningCmd;
            if (messageBase is StopScanningCmd StopScanningCmd)
                message.StopScanning = StopScanningCmd;
            if (messageBase is RequestDeviceListCmd RequestDeviceListCmd)
                message.RequestDeviceList = RequestDeviceListCmd;

            if (messageBase is StopDeviceCmd StopDeviceCmd)
                message.StopDeviceCmd = StopDeviceCmd;
            if (messageBase is StopAllDevicesCmd StopAllDevicesCmd)
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
        public OkCmd Ok { get; set; }
        public ErrorCmd Error { get; set; }
        public PingCmd Ping { get; set; }

        //Handshake
        public RequestServerInfoCmd RequestServerInfo { get; set; }
        public ServerInfoCmd ServerInfo { get; set; }

        //Enumeration
        public StartScanningCmd StartScanning { get; set; }
        public StopScanningCmd StopScanning { get; set; }
        public ScanningFinishedCmd ScanningFinished { get; set; }
        public RequestDeviceListCmd RequestDeviceList { get; set; }
        public DeviceListCmd DeviceList { get; set; }
        public DeviceAddedCmd DeviceAdded { get; set; }
        public DeviceRemovedCmd DeviceRemoved { get; set; }

        //Raw Messages
        //not implemented

        // Generic Device Messages
        public StopDeviceCmd StopDeviceCmd { get; set; }
        public StopAllDevicesCmd StopAllDevices { get; set; }
        public VibrateCmd VibrateCmd { get; set; }
        public LinearCmd LinearCmd { get; set; }
        public RotateCmd RotateCmd { get; set; }

        //Generinc Sensor Messsages
        public BatteryLevelCmd BatteryLevelCmd { get; set; }
        public BatteryLevelReadingCmd BatteryLevelReading { get; set; }
        public RSSILevelCmd RSSILevelCmd { get; set; }
        public RSSILevelReadingCmd RSSILevelReading { get; set; }

    }

    public class MessageBase
    {
        public uint Id { get; set; }
    }

    #region Status Messages

    public class OkCmd : MessageBase
    {
    }

    public class ErrorCmd : MessageBase
    {
        public uint Id { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }


        public enum ErrorCodeEnum
        {
            ERROR_UNKNOWN, ERROR_INIT, ERROR_PING, ERROR_MSG, ERROR_DEVICE
        }
    }

    public class PingCmd : MessageBase
    {
    }

    #endregion

    #region Handshake Messages
    public class ServerInfoCmd : MessageBase
    {
        public string ServerName { get; set; }
        public uint MessageVersion { get; set; }
        public uint MaxPingTime { get; set; }
    }

    public class RequestServerInfoCmd : MessageBase
    {
        public string ClientName { get; set; }
        public uint MessageVersion { get; set; }
    }
    #endregion

    #region Enumeration Messages

    public class StartScanningCmd : MessageBase
    {
    }

    public class StopScanningCmd : MessageBase
    {
    }
    public class ScanningFinishedCmd : MessageBase
    {
    }

    public class RequestDeviceListCmd : MessageBase
    {
    }

    public class DeviceListCmd : MessageBase
    {
        public List<DeviceAddedCmd> Devices { get; set; }
    }

    public class DeviceAddedCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public string DeviceName { get; set; }
        public Dictionary<string, DeviceMessages> DeviceMessagesDetails { get; set; }

        
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

    public class DeviceRemovedCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    #endregion

    #region Raw Messages
    // Not implemented rn


    #endregion

    #region Generic Device Messages

    public class StopDeviceCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class StopAllDevicesCmd : MessageBase
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

    public class BatteryLevelReadingCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public double BatteryLevel { get; set; }
    }

    public class RSSILevelCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class RSSILevelReadingCmd : MessageBase
    {
        public uint DeviceIndex { get; set; }
        public int RSSILevel { get; set; }
    }

    #endregion





}
