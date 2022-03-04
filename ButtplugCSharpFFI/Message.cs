using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buttplug
{
    public class Message
    {
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

    public class IdBase
    {
        uint Id { get; set; }
    }

    #region Status Messages

    public class OkCmd : IdBase
    {
    }

    public class ErrorCmd : IdBase
    {
        public uint Id { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }


        public enum ErrorCodeEnum
        {
            ERROR_UNKNOWN, ERROR_INIT, ERROR_PING, ERROR_MSG, ERROR_DEVICE
        }
    }

    public class PingCmd : IdBase
    {
    }

    #endregion

    #region Handshake Messages
    public class RequestServerInfoCmd : IdBase
    {
        public string ServerName { get; set; }
        public uint MessageVersion { get; set; }
        public uint MaxPingTime { get; set; }
    }

    public class ServerInfoCmd : IdBase
    {
        public string ClientName { get; set; }
        public uint MessageVersion { get; set; }
    }
    #endregion

    #region Enumeration Messages

    public class StartScanningCmd : IdBase
    {
    }

    public class StopScanningCmd : IdBase
    {
    }
    public class ScanningFinishedCmd : IdBase
    {
    }

    public class RequestDeviceListCmd : IdBase
    {
    }

    public class DeviceListCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
        public string DeviceName { get; set; }
        public DeviceMessages DeviceMessages { get; set; }
    }

    public class DeviceAddedCmd : DeviceListCmd
    {
    }

    public class DeviceMessages
    {
        //TODO
        public Dictionary<string, int> VibrateCmd { get; set; }
        public Dictionary<string, int> StopDeviceCmd { get; set; }
        public Dictionary<string, int> BatteryLevelCmd { get; set; }
    }

    public class DeviceRemovedCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
    }

    #endregion

    #region Raw Messages
    // Not implemented rn


    #endregion

    #region Generic Device Messages

    public class StopDeviceCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class StopAllDevicesCmd : IdBase
    {
    }
    public class VibrateCmd : IdBase
    {
        public uint DeviceIndex { get; set; }

        public List<VibrateSpeed> Speeds { get; set; }
    }

    public class VibrateSpeed
    {
        public uint Index { get; set; }
        public double Speed { get; set; }
    }

    public class LinearCmd : IdBase
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

    public class RotateCmd : IdBase
    {
        public uint DeviceIndex { get; set; }

        public List<Rotations> Rotations { get; set; }
    }

    public class Rotations
    {
        public uint Index { get; set; }
        public uint Speed { get; set; }
        public bool Clockwise { get; set; }
    }
    #endregion

    #region Generic Sensor Messages

    public class BatteryLevelCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class BatteryLevelReadingCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
        public double BatteryLevel { get; set; }
    }

    public class RSSILevelCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
    }

    public class RSSILevelReadingCmd : IdBase
    {
        public uint DeviceIndex { get; set; }
        public int RSSILevel { get; set; }
    }

    #endregion





}
