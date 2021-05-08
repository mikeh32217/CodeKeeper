using CodeKeeper.Commands;
using CodeKeeper.Events;
using CodeKeeper.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace CodeKeeper.UserControls
{
    /// <summary>
    /// Interaction logic for TagListViewControl.xaml
    /// </summary>
    public partial class TagListViewControl : UserControl
    {
        public bool DisplaySnippet
        {
            get { return (bool)GetValue(DisplaySnippetProperty); }
            set { SetValue(DisplaySnippetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplaySnippet.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplaySnippetProperty =
            DependencyProperty.Register("DisplaySnippet", typeof(bool), typeof(TagListViewControl), new PropertyMetadata(true));

        public DataView ItemView { get; set; }

        public DataRowView CurrentItem { get; set; }

        public UC_FilterTextChangedCommand UC_FilterTextChangedCommand { get; set; }
        public TagInfoSelectionChangedCommand TagInfoSelectionChangedCommand { get; set; }

        public TagListViewControl()
        {
            InitializeComponent();

            UC_FilterTextChangedCommand = new UC_FilterTextChangedCommand();
            TagInfoSelectionChangedCommand = new TagInfoSelectionChangedCommand();

            // NOTE Received from the UC_FilterTextChangedCommand command
            App.g_eventAggregator.GetEvent<TagListRefreshEvent>().Subscribe(FilterList);

            this.Dispatcher.BeginInvoke((Action)(() => { UpdateList(); }));
        }

        private void FilterList(GenericMessage msg)
        {
            ItemView.RowFilter = "Tag LIKE '%" + msg.Message + "%'";
        }

        private void UpdateList()
        {
            // NOTE I needed to put the if statement here because binding
            //  wasn't complete so DisplaySnippet was always false.
            if (DisplaySnippet)
                ItemView = MasterRepository._Snippet.GetAllAsView();
            else
                ItemView = MasterRepository._Token.GetAllAsView();

            // NOTE Had to put this here because although the ItemView was
            //  being set the DataContext was set before binding was complete
            //  therefore the output didn't show a binding error but there
            //  was no binding being done.
            DataContext = this;
        }
    }
}
