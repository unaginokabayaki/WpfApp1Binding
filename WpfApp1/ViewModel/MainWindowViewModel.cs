using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Command;

namespace WpfApp1.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public void OnPropertyChanged3()
        {
            OnPropertyChanged("FirstValue");
            OnPropertyChanged("SecondValue");
            OnPropertyChanged("AnswerValue");
        }

        private string _input;
        /// <summary>
        /// xamlのTextBoxにバインド
        /// </summary>
        public string Input {
            get { return _input; }
            set 
            { 
                if (_input != value)
                {
                    _input = value;
                    // プロパティの変更を画面へ通知
                    OnPropertyChanged();
                    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input"));
                }
            }
            
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            // 第一引数 ボタンで実行する内容、Action型
            // 第二引数 ボタンが動作可能か、boolを返すメソッド
            ButtonCommand = new DelegateCommand(async () =>
            {
                // Flagを切り替えて、ボタンの状態を変える
                Flag = false;
                await Task.Delay(3000);
                Flag = true;
            },
            () => Flag);
            //canExecuteCommand);

            // 第一引数 ボタンで実行する内容、Action型
            // 第二引数 ボタンが動作可能か、boolを返すメソッド
            ShowCommand = new DelegateCommand(() =>
            {
                // テキストボックスの内容をを表示
                MessageBox.Show($"{ShowText}");
            },
            () =>
            {
                // テキストに入力があれば有効化
                return ShowText.Length > 0;
            });


            CalcCommand = new DelegateCommand<string>((operation) =>
            {
                PaformCalc(operation);
            });

        }

        private bool canExecuteCommand()
        {
            return Flag;
        }

        private void PaformCalc(string operation)
        {
            if (operation != null)
            {
                _operationCurrent = operation;
            }

            if (_operationCurrent == "+")
            {
                Operation = "足す";
                AnswerValue = FirstValue + SecondValue;
            }
            else if (_operationCurrent == "-")
            {
                Operation = "引く";
                AnswerValue = FirstValue - SecondValue;
            }
        }

        /// <summary>
        /// xamlのButtonにバインド
        /// </summary>
        public DelegateCommand ButtonCommand { get; }

        /// <summary>
        /// 実行可能かどうか
        /// </summary>
        private bool _flag = true;
        public bool Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                // 通知する
                // CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                ButtonCommand.DelegateCanExecute();
            }
        }

        /// <summary>
        /// xamlのテキストにバインド
        /// </summary>
        public DelegateCommand ShowCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        private string _showText = "";
        public string ShowText
        {
            get { return _showText; }
            set
            {
                _showText = value;
                ShowCommand.DelegateCanExecute();
            }
        }


        private double _firstValue;
        public double FirstValue
        {
            get { return _firstValue; } 
            set 
            { 
                _firstValue = value;
                PaformCalc(null);
            }
        }

        private double _secondValue;
        public double SecondValue
        {
            get { return _secondValue; }
            set 
            { 
                _secondValue = value;
                PaformCalc(null);
            }
        }

        private double _answerValue;
        public double AnswerValue
        {
            get { return _answerValue; }
            set
            {
                _answerValue = value;
                OnPropertyChanged();
            }
        }

        private string _operationCurrent;

        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set { 
                _operation = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<string> CalcCommand { get; }
 
    }
}
