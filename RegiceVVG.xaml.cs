using ClassLibrary1_Prueba;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace IPOkemonApp
{
    public sealed partial class RegiceVVG : UserControl, iPokemon
    {
        DispatcherTimer dtTimeVida;
        DispatcherTimer dtTimeEnergia;

        public double Vida
        {
            get { return pbBarraVida.Value; }
            set
            {
                pbBarraVida.Value = value;
                if (pbBarraVida.Value > 100) pbBarraVida.Value = 100; // Máximo 100
                if (pbBarraVida.Value < 0) pbBarraVida.Value = 0; // Mínimo 0
            }
        }

        public double Energia
        {
            get { return pbEnergia.Value; }
            set
            {
                pbEnergia.Value = value;
                if (pbEnergia.Value > 100) pbEnergia.Value = 100;
                if (pbEnergia.Value < 0) pbEnergia.Value = 0;
            }
        }

        public string Nombre { get; set; } = "Regice";
        public string Categoría { get; set; } = "Iceberg Pokémon";
        public string Tipo { get; set; } = "Hielo";
        public double Altura { get; set; } = 1.8;
        public double Peso { get; set; } = 175.0;
        public string Evolucion { get; set; } = "No evoluciona";
        public string Descripcion { get; set; } = "Regice tiene un cuerpo hecho de hielo ultrafrío.";


        public RegiceVVG()
        {
            this.InitializeComponent();
            this.activarAniIdle(true);
        }


        public void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        public void usarPocionVida(object sender, PointerRoutedEventArgs e)
        {
            dtTimeVida = new DispatcherTimer();
            dtTimeVida.Interval = TimeSpan.FromMilliseconds(100);
            dtTimeVida.Tick += subirVida;
            dtTimeVida.Start();
            this.pocionVida.Visibility = Visibility.Collapsed;

        }
        public void subirVida(object sender, object e)
        {
            this.pbBarraVida.Value += 0.5;
            if (pbBarraVida.Value >= 100)
            {
                this.dtTimeVida.Stop();
            }
        }
        public void usarPocionEnergia(object sender, PointerRoutedEventArgs e)
        {
            dtTimeEnergia = new DispatcherTimer();
            dtTimeEnergia.Interval = TimeSpan.FromMilliseconds(100);
            dtTimeEnergia.Tick += subirEnergia;
            dtTimeEnergia.Start();
            this.pocionEnergia.Visibility = Visibility.Collapsed;

        }
        public void subirEnergia(object sender, object e)
        {
            this.pbEnergia.Value += 0.5;
            if (pbEnergia.Value >= 100)
            {
                this.dtTimeEnergia.Stop();
            }
        }
        public void ControlTeclas(object sender, KeyRoutedEventArgs e)
        {
            Storyboard sbaux7;
            Storyboard sbaux5;
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number7:
                    sbaux7 = (Storyboard)this.Resources["escudo1"];
                    sbaux7.Begin();
                    break;
                case Windows.System.VirtualKey.Number8:
                    sbaux7 = (Storyboard)this.Resources["escudo1"];
                    sbaux7.Stop();
                    break;
                case Windows.System.VirtualKey.Number1:
                    Storyboard sbaux = (Storyboard)this.Resources["ataque_fuerte"];
                    sbaux.Begin();
                    break;

                case Windows.System.VirtualKey.Number2:
                    Storyboard sbaux2 = (Storyboard)this.Resources["ataque_debil"];
                    sbaux2.Begin();
                    break;
                case Windows.System.VirtualKey.Number3:
                    Storyboard sbaux3 = (Storyboard)this.Resources["defensa"];
                    sbaux3.Begin();
                    break;
                case Windows.System.VirtualKey.Number4:
                    Storyboard sbaux4 = (Storyboard)this.Resources["descansar"];
                    sbaux4.Begin();
                    break;
                case Windows.System.VirtualKey.Number5:
                    sbaux5 = (Storyboard)this.Resources["herido"];
                    sbaux5.Begin();
                    break;
                case Windows.System.VirtualKey.Number6:
                    sbaux5 = (Storyboard)this.Resources["herido"];
                    sbaux5.Stop();
                    break;
                case Windows.System.VirtualKey.Number9:
                    sbaux5 = (Storyboard)this.Resources["cansado"];
                    sbaux5.Begin();
                    break;
                case Windows.System.VirtualKey.Number0:
                    sbaux5 = (Storyboard)this.Resources["derrota"];
                    sbaux5.Begin();
                    break;
            }

        }

        public void verFondo(bool ver)
        {
            fondo.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verFilaVida(bool ver)
        {
            pbBarraVida.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            pocionVida.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            corazon.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            filaVida.Height = ver ? new GridLength(50) : new GridLength(0);
        }

        public void verFilaEnergia(bool ver)
        {
            pbEnergia.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            pocionEnergia.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            rayo.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            filaEnergia.Height = ver ? new GridLength(50) : new GridLength(0);
        }

        public void verPocionVida(bool ver)
        {
            pocionVida.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verPocionEnergia(bool ver)
        {
            pocionEnergia.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verNombre(bool ver)
        {
            nombre.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        public void verEscudo(bool ver)
        {
            Storyboard escudo = (Storyboard)this.Resources["escudo1"];
            if (ver) escudo.Begin();
            else escudo.Stop();
        }


        public void activarAniIdle(bool activar)
        {
            Storyboard idle = (Storyboard)this.Resources["idle"];
            if (activar){
                idle.Begin();
            }else idle.Stop();
        }

        public void animacionAtaqueFlojo()
        {
            ((Storyboard)this.Resources["ataque_debil"]).Begin();
        }

        public void animacionAtaqueFuerte()
        {
            ((Storyboard)this.Resources["ataque_fuerte"]).Begin();
        }

        public void animacionDefensa()
        {
            ((Storyboard)this.Resources["defensa"]).Begin();
        }

        public void animacionDescasar()
        {
            ((Storyboard)this.Resources["descansar"]).Begin();
        }

        public void animacionCansado()
        {
            ((Storyboard)this.Resources["cansado"]).Begin();
        }

        public void animacionNoCansado()
        {
            ((Storyboard)this.Resources["cansado"]).Stop();
        }

        public void animacionHerido()
        {
            ((Storyboard)this.Resources["herido"]).Begin();
        }

        public void animacionNoHerido()
        {
            ((Storyboard)this.Resources["herido"]).Stop();
        }

        public void animacionDerrota()
        {
            ((Storyboard)this.Resources["derrota"]).Begin();
        }
    }
}
