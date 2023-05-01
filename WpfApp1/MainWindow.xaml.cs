using System;
using System.Collections.Generic;
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
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // DataContextを設定、xamlで設定しても良い
            DataContext = new MainWindowViewModel();

            ((MainWindowViewModel)DataContext).Input = "init";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 直接画面を更新するのではなく、ViewModelを更新する
            // ViewModel -> View へ変更通知を飛ばす
            // View -> ViewModelはxamlでBinding （UpdateSourceTriggerでタイミングを設定。フォーカス、プロパティ変更など）

            // プロパティを更新すると、PropertyChangedが発生する
            ((MainWindowViewModel)DataContext).Input = "Click!";
            //var dt = DataContext as MainWindowViewModel;
            //dt.Input = "Click!";

        }
    }
}
