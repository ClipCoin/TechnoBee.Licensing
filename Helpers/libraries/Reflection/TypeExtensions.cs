using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Helpers
{
    public static class TypeExtensions
    {
        public static Object InvokeDefaultConstructor(this Type type)
        {
            return type.GetConstructor(new Type[0])
                .Invoke(new Object[0]);
        }

        public static T InvokeDefaultConstructor<T>(this Type type)
            where T: class
        {
            return type.GetConstructor(new Type[0])
                .Invoke(new Object[0]) as T;
        }

        public static T InvokeDefaultConstructor<T>()
            where T : class
        {
            return InvokeDefaultConstructor<T>(typeof(T));
        }
    }
}
