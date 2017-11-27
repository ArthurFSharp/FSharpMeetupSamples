using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetList.FunctionalExtensions
{
    public static class FSharpExtensions
    {
        public static bool IsSome<T>(this FSharpOption<T> option)
        {
            return FSharpOption<T>.get_IsSome(option);
        }

        public static bool IsNone<T>(this FSharpOption<T> option)
        {
            return FSharpOption<T>.get_IsNone(option);
        }

        public static FSharpOption<T> AsOptionVal<T>(this T value)
            where T : struct
        {
            return FSharpOption<T>.Some(value);
        }

        public static FSharpOption<T> AsOptionRef<T>(this T value)
            where T : class
        {
            if (value == null)
            {
                return FSharpOption<T>.None;
            }
            else
            {
                return FSharpOption<T>.Some(value);
            }
        }

        public static T? AsNullable<T>(this FSharpOption<T> option)
            where T : struct
        {
            if (option.IsNone())
            {
                return default(T?);
            }
            else
            {
                return option.Value;
            }
        }

        public static T ToValue<T>(this FSharpOption<T> option, T defaultValue = default(T))
        {
            if (option.IsNone())
            {
                return defaultValue;
            }
            else
            {
                return option.Value;
            }
        }
    }
}
