using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Events
{
    public class GenericEvent :PubSubEvent<GenericMessage>
    {

    }

    public class TagListRefreshEvent : PubSubEvent<GenericMessage>
    {
    }
    public class GenericMessage
    {
        public string Message { get; set; }

        public GenericMessage(string msg)
        {
            Message = msg;
        }
    }
}
