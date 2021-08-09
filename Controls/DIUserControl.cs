using Avalonia.Controls;

namespace Kaharonus.Avalonia.DependencyInjection.Controls {
    public class DIUserControl : UserControl, IInjectable {
        protected DIUserControl() {
            this.Inject();
        }
    }
}