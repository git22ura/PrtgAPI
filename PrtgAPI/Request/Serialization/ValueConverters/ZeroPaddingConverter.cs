﻿namespace PrtgAPI.Request.Serialization.ValueConverters
{
    class ZeroPaddingConverter : IntValueConverter, IZeroPaddingConverter
    {
        public override string Serialize(int value) => Pad(value, true);

        public override int Deserialize(int value) => value;

        public override int SerializeT(int value) => value;

        public string Pad(object value, bool pad)
        {
            var val = SerializeT((int)value);

            if (pad)
                return val.ToString().PadLeft(10, '0');

            return val.ToString();
        }
    }
}
