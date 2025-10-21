using ClassLibrary1_Prueba;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace IPOkemonApp
{
    public sealed partial class JigglypufVAR : UserControl, iPokemon
    {
        private DispatcherTimer dtTime;
        private Storyboard sbIdle;
        private Storyboard sbAttackWeak;
        private Storyboard sbAttackStrong;
        private Storyboard sbDefense;
        private Storyboard sbRest;
        private Storyboard sbInjured;
        private Storyboard sbDefeat;
        private Storyboard sbCansado;
        private Storyboard sbNoCansado;
        private Storyboard sbHerido;
        private Storyboard sbNoHerido;
        private Storyboard sbEscudo;
        private Storyboard sbNoEscudo;

        private bool isCansadoActive = false;
        private bool isHeridoActive = false;
        private bool isDerrotadoActive = false;

        public double Vida
        {
            get { return this.pbHealth.Value; }
            set { this.pbHealth.Value = value; }
        }

        public double Energia
        {
            get { return this.pbEnergy.Value; }
            set { this.pbEnergy.Value = value; }
        }

        public string NombrePok
        {
            get { return this.name.Text; }
            set { this.name.Text = value; }
        }

        private string categoría;
        public string Categoría
        {
            get { return categoría; }
            set { categoría = value; }
        }

        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private double altura;
        public double Altura
        {
            get { return altura; }
            set { altura = value; }
        }

        private double peso;
        public double Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        private string evolucion;
        public string Evolucion
        {
            get { return evolucion; }
            set { evolucion = value; }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string Nombre
        {
            get { return this.NombrePok; }
            set { this.NombrePok = value; }
        }

        public JigglypufVAR()
        {
            this.InitializeComponent();
            this.Loaded += JigglypufVAR_Loaded;
            Vida = 100;
            Energia = 100;
            NombrePok = "Jigglypuff";
            Categoría = "Pokémon de Canción";
            Tipo = "Hada, Normal";
            Altura = 0.5;
            Peso = 5.5;
            Evolucion = "Evoluciona de Igglybuff";
            Descripcion = "Jigglypuff es un Pokémon de tipo hada y normal, conocido por su habilidad para cantar, lo que provoca que sus oponentes se duerman.";
        }

        private void JigglypufVAR_Loaded(object sender, RoutedEventArgs e)
        {
            Vida = 100;
            Energia = 100;
            NombrePok = "Jigglypuff";
            Categoría = "Pokémon de Canción";
            Tipo = "Hada, Normal";
            Altura = 0.5;
            Peso = 5.5;
            Evolucion = "Evoluciona de Igglybuff";
            Descripcion = "Jigglypuff es un Pokémon de tipo hada y normal, conocido por su habilidad para cantar, lo que provoca que sus oponentes se duerman.";

            if (imRPotion != null)
                this.imRPotion.PointerPressed += usePotionRed;
            if (imYPotion != null)
                this.imYPotion.PointerPressed += usePotionYellow;

            InitializeAnimations();

            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromSeconds(0.1);
            dtTime.Tick += DtTime_Tick;

            activarAniIdle(true);
        }

        private void InitializeAnimations()
        {
            sbIdle = new Storyboard();
            DoubleAnimation aniIdleY1 = new DoubleAnimation
            {
                To = -10,
                Duration = TimeSpan.FromSeconds(0.8),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(aniIdleY1, translateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniIdleY1, "Y");
            sbIdle.Children.Add(aniIdleY1);

            sbAttackWeak = new Storyboard();

            DoubleAnimation aniAttackWeakY1 = new DoubleAnimation
            {
                To = -20,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniAttackWeakY1, translateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniAttackWeakY1, "Y");
            sbAttackWeak.Children.Add(aniAttackWeakY1);

            DoubleAnimation aniAttackEffectOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(0.1)
            };
            Storyboard.SetTarget(aniAttackEffectOpacity, imgAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectOpacity, "Opacity");
            sbAttackWeak.Children.Add(aniAttackEffectOpacity);

            DoubleAnimation aniAttackEffectScaleX = new DoubleAnimation
            {
                From = 0.5,
                To = 1.2,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(0.1)
            };
            Storyboard.SetTarget(aniAttackEffectScaleX, scaleTransformAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectScaleX, "ScaleX");
            sbAttackWeak.Children.Add(aniAttackEffectScaleX);

            DoubleAnimation aniAttackEffectScaleY = new DoubleAnimation
            {
                From = 0.5,
                To = 1.2,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(0.1)
            };
            Storyboard.SetTarget(aniAttackEffectScaleY, scaleTransformAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectScaleY, "ScaleY");
            sbAttackWeak.Children.Add(aniAttackEffectScaleY);

            DoubleAnimation aniAttackEffectRotate = new DoubleAnimation
            {
                To = 360,
                Duration = TimeSpan.FromSeconds(0.5),
                RepeatBehavior = new RepeatBehavior(1),
                BeginTime = TimeSpan.FromSeconds(0.1)
            };
            Storyboard.SetTarget(aniAttackEffectRotate, rotateTransformAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectRotate, "Angle");
            sbAttackWeak.Children.Add(aniAttackEffectRotate);

            ObjectAnimationUsingKeyFrames aniAttackEffectVisibility = new ObjectAnimationUsingKeyFrames();
            aniAttackEffectVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = Visibility.Visible });
            aniAttackEffectVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0.5), Value = Visibility.Collapsed });
            Storyboard.SetTarget(aniAttackEffectVisibility, imgAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectVisibility, "Visibility");
            sbAttackWeak.Children.Add(aniAttackEffectVisibility);


            sbAttackWeak.Completed += (s, e) =>
            {
                imgAttackEffect.Opacity = 0;

                if (!dtTime.IsEnabled)
                {
                    dtTime.Start();
                }
            };

            sbAttackStrong = new Storyboard();
            DoubleAnimation aniAttackStrongY1 = new DoubleAnimation
            {
                To = -40,
                Duration = TimeSpan.FromSeconds(0.3),
                AutoReverse = true,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniAttackStrongY1, translateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniAttackStrongY1, "Y");
            sbAttackStrong.Children.Add(aniAttackStrongY1);

            DoubleAnimation aniAttackStrongRotate = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(0.6),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniAttackStrongRotate, rotateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniAttackStrongRotate, "Angle");
            sbAttackStrong.Children.Add(aniAttackStrongRotate);

            DoubleAnimation aniAttackEffectScaleStrongX = new DoubleAnimation
            {
                From = 0.8,
                To = 1.5,
                Duration = TimeSpan.FromSeconds(0.3),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(0.2)
            };
            Storyboard.SetTarget(aniAttackEffectScaleStrongX, scaleTransformAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectScaleStrongX, "ScaleX");
            sbAttackStrong.Children.Add(aniAttackEffectScaleStrongX);

            DoubleAnimation aniAttackEffectScaleStrongY = new DoubleAnimation
            {
                From = 0.8,
                To = 1.5,
                Duration = TimeSpan.FromSeconds(0.3),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(0.2)
            };
            Storyboard.SetTarget(aniAttackEffectScaleStrongY, scaleTransformAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectScaleStrongY, "ScaleY");
            sbAttackStrong.Children.Add(aniAttackEffectScaleStrongY);

            DoubleAnimation aniAttackEffectOpacityStrong = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(0.2)
            };
            Storyboard.SetTarget(aniAttackEffectOpacityStrong, imgAttackEffect);
            Storyboard.SetTargetProperty(aniAttackEffectOpacityStrong, "Opacity");
            sbAttackStrong.Children.Add(aniAttackEffectOpacityStrong);

            sbAttackStrong.Completed += (s, e) =>
            {
                imgAttackEffect.Visibility = Visibility.Collapsed;
                imgAttackEffect.Opacity = 0;
                if (!dtTime.IsEnabled)
                {
                    dtTime.Start();
                }
            };


            sbDefense = new Storyboard();
            DoubleAnimation aniShieldScaleX = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniShieldScaleX, scaleTransformEscudo);
            Storyboard.SetTargetProperty(aniShieldScaleX, "ScaleX");
            sbDefense.Children.Add(aniShieldScaleX);

            DoubleAnimation aniShieldScaleY = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniShieldScaleY, scaleTransformEscudo);
            Storyboard.SetTargetProperty(aniShieldScaleY, "ScaleY");
            sbDefense.Children.Add(aniShieldScaleY);

            DoubleAnimation aniShieldOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            Storyboard.SetTarget(aniShieldOpacity, imgEscudo);
            Storyboard.SetTargetProperty(aniShieldOpacity, "Opacity");
            sbDefense.Children.Add(aniShieldOpacity);

            ObjectAnimationUsingKeyFrames aniShieldVisibility = new ObjectAnimationUsingKeyFrames();
            aniShieldVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = Visibility.Visible });
            aniShieldVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(3), Value = Visibility.Collapsed });
            Storyboard.SetTarget(aniShieldVisibility, imgEscudo);
            Storyboard.SetTargetProperty(aniShieldVisibility, "Visibility");
            sbDefense.Children.Add(aniShieldVisibility);

            sbDefense.Completed += (s, e) =>
            {
                if (!dtTime.IsEnabled)
                {
                    dtTime.Start();
                }
            };

            sbRest = new Storyboard();

            DoubleAnimation aniRestY = new DoubleAnimation
            {
                From = 0,
                To = 10,
                Duration = TimeSpan.FromSeconds(0.8),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(1),
                EasingFunction = new BounceEase { Bounces = 1, Bounciness = 1, EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniRestY, translateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniRestY, "Y");
            sbRest.Children.Add(aniRestY);

            DoubleAnimation aniRestRotate = new DoubleAnimation
            {
                From = 0,
                To = 5,
                Duration = TimeSpan.FromSeconds(0.4),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTarget(aniRestRotate, rotateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniRestRotate, "Angle");
            sbRest.Children.Add(aniRestRotate);


            sbRest.Completed += (s, e) =>
            {
                if (!dtTime.IsEnabled)
                {
                    dtTime.Start();
                }

                if (rotateTransformJigglypuff != null)
                {
                    rotateTransformJigglypuff.Angle = 0;
                }
            };

            sbCansado = new Storyboard();

            DoubleAnimation aniSweatOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniSweatOpacity, imgSweatDrop);
            Storyboard.SetTargetProperty(aniSweatOpacity, "Opacity");
            sbCansado.Children.Add(aniSweatOpacity);

            DoubleAnimation aniSweatFallIn = new DoubleAnimation
            {
                From = -20,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new BounceEase { Bounces = 1, Bounciness = 2, EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniSweatFallIn, translateTransformSweatDrop);
            Storyboard.SetTargetProperty(aniSweatFallIn, "Y");
            sbCansado.Children.Add(aniSweatFallIn);

            ObjectAnimationUsingKeyFrames aniSweatVisibility = new ObjectAnimationUsingKeyFrames();
            aniSweatVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = Visibility.Visible });
            Storyboard.SetTarget(aniSweatVisibility, imgSweatDrop);
            Storyboard.SetTargetProperty(aniSweatVisibility, "Visibility");
            sbCansado.Children.Add(aniSweatVisibility);


            sbNoCansado = new Storyboard();

            DoubleAnimation aniNoSweatOpacity = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTarget(aniNoSweatOpacity, imgSweatDrop);
            Storyboard.SetTargetProperty(aniNoSweatOpacity, "Opacity");
            sbNoCansado.Children.Add(aniNoSweatOpacity);

            DoubleAnimation aniSweatFallOut = new DoubleAnimation
            {
                From = 0,
                To = 20,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTarget(aniSweatFallOut, translateTransformSweatDrop);
            Storyboard.SetTargetProperty(aniSweatFallOut, "Y");
            sbNoCansado.Children.Add(aniSweatFallOut);


            sbNoCansado.Completed += (s, e) =>
            {
                imgSweatDrop.Visibility = Visibility.Collapsed;
                if (translateTransformSweatDrop != null)
                {
                    translateTransformSweatDrop.Y = 0;
                }
            };


            sbHerido = new Storyboard();

            DoubleAnimation aniHeridaOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.6),
            };
            Storyboard.SetTarget(aniHeridaOpacity, imgHerida);
            Storyboard.SetTargetProperty(aniHeridaOpacity, "Opacity");
            sbHerido.Children.Add(aniHeridaOpacity);

            DoubleAnimation aniHeridaScaleX = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniHeridaScaleX, scaleTransformHerida);
            Storyboard.SetTargetProperty(aniHeridaScaleX, "ScaleX");
            sbHerido.Children.Add(aniHeridaScaleX);

            DoubleAnimation aniHeridaScaleY = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.4),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniHeridaScaleY, scaleTransformHerida);
            Storyboard.SetTargetProperty(aniHeridaScaleY, "ScaleY");
            sbHerido.Children.Add(aniHeridaScaleY);

            ObjectAnimationUsingKeyFrames aniHeridaVisibility = new ObjectAnimationUsingKeyFrames();
            aniHeridaVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = Visibility.Visible });
            Storyboard.SetTarget(aniHeridaVisibility, imgHerida);
            Storyboard.SetTargetProperty(aniHeridaVisibility, "Visibility");
            sbHerido.Children.Add(aniHeridaVisibility);

            DoubleAnimation aniJigglypufRotation = new DoubleAnimation
            {
                From = 0,
                To = 10,
                Duration = TimeSpan.FromSeconds(0.1),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniJigglypufRotation, rotateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniJigglypufRotation, "Angle");
            sbHerido.Children.Add(aniJigglypufRotation);


            sbHerido.Completed += (s, e) =>
            {
                if (rotateTransformJigglypuff != null)
                {
                    rotateTransformJigglypuff.Angle = 0;
                }
            };


            sbNoHerido = new Storyboard();

            DoubleAnimation aniNoHeridaOpacity = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            Storyboard.SetTarget(aniNoHeridaOpacity, imgHerida);
            Storyboard.SetTargetProperty(aniNoHeridaOpacity, "Opacity");
            sbNoHerido.Children.Add(aniNoHeridaOpacity);

            DoubleAnimation aniNoHeridoRotationReset = new DoubleAnimation
            {
                From = rotateTransformJigglypuff.Angle,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            Storyboard.SetTarget(aniNoHeridoRotationReset, rotateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniNoHeridoRotationReset, "Angle");
            sbNoHerido.Children.Add(aniNoHeridoRotationReset);


            sbNoHerido.Completed += (s, e) =>
            {
                imgHerida.Visibility = Visibility.Collapsed;
                if (imgHerida != null)
                {
                    imgHerida.Opacity = 0;
                }
                if (scaleTransformHerida != null)
                {
                    scaleTransformHerida.ScaleX = 1;
                    scaleTransformHerida.ScaleY = 1;
                }

                if (rotateTransformJigglypuff != null)
                {
                    rotateTransformJigglypuff.Angle = 0;
                }
            };

            sbDefeat = new Storyboard();

            DoubleAnimation aniDefeatY = new DoubleAnimation
            {
                To = 100,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new BounceEase { EasingMode = EasingMode.EaseOut, Bounces = 3, Bounciness = 3 }
            };
            Storyboard.SetTarget(aniDefeatY, translateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniDefeatY, "Y");
            sbDefeat.Children.Add(aniDefeatY);

            DoubleAnimation aniDefeatRotation = new DoubleAnimation
            {
                To = 90,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniDefeatRotation, rotateTransformJigglypuff);
            Storyboard.SetTargetProperty(aniDefeatRotation, "Angle");
            sbDefeat.Children.Add(aniDefeatRotation);

            DoubleAnimation aniSpiralOpacityLeft = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(0.8)
            };
            Storyboard.SetTarget(aniSpiralOpacityLeft, imgSpiralLeftEye);
            Storyboard.SetTargetProperty(aniSpiralOpacityLeft, "Opacity");
            sbDefeat.Children.Add(aniSpiralOpacityLeft);

            ObjectAnimationUsingKeyFrames aniSpiralVisibilityLeft = new ObjectAnimationUsingKeyFrames();
            aniSpiralVisibilityLeft.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0.8), Value = Visibility.Visible });
            Storyboard.SetTarget(aniSpiralVisibilityLeft, imgSpiralLeftEye);
            Storyboard.SetTargetProperty(aniSpiralVisibilityLeft, "Visibility");
            sbDefeat.Children.Add(aniSpiralVisibilityLeft);

            DoubleAnimation aniSpiralRotateLeft = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                BeginTime = TimeSpan.FromSeconds(0.8)
            };
            Storyboard.SetTarget(aniSpiralRotateLeft, rotateTransformSpiralLeft);
            Storyboard.SetTargetProperty(aniSpiralRotateLeft, "Angle");
            sbDefeat.Children.Add(aniSpiralRotateLeft);


            DoubleAnimation aniSpiralOpacityRight = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(0.8)
            };
            Storyboard.SetTarget(aniSpiralOpacityRight, imgSpiralRightEye);
            Storyboard.SetTargetProperty(aniSpiralOpacityRight, "Opacity");
            sbDefeat.Children.Add(aniSpiralOpacityRight);

            ObjectAnimationUsingKeyFrames aniSpiralVisibilityRight = new ObjectAnimationUsingKeyFrames();
            aniSpiralVisibilityRight.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0.8), Value = Visibility.Visible });
            Storyboard.SetTarget(aniSpiralVisibilityRight, imgSpiralRightEye);
            Storyboard.SetTargetProperty(aniSpiralVisibilityRight, "Visibility");
            sbDefeat.Children.Add(aniSpiralVisibilityRight);

            DoubleAnimation aniSpiralRotateRight = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(2),
                RepeatBehavior = RepeatBehavior.Forever,
                BeginTime = TimeSpan.FromSeconds(0.8)
            };
            Storyboard.SetTarget(aniSpiralRotateRight, rotateTransformSpiralRight);
            Storyboard.SetTargetProperty(aniSpiralRotateRight, "Angle");
            sbDefeat.Children.Add(aniSpiralRotateRight);


            DoubleAnimation aniDefeatHeridaOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.FromSeconds(0.5)
            };
            Storyboard.SetTarget(aniDefeatHeridaOpacity, imgHerida);
            Storyboard.SetTargetProperty(aniDefeatHeridaOpacity, "Opacity");
            sbDefeat.Children.Add(aniDefeatHeridaOpacity);

            DoubleAnimation aniDefeatHeridaScaleX = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                BeginTime = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniDefeatHeridaScaleX, scaleTransformHerida);
            Storyboard.SetTargetProperty(aniDefeatHeridaScaleX, "ScaleX");
            sbDefeat.Children.Add(aniDefeatHeridaScaleX);

            DoubleAnimation aniDefeatHeridaScaleY = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                BeginTime = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniDefeatHeridaScaleY, scaleTransformHerida);
            Storyboard.SetTargetProperty(aniDefeatHeridaScaleY, "ScaleY");
            sbDefeat.Children.Add(aniDefeatHeridaScaleY);

            ObjectAnimationUsingKeyFrames aniDefeatHeridaVisibility = new ObjectAnimationUsingKeyFrames();
            aniDefeatHeridaVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0.5), Value = Visibility.Visible });
            Storyboard.SetTarget(aniDefeatHeridaVisibility, imgHerida);
            Storyboard.SetTargetProperty(aniDefeatHeridaVisibility, "Visibility");
            sbDefeat.Children.Add(aniDefeatHeridaVisibility);


            sbDefeat.Completed += (s, e) =>
            {
                if (dtTime != null) dtTime.Stop();

                if (translateTransformJigglypuff != null) translateTransformJigglypuff.Y = 100;
                if (rotateTransformJigglypuff != null) rotateTransformJigglypuff.Angle = 90;

                if (imgSpiralLeftEye != null) { imgSpiralLeftEye.Opacity = 1; imgSpiralLeftEye.Visibility = Visibility.Visible; }
                if (imgSpiralRightEye != null) { imgSpiralRightEye.Opacity = 1; imgSpiralRightEye.Visibility = Visibility.Visible; }
                if (imgHerida != null) { imgHerida.Opacity = 1; imgHerida.Visibility = Visibility.Visible; }

            };

            sbEscudo = new Storyboard();
            DoubleAnimation aniShowShieldScaleX = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniShowShieldScaleX, scaleTransformEscudo);
            Storyboard.SetTargetProperty(aniShowShieldScaleX, "ScaleX");
            sbEscudo.Children.Add(aniShowShieldScaleX);

            DoubleAnimation aniShowShieldScaleY = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(aniShowShieldScaleY, scaleTransformEscudo);
            Storyboard.SetTargetProperty(aniShowShieldScaleY, "ScaleY");
            sbEscudo.Children.Add(aniShowShieldScaleY);

            DoubleAnimation aniShowShieldOpacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            Storyboard.SetTarget(aniShowShieldOpacity, imgEscudo);
            Storyboard.SetTargetProperty(aniShowShieldOpacity, "Opacity");
            sbEscudo.Children.Add(aniShowShieldOpacity);

            ObjectAnimationUsingKeyFrames aniShowShieldVisibility = new ObjectAnimationUsingKeyFrames();
            aniShowShieldVisibility.KeyFrames.Add(new DiscreteObjectKeyFrame { KeyTime = TimeSpan.FromSeconds(0), Value = Visibility.Visible });
            Storyboard.SetTarget(aniShowShieldVisibility, imgEscudo);
            Storyboard.SetTargetProperty(aniShowShieldVisibility, "Visibility");
            sbEscudo.Children.Add(aniShowShieldVisibility);

            sbNoEscudo = new Storyboard();
            DoubleAnimation aniNoShieldOpacity = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            Storyboard.SetTarget(aniNoShieldOpacity, imgEscudo);
            Storyboard.SetTargetProperty(aniNoShieldOpacity, "Opacity");
            sbNoEscudo.Children.Add(aniNoShieldOpacity);

            sbNoEscudo.Completed += (s, e) =>
            {
                imgEscudo.Visibility = Visibility.Collapsed;
            };
        }

        private void DtTime_Tick(object sender, object e)
        {
            if (Energia <= 20 && !isCansadoActive)
            {
                animacionCansado();
                isCansadoActive = true;
            }
            else if (Energia > 20 && isCansadoActive)
            {
                animacionNoCansado();
                isCansadoActive = false;
            }

            if (Vida <= 40 && Vida > 0 && !isHeridoActive && !isDerrotadoActive)
            {
                animacionHerido();
                isHeridoActive = true;
            }
            else if ((Vida > 40 || Vida == 0) && isHeridoActive)
            {
                animacionNoHerido();
                isHeridoActive = false;
            }

            if (Vida <= 0 && !isDerrotadoActive)
            {
                sbIdle.Stop();
                if (isHeridoActive)
                {
                    animacionNoHerido();
                    isHeridoActive = false;
                }
                if (isCansadoActive)
                {
                    animacionNoCansado();
                    isCansadoActive = false;
                }

                animacionDerrota();
                isDerrotadoActive = true;
                dtTime.Stop();
            }
        }

        private void usePotionRed(object sender, PointerRoutedEventArgs e)
        {
            if (dtTime == null)
            {
                dtTime = new DispatcherTimer();
                dtTime.Interval = TimeSpan.FromMilliseconds(100);
            }
            dtTime.Tick -= increaseEnergy;
            dtTime.Tick += increaseHealth;
            dtTime.Start();
            this.imRPotion.Visibility = Visibility.Collapsed;
        }

        private void usePotionYellow(object sender, PointerRoutedEventArgs e)
        {
            if (dtTime == null)
            {
                dtTime = new DispatcherTimer();
                dtTime.Interval = TimeSpan.FromMilliseconds(100);
            }
            dtTime.Tick -= increaseHealth;
            dtTime.Tick += increaseEnergy;
            dtTime.Start();
            this.imYPotion.Visibility = Visibility.Collapsed;
        }

        private void increaseHealth(object sender, object e)
        {
            if (this.pbHealth.Value < 100)
            {
                this.pbHealth.Value += 0.5;
            }
            else
            {
                this.dtTime.Stop();
                this.dtTime.Tick -= increaseHealth;
            }
        }

        private void increaseEnergy(object sender, object e)
        {
            if (this.pbEnergy.Value < 100)
            {
                this.pbEnergy.Value += 0.5;
            }
            else
            {
                this.dtTime.Stop();
                this.dtTime.Tick -= increaseEnergy;
            }
        }

        public void verFondo(bool ver)
        {
            if (this.fondo != null)
            {
                this.fondo.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void verFilaVida(bool ver)
        {
            if (this.pbHealth != null)
            {
                this.pbHealth.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void verFilaEnergia(bool ver)
        {
            if (this.pbEnergy != null)
            {
                this.pbEnergy.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void verPocionVida(bool ver)
        {
            if (this.imRPotion != null)
            {
                this.imRPotion.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if (this.imYPotion != null)
            {
                this.imYPotion.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void verNombre(bool ver)
        {
            if (this.name != null)
            {
                this.name.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void verCorazon(bool ver)
        {
            if (!ver) { this.corazon.Visibility = Visibility.Collapsed; }
            else { this.corazon.Visibility = Visibility.Visible; }
        }
        public void verEscudo1(bool ver)
        {
            if (!ver) { this.energia.Visibility = Visibility.Collapsed; }
            else { this.energia.Visibility = Visibility.Visible; }
        }

        public void verEscudo(bool ver)
        {
            if (ver)
            {
                sbEscudo.Begin();
            }
            else
            {
                sbNoEscudo.Begin();
            }
        }

        public void activarAniIdle(bool activar)
        {
            if (activar)
            {
                dtTime.Start();
                sbIdle.Begin();
            }
            else
            {
                dtTime.Stop();
                sbIdle.Stop();
                if (translateTransformJigglypuff != null)
                {
                    translateTransformJigglypuff.Y = 0;
                }
            }
        }

        public void animacionAtaqueFlojo()
        {
            if (isDerrotadoActive)
            {
                return;
            }
            sbIdle.Stop();
            dtTime.Stop();
            imgAttackEffect.Visibility = Visibility.Visible;
            sbAttackWeak.Begin();
        }

        public void animacionAtaqueFuerte()
        {
            if (isDerrotadoActive)
            {
                return;
            }
            sbIdle.Stop();
            dtTime.Stop();
            imgAttackEffect.Visibility = Visibility.Visible;
            sbAttackStrong.Begin();
        }

        public void animacionDefensa()
        {
            if (isDerrotadoActive)
            {
                return;
            }
            sbIdle.Stop();
            dtTime.Stop();
            sbDefense.Begin();
        }

        public void animacionDescasar()
        {
            if (isDerrotadoActive)
            {
                return;
            }
            sbIdle.Stop();
            dtTime.Stop();
            sbRest.Begin();
        }

        public void animacionCansado()
        {
            sbCansado.Begin();
        }

        public void animacionNoCansado()
        {
            sbNoCansado.Begin();
        }

        public void animacionHerido()
        {
            sbHerido.Begin();
        }

        public void animacionNoHerido()
        {
            sbNoHerido.Begin();
        }

        public void animacionDerrota()
        {
            sbDefeat.Begin();
        }
    }
}