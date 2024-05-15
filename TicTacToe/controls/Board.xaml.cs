using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using TicTacToe.Enums;

namespace TicTacToe.controls;

/// <summary>
/// Interaction logic for Board.xaml
/// </summary>
public partial class Board : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    
    public EventHandler<GameEndEventArgs> GameEnded;

    private const string PlayerOneContent= "X";
    private const string PlayerTwoContent= "0";


    private readonly Button[,] _buttons = new Button[3, 3];

    private readonly Random _rnd = new Random();
    bool _isPlayerOneTurn = true;
    private bool _gameIsActive = true;
    private GameType _gameType = GameType.PvP;

    

    public Board()
    {
        InitializeComponent();
        InitlalizeGameGrid();

        DataContext = this;
    }
    private void OnPropertyChanged(string name)
    {
        if (PropertyChanged == null)
        {
            return;
        }
        PropertyChanged(this, new PropertyChangedEventArgs(name));
    }

    private void OnGameEnd(GameResult result)
    {
        GameEnded?.Invoke(this, new  GameEndEventArgs(result));
    }

    public bool IsPlayerOneTurn { 
        get => _isPlayerOneTurn; 
        set { 
            _isPlayerOneTurn = value; 
            OnPropertyChanged(nameof(IsPlayerOneTurn));
            OnPropertyChanged(nameof(CurrentPlayerTurn));
        } 
    }
    public string CurrentPlayerTurn => IsPlayerOneTurn ? $"Player 1 = {PlayerOneContent} " : $"Player 2 = {PlayerTwoContent}";



    public GameType CurrentGameType { get
        { return _gameType; } 
        set 
        { _gameType = value;
        OnPropertyChanged(nameof(CurrentGameType));
                } 
    }
    private void InitlalizeGameGrid()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Button btn = new Button()
                {
                    FontSize = 40,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                   
                };
                btn.Click += Button_Click;

                Grid.SetRow(btn, i);
                Grid.SetColumn(btn, j);

                _buttons[i, j] = btn;

                GameGrid.Children.Add(btn);
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e){
    if (!_gameIsActive || (CurrentGameType == GameType.PvC && !IsPlayerOneTurn) || CurrentGameType == GameType.CvC)
        {
            return;
        }
        Button btn = sender as Button;
        if(btn == null)
        {
            return;
        }
        if (btn.Content == null)
        {
            btn.Content = IsPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;
            if (ProcessEndGame())
            {
                return;
            }
            IsPlayerOneTurn = !IsPlayerOneTurn;

            if(CurrentGameType == GameType.PvC && !IsPlayerOneTurn)
            {
                ComputerMove();
            }


        }

    }

    private void ComputerMove(){
        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(_rnd.Next(10) / 10.0)
        };
        timer.Tick += (sender, e) =>
        {
            timer.Stop();
                Button btn;
                do
                {
                    int row = _rnd.Next(3);
                    int col = _rnd.Next(3);
                    btn = _buttons[row, col];

                } while (btn.Content != null);
           
                btn.Content = IsPlayerOneTurn ? PlayerOneContent : PlayerTwoContent;
                if (ProcessEndGame())
                {
                    return;
                }
            IsPlayerOneTurn = !IsPlayerOneTurn;
            if(
            CurrentGameType == GameType.CvC && !IsBoardFull())
            {
                ComputerMove();
            }
        };
        timer.Start();
      
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private bool ProcessEndGame()
    {
        bool win = CheckForWineer();
        if (CheckForWineer()){
        
            GameResult result = IsPlayerOneTurn ? GameResult.PlayerOneWins: GameResult.PlayerTwoWins;

            //MessageBox.Show(result.ToString());
            OnGameEnd(result);
           _gameIsActive = false;
            return true;

        } if (IsBoardFull())
        {
            GameResult result = GameResult.Draw;
            //MessageBox.Show(result.ToString());
            OnGameEnd(result);

           _gameIsActive = false;
            return true;
        }
        return false;
    }
   

    internal void StartNewGame(GameType gameType)
    {
        if(_gameIsActive) { return;
        }

        CurrentGameType = gameType;
        IsPlayerOneTurn = true;
        _gameIsActive = true;

        foreach(Button btn in _buttons)
        {
            btn.Content = null;
        }
        if (CurrentGameType == GameType.CvC && !IsBoardFull())
        {
            ComputerMove();
        }

    }

    

    private bool IsBoardFull()
    {
        foreach(Button button in _buttons)
        {
            if(button.Content == null)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckForWineer()
    {

        for (int i = 0; i < 3; i++)
        {

            if (_AreButtonsEqual(_buttons[i, 0], _buttons[i, 1], _buttons[i, 2]))
            {

                return true;
            }

            if (_AreButtonsEqual(_buttons[0, i], _buttons[1, i], _buttons[2, i]))
            {

                return true;
            }
        }
        if (_AreButtonsEqual(_buttons[0, 0], _buttons[1, 1], _buttons[2, 2]))
        {
            return true;
        }
        if (_AreButtonsEqual(_buttons[0, 2], _buttons[1, 1], _buttons[2, 0]))
        {
            return true;
        }
        return false;
    }


    private bool _AreButtonsEqual(Button b1, Button b2, Button b3)
    {
       return b1.Content != null && b1.Content == b2.Content && b2.Content == b3.Content;
    }
}
