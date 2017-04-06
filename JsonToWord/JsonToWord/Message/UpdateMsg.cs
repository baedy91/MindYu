using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonToWord.Model;
using GalaSoft.MvvmLight.Messaging;

namespace JsonToWord.Message
{
    class UpdateMsg: NotificationMessageAction<RootAllModel>
    {
        public int index { get; set; }
        public string updateData { get; set; }
        public string oldData { get; set; }

        public UpdateMsg(string notification, Action<RootAllModel> callback) 
            : base(notification, callback)
        {         
        }
    }
}
