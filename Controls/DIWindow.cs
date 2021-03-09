using System;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;

namespace Avalonia.Extensions.DependencyInjection.Controls {
    public class DIWindow : Window, IInjectable {
        protected DIWindow() {
            this.Inject();
        }
    }
}