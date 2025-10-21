using ClassLibrary1_Prueba;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IPOkemonApp
{
    public sealed partial class CombatePage : Page
    {
        private iPokemon pokemonIzquierdo;
        private iPokemon pokemonDerecho;
        private bool turnoJugador = true; 

        // Estados de combate
        private bool escudoIzquierdo = false;
        private bool escudoDerecho = false;

        public CombatePage()
        {
            this.InitializeComponent();

            pokemonIzquierdo = new JigglypufVAR();
            pokemonDerecho = new RegiceVVG();

            pokemonIzquierdo.verNombre(true);
            pokemonDerecho.verNombre(true);

            pokemonIzquierdo.verFilaVida(true);
            pokemonDerecho.verFilaVida(true);
            pokemonIzquierdo.verFilaEnergia(true);
            pokemonDerecho.verFilaEnergia(true);

            pokemonDerecho.Vida = 100;
            pokemonIzquierdo.Vida = 100;
            pokemonDerecho.Energia = 100;
            pokemonIzquierdo.Energia = 100;

            flipViewIzq.ItemsSource = new[] { pokemonIzquierdo };
            flipViewDer.ItemsSource = new[] { pokemonDerecho };

            this.Loaded += CombatePage_Loaded;
        }

        private async void CombatePage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(200);
            ActualizarEstado(); 
        }

        private void ActualizarEstado()
        {
            txtEstadoCombate.Text = turnoJugador ? "Turno del Pokémon Derecho" : "Turno del Pokémon Izquierdo";

            iPokemon actual = turnoJugador ? pokemonIzquierdo : pokemonDerecho;

            // Resetear escudo al inicio del turno
            if (turnoJugador)
            {
                if (escudoIzquierdo)
                {
                    escudoIzquierdo = false;
                    pokemonIzquierdo.verEscudo(false);
                }
            }
            else
            {
                if (escudoDerecho)
                {
                    escudoDerecho = false;
                    pokemonDerecho.verEscudo(false);
                }
            }

            // Comprobar estado de energía y aplicar animación de cansancio
            if (actual.Energia < 25)
            {
                actual.animacionCansado();
            }
            else
            {
                actual.animacionNoCansado();
            }

            // Comprobar estado de vida y aplicar animación de herido
            if (actual.Vida < 25 && actual.Vida > 0)
            {
                actual.animacionHerido();
            }
            else
            {
                actual.animacionNoHerido();
            }
        }


        private void AtaqueDebil_Click(object sender, RoutedEventArgs e)
        {
            var atacante = turnoJugador ? pokemonIzquierdo : pokemonDerecho;
            var defensor = turnoJugador ? pokemonDerecho : pokemonIzquierdo;
            bool defensorEscudo = turnoJugador ? escudoDerecho : escudoIzquierdo;

            if (atacante.Energia >= 20)
            {
                double daño = defensorEscudo ? 5 : 20;
                defensor.Vida -= daño;
                atacante.Energia -= 20;
                atacante.animacionAtaqueFlojo();
                CambiarTurno();
                VerificarDerrota(defensor);

                
            }

            
        }

        private void AtaqueFuerte_Click(object sender, RoutedEventArgs e)
        {
            var atacante = turnoJugador ? pokemonIzquierdo : pokemonDerecho;
            var defensor = turnoJugador ? pokemonDerecho : pokemonIzquierdo;
            bool defensorEscudo = turnoJugador ? escudoDerecho : escudoIzquierdo;

            if (atacante.Energia >= 40)
            {
                double daño = defensorEscudo ? 20 : 35;
                defensor.Vida -= daño;
                atacante.Energia -= 40;
                atacante.animacionAtaqueFuerte();
                CambiarTurno();
                VerificarDerrota(defensor);

                
            }

            
        }

        private void Defender_Click(object sender, RoutedEventArgs e)
        {
            var defensor = turnoJugador ? pokemonIzquierdo : pokemonDerecho;
            defensor.animacionDefensa();
            defensor.verEscudo(true);
            defensor.Energia += 10;

            if (turnoJugador)
                escudoIzquierdo = true;
            else
                escudoDerecho = true;

            CambiarTurno();
        }

        private void Descansar_Click(object sender, RoutedEventArgs e)
        {
            var defensor = turnoJugador ? pokemonIzquierdo : pokemonDerecho;
            defensor.animacionDescasar();
            defensor.Energia += 40;
            defensor.Vida += 5;

            if(defensor.Energia>25)
                defensor.animacionNoCansado();

            CambiarTurno();
        }

        private void CambiarTurno()
        {
            turnoJugador = !turnoJugador;
            ActualizarEstado();
        }

        private void VerificarDerrota(iPokemon derrotado)
        {
            if (derrotado.Vida <= 0)
            {
                derrotado.Vida = 0;
                derrotado.animacionDerrota();
                pokemonIzquierdo.verPocionVida(false);
                pokemonIzquierdo.verPocionEnergia(false);
                pokemonDerecho.verPocionVida(false);
                pokemonDerecho.verPocionEnergia(false);

                // Mostrar resultado
                bool esJugador = derrotado == pokemonIzquierdo;
                txtEstadoCombate.Text = esJugador ? "¡Victoria!" : "¡Derrota!";

                // Ocultar botones de combate
                panelBotones.Visibility = Visibility.Collapsed;

                // Mostrar botón de revancha
                btnRevancha.Visibility = Visibility.Visible;
            }
        }
        private void BtnRevancha_Click(object sender, RoutedEventArgs e)
        {
            // Recargar la página (simplemente navega de nuevo a sí misma)
            this.Frame.Navigate(typeof(CombatePage));
        }

    }
}
