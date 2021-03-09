using Avalonia.Controls;

namespace Avalonia.Extensions.DependencyInjection.Controls {
    public class DIUserControl : UserControl, IInjectable {
        protected DIUserControl() {
            this.Inject();
        }
    }
}