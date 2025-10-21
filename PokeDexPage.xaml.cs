using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1_Prueba;
using System;

namespace IPOkemonApp
{
    public sealed partial class PokeDexPage : Page
    {
        private List<iPokemon> todosLosPokemon;

        public PokeDexPage()
        {
            this.InitializeComponent();
            CargarPokemon();
        }

        private void CargarPokemon()
        {
            todosLosPokemon = new List<iPokemon>
            {
                new JigglypufVAR(),
                new RegiceVVG(),
                new OddishJCS(),
                new AzumarillEFAR(),
                new Corphish_JFV(),
                new DratiniGFS(),
                new DunsparcePCA(),
                new GardevoirAPM(),
                new GengarJMC(),
                new GengarRSR(),    
                new GolbatDGMS(),
                new MimikyuCBM(),
                new OshawottHAM(),
                new OshawottJSV(),
                new PachirisuNSL(),
                new PichuJMG(),
                new PigniteJHL(),
                new PorygonCNC(),
                new Porygon2DAR(),
                new PsyduckERP(),
                new RioluMRB(),
                new RioluPATF(),
                new SprigatitoJMBL(),
                new SwabluSCP(),
                new VictiniLDM(),
                new WartortleAAA(),
                new ZygardeFRB(),
            };

            OcultarElementosParaModoPokedex(todosLosPokemon);
            listaPokemon.ItemsSource = todosLosPokemon;
        }

        private void OcultarElementosParaModoPokedex(IEnumerable<iPokemon> pokemons)
        {
            foreach (var p in pokemons)
            {
                p.verFilaVida(false);
                p.verFilaEnergia(false);
                p.verPocionVida(false);
                p.verPocionEnergia(false);
                p.verNombre(true);
                p.verFondo(false);
                if (p is JigglypufVAR jigglypuf)
                {
                    jigglypuf.verEscudo1(false);
                    jigglypuf.verCorazon(false);
                }
                else{
                    p.verEscudo(false);
                }
            }
        }


        private void buscadorPokemon_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string texto = sender.Text?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(texto))
            {
                listaPokemon.ItemsSource = todosLosPokemon;
                return;
            }

            var filtrados = todosLosPokemon
                .Where(p =>
                    !string.IsNullOrEmpty(p.Nombre) &&
                    !string.IsNullOrEmpty(p.Tipo) &&
                    ((p.Nombre?.ToLower().Contains(texto) ?? false) || (p.Tipo?.ToLower().Contains(texto) ?? false))
                ).ToList();

            OcultarElementosParaModoPokedex(filtrados);
            listaPokemon.ItemsSource = filtrados;
        }

        private void listaPokemon_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is iPokemon pokemon)
            {
                Frame.Navigate(typeof(InfoPokemonPage), pokemon);
            }
        }
    }
}
