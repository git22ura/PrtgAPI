﻿using System.Xml.Serialization;
using Prtg.Attributes;

namespace Prtg.Objects.Shared
{
    /// <summary>
    /// Properties that apply to Devices, Groups and Probes
    /// </summary>
    public class DeviceOrGroupOrProbe : SensorOrDeviceOrGroupOrProbe
    {
        /// <summary>
        /// If this object is a Probe, connected status of the probe. If this object is a group or device, auto-discovery progress (if one is in progress). Otherwise, null.
        /// </summary>
        [XmlElement("condition")]
        [PropertyParameter(nameof(Property.Condition))]
        public string Condition { get; set; }


        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.Up"/> state.
        /// </summary>
        [XmlElement("upsens_raw")]
        [PropertyParameter(nameof(Property.UpSens))]
        public int? UpSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.Down"/> state.
        /// </summary>
        [XmlElement("downsens_raw")]
        [PropertyParameter(nameof(Property.DownSens))]
        public int? DownSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.DownAcknowledged"/> state.
        /// </summary>
        [XmlElement("downacksens_raw")]
        [PropertyParameter(nameof(Property.DownAckSens))]
        public int? DownAcknowledgedSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.DownPartial"/> state.
        /// </summary>
        [XmlElement("partialdownsens_raw")]
        [PropertyParameter(nameof(Property.PartialDownSens))]
        public int? PartialDownSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.Warning"/> state.
        /// </summary>
        [XmlElement("warnsens_raw")]
        [PropertyParameter(nameof(Property.WarnSens))]
        public int? WarningSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.PausedbyUser"/>, <see cref="Prtg.SensorStatus.PausedbyDependency"/>, <see cref="Prtg.SensorStatus.PausedbySchedule"/> or <see cref="Prtg.SensorStatus.PausedbyLicense"/> state.
        /// </summary>
        [XmlElement("pausedsens_raw")]
        [PropertyParameter(nameof(Property.PausedSens))]
        public int? PausedSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.Unusual"/> state.
        /// </summary>
        [XmlElement("unusualsens_raw")]
        [PropertyParameter(nameof(Property.UnusualSens))]
        public int? UnusualSensors { get; set; }

        /// <summary>
        /// Number of sensors in <see cref="Prtg.SensorStatus.Unknown"/> state.
        /// </summary>
        [XmlElement("undefinedsens_raw")]
        [PropertyParameter(nameof(Property.UndefinedSens))]
        public int? UndefinedSensors { get; set; }

        /// <summary>
        /// Total number of sensors in any <see cref="T:Prtg.SensorStatus"/> state.
        /// </summary>
        [XmlElement("totalsens")]
        [PropertyParameter(nameof(Property.TotalSens))]
        public int? TotalSensors { get; set; }

        /// <summary>
        /// Location of this object.
        /// </summary>
        [XmlElement("location_raw")]
        [PropertyParameter(nameof(Property.Location))]
        public string Location { get; set; }
    }
}