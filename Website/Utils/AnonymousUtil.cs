using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Utils
{
    public static class AnonymousUtil
    {
        public static T Cast<T>(T typeHolder, Object x)
        {
            return (T)x;
        }
    }
}