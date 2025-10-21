using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace IPOkemonApp
{
    public sealed partial class MainPage : Page
    {
        // Constantes y variables privadas
        private readonly SolidColorBrush _botonActivo = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 30, 58, 138));
        private string _pestanaActual = "Inicio";

        public MainPage()
        {
            InitializeComponent();
            fmMain.Navigate(typeof(InicioPage));
            ConfigurarResponsividad();
        }

        // Configuración de la responsividad de la aplicación
        private void ConfigurarResponsividad()
        {
            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.SetPreferredMinSize(new Size(360, 640));

            SizeChanged += (s, e) =>
            {
                if (e.NewSize.Width >= 720)
                {
                    sView.DisplayMode = SplitViewDisplayMode.CompactInline;
                    sView.IsPaneOpen = true;
                }
                else
                {
                    sView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    sView.IsPaneOpen = false;
                }
            };
        }

        // Actualiza el botón activo en el menú lateral
        private void ActualizarBotonActivo(string nuevaPestana)
        {
            // Reiniciar estilos de todos los botones
            btnInicio.Background = null;
            btnMisPokemon.Background = null;
            btnPokedex.Background = null;
            btnCombate.Background = null;
            btnAcercaDe.Background = null;

            // Aplicar estilo al botón activo
            switch (nuevaPestana)
            {
                case "Inicio":
                    btnInicio.Background = _botonActivo;
                    break;
                case "Mi Equipo":
                    btnMisPokemon.Background = _botonActivo;
                    break;
                case "Pokédex":
                    btnPokedex.Background = _botonActivo;
                    break;
                case "Combate":
                    btnCombate.Background = _botonActivo;
                    break;
                case "Ayuda":
                    btnAcercaDe.Background = _botonActivo;
                    break;
            }

            _pestanaActual = nuevaPestana;
        }

        // Métodos de navegación
        private void NavegarA(Type pagina, string pestana)
        {
            fmMain.Navigate(pagina);
            ActualizarBotonActivo(pestana);
        }

        // Eventos de clic de los botones
        private void btnInicio_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(InicioPage), "Inicio");

        private void btnMisPokemon_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(MisPokemonPage), "Mi Equipo");

        private void btnPokedex_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(PokeDexPage), "Pokédex");

        private void btnCombate_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(CombatePage), "Combate");

        private void btnAcercaDe_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(AcercaDePage), "Ayuda");

        private void btnMenu_Click(object sender, RoutedEventArgs e)
            => sView.IsPaneOpen = !sView.IsPaneOpen;
    }
}
