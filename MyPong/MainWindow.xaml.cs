using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double paddleSpeed = 10;
        private Rectangle paddle1;
        private Rectangle paddle2;
        private Rectangle ball;
        private DispatcherTimer gametimer;

        public MainWindow()
        {
            InitializeComponent();


            paddle1 =  CreatePlayerRectangle();
             paddle2 = CreatePlayerRectangle();
            ball = new Rectangle
            {
                Width =15,
                Height =15,
                Fill = Brushes.Yellow
            };
            GameCanvas.Children.Add(paddle1);
            GameCanvas.Children.Add(paddle2);
            GameCanvas.Children.Add(ball);
            Loaded += new RoutedEventHandler(WindowLoaded);
            SizeChanged += new SizeChangedEventHandler(HandleWindowsSizeChanged);
            KeyDown += new KeyEventHandler(HandleKeyDown);

            gametimer = new DispatcherTimer();
            gametimer.Interval = TimeSpan.FromMilliseconds(16);
            gametimer.Tick += new EventHandler(GameLoop);
            gametimer.Start();


            }

        private void GameLoop(object? sender, EventArgs e)
        {
           Canvas.SetLeft(ball, Canvas.GetLeft(ball)+ 1 );
            Canvas.SetTop(ball, Canvas.GetTop(ball) + 1);
        }

        private void HandleWindowsSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetIntialPosition();
        }

        private void SetIntialPosition()
        {
            Canvas.SetLeft(paddle1, 50);
            Canvas.SetTop(paddle1, (GameCanvas.ActualHeight / 2) - (paddle1.ActualHeight));


            Canvas.SetLeft(paddle2, GameCanvas.ActualWidth - 50);
            Canvas.SetTop(paddle2, (GameCanvas.ActualHeight / 2) - (paddle2.ActualHeight));

            Canvas.SetLeft(ball, (GameCanvas.ActualWidth - ball.ActualWidth) / 2);
            Canvas.SetTop(ball, (GameCanvas.ActualHeight - ball.ActualHeight) / 2);

        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
             
            


            SetIntialPosition();

            
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            
            double p1Top = Canvas.GetTop(paddle1);
            double p2Top = Canvas.GetTop(paddle2);
            if (e.Key == Key.W && p1Top > 0) 
            {
                Canvas.SetTop(paddle1,p1Top - paddleSpeed );
            }
            if (e.Key == Key.S && p1Top < (GameCanvas.ActualHeight - paddle1.ActualHeight))
            {
                Canvas.SetTop (paddle1, p1Top + paddleSpeed);
            }
            if (e.Key == Key.Up && p2Top > 0)
            {
                Canvas.SetTop(paddle2, p2Top - paddleSpeed);
            }
            if (e.Key == Key.Down && p2Top < (GameCanvas.ActualHeight - paddle2.ActualHeight))
            {
                Canvas.SetTop(paddle2, p2Top + paddleSpeed);
            }

        }

        private Rectangle CreatePlayerRectangle()
        {
           return new Rectangle()
            {
                Width = 10,
                Height = 100,
                Fill = Brushes.White,

            };
        }
    }
}