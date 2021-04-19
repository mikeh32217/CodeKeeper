using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CodeKeeper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Put globals here!
        public static EventAggregator g_eventAggregator = new EventAggregator();
    }
}
