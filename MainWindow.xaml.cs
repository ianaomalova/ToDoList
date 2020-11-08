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

namespace TasksList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}
