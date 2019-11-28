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
        private bool isConnected = false;
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
                if(value != "") {
                    name = value;
                    OnPropertyChanged();
                }                    
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                if (value != "") {
                    message = value;
                    OnPropertyChanged();
                }
            }
        }
       
        public void messagecallback(string msg)
        {
            Messages.Add(msg);
        }    
        
        //Добавление команды для кнопки соединения
        private Uzing_command addCommand1;
        public Uzing_command AddCommand1
        {
            get
            {
                return addCommand1 ??
                  (addCommand1 = new Uzing_command(obj =>
                  {
                      
                      if (isConnected == true)
                      {                              
                              C.Disconnect(I);
                              C = null;
                              ISC = false;                          
                      }
                      else
                      {
                              C = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                              I = client.Connect(name);
                              ISC = true;                          
                      }
                  }));
            }
            
        }
        public bool ISC
        {
            get { return isConnected; }
            set { isConnected = value;
                OnPropertyChanged();
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
