using ClassLibrary1_Prueba;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace IPOkemonApp
{
    public sealed partial class OddishJCS : UserControl, iPokemon
    {
        private DispatcherTimer dtTime;

        public OddishJCS()
        {
            this.InitializeComponent();
        }

        public double Vida
        {
            get { return pbHealth.Value; }
            set
            {
                pbHealth.Value = value;
                if (pbHealth.Value > 100) pbHealth.Value = 100;
                if (pbHealth.Value < 0) pbHealth.Value = 0;
            }
        }

        public double Energia
        {
            get { return pbEnergy.Value; }
            set
            {
                pbEnergy.Value = value;
                if (pbEnergy.Value > 100) pbEnergy.Value = 100;
                if (pbEnergy.Value < 0) pbEnergy.Value = 0;
            }
        }

        public string Nombre { get; set; } = "Oddish";
        public string Categoría { get; set; } = "Hierba";
        public string Tipo { get; set; } = "Planta/Veneno";
        public double Altura { get; set; } = 0.5;
        public double Peso { get; set; } = 5.4;
        public string Evolucion { get; set; } = "Gloom";
        public string Descripcion { get; set; } = "Pokémon tipo planta que se mueve bajo la luz de la luna.";


        

        public void verFondo(bool ver) { }
        public void verFilaVida(bool ver) => pbHealth.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        public void verFilaEnergia(bool ver) => pbEnergy.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        public void verPocionVida(bool ver) => imgPotionRed.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        public void verPocionEnergia(bool ver) => imgPotionYellow.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        public void verNombre(bool ver) => pokemonNombre.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        public void verEscudo(bool ver) { }

        public void activarAniIdle(bool activar) { }
        public void animacionAtaqueFlojo() { }
        public void animacionAtaqueFuerte() { }
        public void animacionDefensa() { }
        public void animacionDescasar() { }
        public void animacionCansado() { }
        public void animacionNoCansado() { }
        public void animacionHerido() { }
        public void animacionNoHerido() { }
        public void animacionDerrota() { }

        private void UsePotionRed(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            dtTime.Tick += IncreaseHealth;
            dtTime.Start();
            imgPotionRed.Visibility = Visibility.Collapsed;
        }

        private void UsePotionYellow(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            dtTime.Tick += IncreaseEnergy;
            dtTime.Start();
            imgPotionYellow.Visibility = Visibility.Collapsed;
        }

        private void IncreaseHealth(object sender, object e)
        {
            if (pbHealth.Value < 100)
            {
                pbHealth.Value += 2;
            }
            else
            {
                dtTime.Stop();
                dtTime.Tick -= IncreaseHealth;
            }
        }

        private void IncreaseEnergy(object sender, object e)
        {
            if (pbEnergy.Value < 100)
            {
                pbEnergy.Value += 2;
            }
            else
            {
                dtTime.Stop();
                dtTime.Tick -= IncreaseEnergy;
            }
        }

        private void pbHealth_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (pbHealth.Value <= 30) animacionHerido();
            else animacionNoHerido();

            if (pbHealth.Value == 0) animacionDerrota();
        }

        private void pbEnergy_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (pbEnergy.Value <= 30) animacionCansado();
            else animacionNoCansado();
        }
    }
}
