using Sledilec.Odjemalec.Stroj.PogledniModeli;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sledilec.Odjemalec
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GlavnaStran : Page
    {
        public GlavniPogledniModel PogledniModel { get; }
        public GlavnaStran()
        {
            InitializeComponent();
            PogledniModel = new GlavniPogledniModel();
            var ignore = PogledniModel.NaložiVseAsync(CancellationToken.None);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "PrikažiDodajanje", true);
        }
    }
}
