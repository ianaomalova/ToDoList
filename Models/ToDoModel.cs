using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksList.Models
{
    //при изменении уже существующего объекта чтобы вызывалось событие, нужно наследоваться от класса и реализовать его
    class ToDoModel:INotifyPropertyChanged
    {
        private bool _isDone;
        public bool IsDone
        {
            get { return _isDone; }
            set 
            {
                if(_isDone == value)
                {
                    return;
                }
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        private string _text;       
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            //проверка на null в случае если уведомлять о событии некого
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
