using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        // イベントハンドラのデリゲート、プロパティ値が変更されたことをクライアントに通知します
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// propertyNameには変更が行われたプロパティの名前が入る
        /// CallerMemberNameを使うことで、いちいちプロパティ名を指定しなくて済む
        /// 
        /// CallerMemberName 呼び出し元の名前
        /// CallerFilePath 呼び出し元ファイル名
        /// CallerLineNumber 呼び出し元行数
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
