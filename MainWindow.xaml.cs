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
        TimeSpan _time1;
        TimeSpan _time2;
        int _dispTimerCounterWork;
        //int _dispTimerCounterRelax;
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

            catch (Exception ex)
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
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
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


        private void StartWork(object sender, RoutedEventArgs e)
        {
            GoodJob.Opacity = 0;
            Good.Opacity = 0;
            GoodJ.Opacity = 0;
            RectangleGoodJob.Opacity = 0;

            MaxTimerCounterWork = Convert.ToInt32(TextBox1.Text);
            _time1 = TimeSpan.FromMinutes(MaxTimerCounterWork);

            _dispTimer1 = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Timer1.Text = _time1.ToString("c");
                if (_time1 == TimeSpan.Zero)
                {
                    _dispTimer1.Stop();
                    GoodJob.Opacity = 1;
                    Good.Opacity = 1;
                    GoodJ.Opacity = 1;
                    RectangleGoodJob.Opacity = 1;
                    SystemSounds.Asterisk.Play();
                }
                _time1 = _time1.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

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


        private void StartRlx(object sender, RoutedEventArgs e)
        {
            RectangleTimetoWork.Opacity = 0;
            TimeWork.Opacity = 0;
            TimetoWork.Opacity = 0;
            Book.Opacity = 0;

            MaxTimerCounterRelax = Convert.ToInt32(Relax.Text);
            _time2 = TimeSpan.FromMinutes(MaxTimerCounterRelax);

            _dispTimer2 = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Timer2.Text = _time2.ToString("c");
                if (_time2 == TimeSpan.Zero)
                {
                    _dispTimer2.Stop();
                    RectangleTimetoWork.Opacity = 1;
                    TimeWork.Opacity = 1;
                    TimetoWork.Opacity = 1;
                    Book.Opacity = 1;
                    SystemSounds.Asterisk.Play();
                }
                _time2 = _time2.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _dispTimer2.Start();
        }

        private void StopRlx(object sender, RoutedEventArgs e)
        {
            _dispTimer2.Stop();
        }
    }
}
