using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary1
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> Users = new List<ServerUser>();
        int NextID = 1;
        public int Connect(string UserName)
        {
            ServerUser User = new ServerUser()
            {
                Id = NextID,
                Name = UserName,
                operationContext = OperationContext.Current
            };
            NextID++;
            SendMessage(User.Name + " добро пожаловать!",0);
            Users.Add(User);
            return User.Id;
        }

        public void Disconnect(int id)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                Users.Remove(user);
                SendMessage(user.Name + " До встречи!",0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in Users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    answer += ": " + user.Name+" ";
                }
                answer += message;

                item.operationContext.GetCallbackChannel<IServerChatCallBack>().messagecallback(answer);
            }
        }
    }
}
