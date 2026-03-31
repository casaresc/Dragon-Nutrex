using System;
namespace Dragon_Nutrex.Utils
{
    public static class EnumHelpers
    {
        private static readonly Random _random = new Random();

        public static T RandomEnumValue<T>() where T : struct, Enum
        {
           
            Array values = Enum.GetValues(typeof(T));

            if (values.Length == 0)
                throw new InvalidOperationException($"El Enum {typeof(T).Name} no tiene valores definidos.");

            int randomIndex = _random.Next(0, values.Length);

            return (T)values.GetValue(randomIndex)!;
        }
    }
}