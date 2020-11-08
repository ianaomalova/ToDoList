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
using TasksList.Models;
using TasksList.Services;
using System.Windows.Threading;
using System.Media;

namespace TasksList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int MaxTimerCounterWork;
        private int MaxTimerCounterRelax;
        const int MinTimerCounter = -1;
        DispatcherTimer _dispTimer1;
        DispatcherTimer _dispTimer2;
        int _dispTimerCounterWork;
        int _dispTimerCounterRelax;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        private BindingList<ToDoModel> _toDo;
        private FileIOService _fileIOService;
        public MainWindow()
        {
            InitializeComponent();
        }

        //список хранит тип була и строки. привязка к датагриду
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);
            try
            {
                _toDo = _fileIOService.LoadData();
            } 

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
           
            Tasks.ItemsSource = _toDo;
            _toDo.ListChanged += _toDo_ListChanged;
        }
        //это событие вызывается при изменениях в дата гриде, а значит и в самом списке, так как мы на него подписаны
        private void _toDo_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }

        private void TextBox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // если пробел, отклоняем ввод
            }
        }

        private void TextBox_PreviewKeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // если пробел, отклоняем ввод
            }
        }

        void dispTimer_Tick1(object sender, EventArgs e)
        {
            //сначала выводится значение, потом инкрементируется (не для всех очевидно)
            Timer1.Text = (_dispTimerCounterWork--).ToString();
            if (_dispTimerCounterWork == MinTimerCounter)
            {
                _dispTimer1.Stop();
                SystemSounds.Beep.Play();
            }

        }
        private void StartWork(object sender, RoutedEventArgs e)
        {
            MaxTimerCounterWork = Convert.ToInt32(TextBox1.Text);
            _dispTimerCounterWork = MaxTimerCounterWork;
            _dispTimer1 = new DispatcherTimer();
            _dispTimer1.Interval = TimeSpan.FromSeconds(1);
            _dispTimer1.Tick += dispTimer_Tick1;
            _dispTimer1.Start();
        }

        private void StopWork(object sender, RoutedEventArgs e)
        {
            _dispTimer1.Stop();
        }

        private void RestartButtonWork(object sender, RoutedEventArgs e)
        {
            _dispTimerCounterWork = MaxTimerCounterWork;
            Timer1.Text = (_dispTimerCounterWork--).ToString();
            _dispTimer1.Stop();
        }

        void dispTimer_Tick2(object sender, EventArgs e)
        {
            //сначала выводится значение, потом инкрементируется (не для всех очевидно)
            Timer2.Text = (_dispTimerCounterRelax--).ToString();
            if (_dispTimerCounterRelax == MinTimerCounter)
            {
                _dispTimer2.Stop();
                SystemSounds.Beep.Play();
            }

        }

        private void StartRlx(object sender, RoutedEventArgs e)
        {
            MaxTimerCounterRelax = Convert.ToInt32(Relax.Text);
            _dispTimerCounterRelax = MaxTimerCounterRelax;
            _dispTimer2 = new DispatcherTimer();
            _dispTimer2.Interval = TimeSpan.FromSeconds(1);
            _dispTimer2.Tick += dispTimer_Tick2;
            _dispTimer2.Start();
        }

        private void StopRlx(object sender, RoutedEventArgs e)
        {
            _dispTimer2.Stop();
        }
    }
}
