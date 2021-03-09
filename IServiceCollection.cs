using System;
using System.Collections;
using System.Collections.Generic;

namespace Avalonia.Extensions.DependencyInjection {
    public interface IServiceCollection {
        public void AddTransient<T>(Func<T> ctor);
        public void AddSingleton<T>(Func<T> ctor);
    }
}