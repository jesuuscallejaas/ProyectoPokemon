using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IPOkemonApp
{
    /// <summary>
    /// Página que muestra los Pokémon del usuario.
    /// </summary>
    public sealed partial class MisPokemonPage : Page
    {
        public MisPokemonPage()
        {
            InitializeComponent();
            ConfigurarControles();
        }

        // Configura los controles de los Pokémon
        private void ConfigurarControles()
        {
            JigglypuffControl.Loaded += (s, e) => ConfigurarJigglypuff();
            RegiceControl.Loaded += (s, e) => ConfigurarRegice();
            OddishControl.Loaded += (s, e) => ConfigurarOddish();
        }

        private void ConfigurarJigglypuff()
        {
            JigglypuffControl.verFondo(false);
            JigglypuffControl.verFilaVida(false);
            JigglypuffControl.verFilaEnergia(false);
            JigglypuffControl.verPocionVida(false);
            JigglypuffControl.verPocionEnergia(false);
            JigglypuffControl.verNombre(false);
            JigglypuffControl.verEscudo1(false);
            JigglypuffControl.verCorazon(false);
            JigglypuffControl.activarAniIdle(true);
        }

        private void ConfigurarRegice()
        {
            RegiceControl.verFondo(false);
            RegiceControl.verFilaVida(false);
            RegiceControl.verFilaEnergia(false);
            RegiceControl.verPocionVida(false);
            RegiceControl.verPocionEnergia(false);
            RegiceControl.verNombre(false);
            RegiceControl.verEscudo(false);
            RegiceControl.activarAniIdle(true);
        }

        private void ConfigurarOddish()
        {
            OddishControl.verFondo(false);
            OddishControl.verFilaVida(false);
            OddishControl.verFilaEnergia(false);
            OddishControl.verPocionVida(false);
            OddishControl.verPocionEnergia(false);
            OddishControl.verNombre(false);
            OddishControl.verEscudo(false);
            OddishControl.activarAniIdle(true);
        }

        // Métodos de navegación
        private void btnJigglypuff_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(InfoPokemonPage), new JigglypufVAR());

        private void btnRegice_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(InfoPokemonPage), new RegiceVVG());

        private void btnOddish_Click(object sender, RoutedEventArgs e)
            => NavegarA(typeof(InfoPokemonPage), new OddishJCS());

        private void NavegarA(System.Type pagina, object parametro)
            => Frame.Navigate(pagina, parametro);

    }
}

