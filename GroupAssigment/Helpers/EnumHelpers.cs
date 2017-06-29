using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupAssigment.Helpers
{
    public class EnumHelpers
    {

        public static Dictionary<string, T> ToDictionary<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Expected an enumeration.");

            var names = Enum.GetNames(typeof(T));
            var values = Enum.GetValues(typeof(T));

            var list = new Dictionary<string, T>();

            for (int i = 0; i < names.Length; i++)
            {
                var value = values.GetValue(i);

                if (Convert.ToInt64(value) == 0L && names[i].Equals("None")) continue;

                var name = names[i].Replace("_", " ");

                list.Add(name, (T)value);
            }

            return list;
        }

        public static T Combine<T>(IEnumerable<T> enumerations) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Expected an enumeration.");

            long result = 0;

            foreach (var value in enumerations)
            {
                result = result | Convert.ToInt64(value);
            }

            return (T)Enum.ToObject(typeof(T), result);
        }
    }
}