using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Command
{
    internal class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// ボタンの動作
        /// </summary>
        /// <param name="action">ボタンで実行する内容、Action型のメソッド</param>
        /// <param name="canExecute">ボタンが動作可能か、boolを返すメソッド</param>
        public DelegateCommand(Action action, Func<bool> canExecute = default)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 有効無効の判定
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        /// <summary>
        /// コマンドで実行する内容
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        /// <summary>
        /// 変更通知。イベントを発生させる。
        /// EventArgs.Empty イベント データのないイベントに使用 
        /// </summary>
        public void DelegateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }

    internal class DelegateCommand<T> : ICommand
    {
       
        public event EventHandler CanExecuteChanged;

        private readonly Action<T> _action;
        private readonly Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> action, Func<T, bool> canExecute = default) 
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke((T)parameter);
        }

        public void DelegateCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
