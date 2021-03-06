﻿using System;
using PrtgAPI.Attributes;
using PrtgAPI.Request;

namespace PrtgAPI.Parameters
{
    /// <summary>
    /// <para type="description">Represents parameters used to construct a <see cref="PrtgUrl"/> for adding new <see cref="Sensor"/> objects.</para>
    /// </summary>
    public abstract class NewSensorParameters : NewObjectParameters
    {
        internal override CommandFunction Function => CommandFunction.AddSensor5;

        /// <summary>
        /// Gets or sets the priority of the sensor, controlling how the sensor is displayed in table lists.
        /// </summary>
        [PropertyParameter(nameof(ObjectProperty.Priority))]
        public Priority? Priority
        {
            get { return (Priority?)GetCustomParameterEnumXml<Priority?>(ObjectProperty.Priority); }
            set { SetCustomParameterEnumXml(ObjectProperty.Priority, value); }
        }

        /// <summary>
        /// Gets or sets whether to inherit notification triggers from the parent object.
        /// </summary>
        [PropertyParameter(nameof(ObjectProperty.InheritTriggers))]
        public bool? InheritTriggers
        {
            get { return GetCustomParameterBool(ObjectProperty.InheritTriggers); }
            set { SetCustomParameterBool(ObjectProperty.InheritTriggers, value); }
        }

        /// <summary>
        /// Gets or sets whether this sensor's scanning interval settings are inherited from its parent.
        /// </summary>
        [PropertyParameter(nameof(ObjectProperty.InheritInterval))]
        public bool? InheritInterval
        {
            get { return GetCustomParameterBool(ObjectProperty.InheritInterval); }
            set { SetCustomParameterBool(ObjectProperty.InheritInterval, value); }
        }

        /// <summary>
        /// Gets or sets the scanning interval of the sensor. Applies only if <see cref="InheritInterval"/> is false.
        /// </summary>
        [PropertyParameter(nameof(ObjectProperty.Interval))]
        public ScanningInterval Interval
        {
            get { return (ScanningInterval)GetCustomParameter(ObjectProperty.Interval); }
            set { SetCustomParameter(ObjectProperty.Interval, value); }
        }

        /// <summary>
        /// Gets or sets the number of scanning intervals the sensor will wait before entering a <see cref="Status.Down"/> state when the sensor reports an error.
        /// </summary>
        [PropertyParameter(nameof(ObjectProperty.IntervalErrorMode))]
        public IntervalErrorMode? IntervalErrorMode
        {
            get { return (IntervalErrorMode?)GetCustomParameterEnumXml<IntervalErrorMode?>(ObjectProperty.IntervalErrorMode); }
            set { SetCustomParameterEnumXml(ObjectProperty.IntervalErrorMode, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewSensorParameters"/> class.
        /// </summary>
        /// <param name="sensorName">The name to use for this sensor.</param>
        /// <param name="sensorType">The type of sensor these parameters will create.</param>
        protected NewSensorParameters(string sensorName, object sensorType) : base(sensorName)
        {
            if (string.IsNullOrEmpty(sensorType?.ToString()))
                throw new ArgumentException($"{nameof(sensorType)} cannot be null or empty", nameof(sensorType));

            this[Parameter.SensorType] = sensorType;
        }
    }
}
