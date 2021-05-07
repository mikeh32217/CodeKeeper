using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Events
{
    public class TagRefreshEvent :  PubSubEvent<UpdateMessage>
    {
    }

    public class UpdateMessage
    {
        public bool UpdateState { get; set; }

        public UpdateMessage(bool state)
        {
            UpdateState = state;
        }
    }
}
