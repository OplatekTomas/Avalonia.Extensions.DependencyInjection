using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kaharonus.Avalonia.DependencyInjection {
    public static class Helper {
        public static IEnumerable<FieldInfo> GetRequiredServices(IReflect t) {
            return t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).Where(x=>Attribute.IsDefined(x, typeof(Inject)));
        }
    }
}