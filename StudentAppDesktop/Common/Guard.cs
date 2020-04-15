using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAppDesktop.Common
{
    public static class Guard
    {
        public static void NotNull(object arg, string name)
        {
            if (ReferenceEquals(null, arg)) throw new ArgumentNullException(name);
        }
    }
}
