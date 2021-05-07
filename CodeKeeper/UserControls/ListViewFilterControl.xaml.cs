using CodeKeeper.Commands;
using CodeKeeper.Events;
using CodeKeeper.Model;
using CodeKeeper.Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CodeKeeper.UserControls
{
    /// <summary>
    /// Interaction logic for ListViewFilterControl.xaml
    /// </summary>
    public partial class ListViewFilterControl : UserControl
    {
        public FilterTextChangedCommand FilterTextChangedCommand { get; set; }

        public string RawContent
        {
            get { return (string)GetValue(RawContentProperty); }
            set { SetValue(RawContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RawContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RawContentProperty =
            DependencyProperty.Register("RawContent", typeof(string), typeof(ListViewFilterControl), new PropertyMetadata(string.Empty));


        public ObservableCollection<TagInfo> TagInfoList
        {
            get { return (ObservableCollection<TagInfo>)GetValue(TagInfoListProperty); }
            set { SetValue(TagInfoListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TagInfoList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TagInfoListProperty =
            DependencyProperty.Register("TagInfoList", typeof(ObservableCollection<TagInfo>), typeof(ListViewFilterControl));


        public ListViewFilterControl()
        {
            InitializeComponent();

            FilterTextChangedCommand = new FilterTextChangedCommand(this);

            // NOTE Do this becuase binding not done until after render
            this.Dispatcher.BeginInvoke((Action)(() => { RefreshTagList(); }));

            App.g_eventAggregator.GetEvent<TagRefreshEvent>().Subscribe(RefreshTagList);

            DataContext = this;
        }

        public void RefreshTagList(UpdateMessage state = null)
        {
            // At first go this will be null...duh!
            if (TagInfoList != null)
                TagInfoList.Clear();

            TagInfoList = Utilities.ParseUtils.GetTagInfo(RawContent);
            foreach (TagInfo ti in TagInfoList)
                GetValidTagInfo(ti);
        }

        private static TagInfo GetValidTagInfo(TagInfo ti)
        {
            bool fnd = false;

            if (ti.TagType == TagInfo.TokenType.Snippet)
            {
                // Look in the Snippet list
                MasterRepository._Snippet.GetSnippetByTag(ti.LinkTargetInnerText.Substring(1));
                if (MasterRepository._Snippet.WorkingView.Count > 0)
                {
                    ti.TagType = TagInfo.TokenType.Snippet;
                    ti.IsValid = true;
                    fnd = true;
                }
            }
            else
            {
                MasterRepository._Token.GetTokenByTag(ti.LinkTargetInnerText);
                if (MasterRepository._Token.WorkingView.Count > 0)
                {
                    ti.TagType = TagInfo.TokenType.Token;
                    ti.IsValid = true;
                    fnd = true;
                }
            }

            // If not found in either place it is undefined.
            if (!fnd)
            {
                ti.TagType = TagInfo.TokenType.Undefined;
                ti.IsValid = false;
            }

            return ti;
        }
    }
}
