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
    internal class MainWindowViewModel : INotifyPropertyChanged
    //internal class MainWindowViewModel : ViewModelBase
    {
        // イベントハンドラのデリゲート、プロパティ値が変更されたことをクライアントに通知します
        public event PropertyChangedEventHandler PropertyChanged;

        // CallerMemberNameを使うことで、いちいちプロパティ名を指定しなくて済む
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // PropertyChangedを実行する、スレッドセーフなのでInvoke推奨か
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //PropertyChanged?.BeginInvoke(this, new PropertyChangedEventArgs(propertyName), new AsyncCallback((ar) => { }) , null);
            //if (PropertyChanged != null) 
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

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


            //CalcCommand = new DelegateCommand(() =>
            //{
            //    PaformCalc();
            //});

            PlusCommand = new DelegateCommand(() =>
            {
                PaformCalc("+");
            });

            MinusCommand = new DelegateCommand(() =>
            {
                PaformCalc("-");
            });

            PlusChecked = true;
        }

        private bool canExecuteCommand()
        {
            return Flag;
        }

        private void PaformCalc(string operation)
        {
            if (operation == "+")
            {
                Operation = "+";
                AnswerValue = FirstValue + SecondValue;
            }
            else if (operation == "-")
            {
                Operation = "-";
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
                PaformCalc(Operation);
                OnPropertyChanged3();
            }
        }

        private double _secondValue;
        public double SecondValue
        {
            get { return _secondValue; }
            set 
            { 
                _secondValue = value;
                PaformCalc(Operation);
                OnPropertyChanged3();
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

        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set { 
                _operation = value;
                OnPropertyChanged();
            }
        }

        private bool _plusChecked;
        public bool PlusChecked
        {
            get { return _plusChecked; } 
            set { 
                _plusChecked = value;
                Operation = "+";
            }
        }

        private bool _minusChecked;
        public bool MinusChecked
        {
            get { return _minusChecked; }
            set { 
                _minusChecked = value;
                Operation = "-";
            }
        }

        public DelegateCommand CalcCommand { get; }
        public DelegateCommand PlusCommand { get; }
        public DelegateCommand MinusCommand {  get; }
    }
}
