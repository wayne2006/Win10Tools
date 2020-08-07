using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using Win10Tools.Data;
using Win10Tools.ViewModel;

namespace Win10Tools.UserControl
{
    /// <summary>
    /// LeftMainContent.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMainContent
    {
        public LeftMainContent()
        {
            InitializeComponent();
        }

        private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            if (e.AddedItems[0] is DemoInfoModel demoInfo)
            {
                ViewModelLocator.Instance.Main.DemoInfoCurrent = demoInfo;
                var selectedIndex = demoInfo.SelectedIndex;
                demoInfo.SelectedIndex = -1;
                demoInfo.SelectedIndex = selectedIndex;
            }
        }

        private void SearchBar_OnSearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            if (e.Info == null) return;
            if (!(sender is FrameworkElement searchBar && searchBar.Tag is ListBox listBox)) return;
            foreach (DemoItemModel item in listBox.Items)
            {
                var listBoxItem = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                listBoxItem?.Show(item.Name.ToLower().Contains(e.Info.ToLower()) ||
                                  item.TargetCtlName.Replace("DemoCtl", "").ToLower().Contains(e.Info.ToLower()));
            }
        }

        private void ButtonAscending_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button && button.Tag is ItemsControl itemsControl)
            {
                if (button.IsChecked == true)
                {
                    itemsControl.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                }
                else
                {
                    itemsControl.Items.SortDescriptions.Clear();
                }
            }
        }
    }
}
