using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IPOkemonApp
{
    public sealed partial class InicioPage : Page
    {
        public InicioPage()
        {
            InitializeComponent();
            MostrarMensajeBienvenida();
        }

        // Muestra el mensaje de bienvenida en el TextBlock
        private void MostrarMensajeBienvenida()
        {
            tbBienvenida.Text = @"¡Bienvenido a IPokemon!

En este mundo de criaturas fascinantes, tendrás la oportunidad de:

• Ver en todo momento tu colección de Pokémon
• Competir en combates desafiantes
• Descubrir Pokémon legendarios
• Convertirte en el Entrenador definitivo

¡Cada decisión que tomes dará forma a tu leyenda!";
        }
    }
}
