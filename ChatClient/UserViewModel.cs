using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChatClient.ServiceChat;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace ChatClient
{
    class UserViewModel : INotifyPropertyChanged, IServiceChatCallback
    {
        private bool isConnected = true;
        private ServiceChatClient client;
        private int ID;

        public ObservableCollection<string> Messages { get; set; }
        private string name;
        private string message;
        public UserViewModel()
        {
            Messages = new ObservableCollection<string>();
        }       
        public ServiceChatClient C
        {
            get { return client; }
            set { client = value; }
        }
        public int I
        {
            get { return ID; }
            set { ID = value; }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Select_equation");
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Select_equation");
            }
        }
       
        public void messagecallback(string msg)
        {
            Messages.Add(msg);
        }    
        
        //Добавление команды кнопки
        private Uzing_command addCommand1;
        public Uzing_command AddCommand1
        {
            get
            {
                return addCommand1 ??
                  (addCommand1 = new Uzing_command(obj =>
                  {
                      
                      if (isConnected == false)
                      {                              
                              C.Disconnect(I);
                              C = null;
                              ISC = true;                          
                      }
                      else
                      {
                              C = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                              I = client.Connect(name);
                              ISC = false;                          
                      }
                  }));
            }
            
        }
        public bool ISC
        {
            get { return isConnected; }
            set { isConnected = value;
                OnPropertyChanged("Select_equation");
            }
        }
        //добавление команды текстбоксом
        private Uzing_command addCommand2;
        public Uzing_command AddCommand2
        {
            get
            {
                return addCommand2 ??
                  (addCommand2 = new Uzing_command(obj =>
                  {
                      
                      if (C != null)
                      {
                          C.SendMessage(message, I);
                      }
                  }));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged; 
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
