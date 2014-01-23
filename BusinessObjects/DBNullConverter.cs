using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class DBNullConverter
    {
        public const int NullInteger = -100;

        public static bool ToBool(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return false;
            }
            return Convert.ToBoolean(Value);
        }

        public static Int64 ToInt64(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return NullInteger;
            }
            return Convert.ToInt64(Value);
        }

        public static Int32 ToInt32(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return NullInteger;
            }
            return Convert.ToInt32(Value);
        }

        public static int ToInteger(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return NullInteger;
            }
            return Convert.ToInt32(Value);
        }

        public static Int16 ToInt16(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return NullInteger;
            }
            return Convert.ToInt16(Value);
        }

        public static string ToStr(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return "";
            }
            return Convert.ToString(Value);
        }

        public static double ToDouble(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return NullInteger;
            }
            return Convert.ToDouble(Value);
        }

        public static DateTime ToDateTime(object value)
        {
            if ((value == null) || (value == DBNull.Value)) return DateTime.MinValue;
            return Convert.ToDateTime(value);
        }
    }
}
