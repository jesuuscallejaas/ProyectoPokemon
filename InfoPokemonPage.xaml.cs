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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPOkemonApp
{
    public sealed partial class InfoPokemonPage : Page
    {
        private iPokemon pokemon;

        Frame frame = Window.Current.Content as Frame;
        public InfoPokemonPage()
        {
            this.InitializeComponent();
        }
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            // Comprueba si hay alguna página en el historial de navegación para retroceder
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is iPokemon pokemon)
            {
                tbNombre.Text = "Nombre: " + pokemon.Nombre;
                tbCategoria.Text = "Categoria: " + pokemon.Categoría;
                tbTipo.Text = "Tipo: " + pokemon.Tipo;
                tbAltura.Text = "Altura: " + pokemon.Altura;
                tbPeso.Text = "Peso: " + pokemon.Peso;
                tbEvolucion.Text = "Evolución: " + pokemon.Evolucion;
                tbDesc.Text = "Descripción: " + pokemon.Descripcion;
            }
        }
    }
}