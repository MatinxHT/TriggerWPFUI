using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriggerWPFUI.MVVM.Views;
using TriggerWPFUI.MVVM.Views.Pages;
using Wpf.Ui.Controls;

namespace TriggerWPFUI.MVVM.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
        }

        [ObservableProperty]
        private string _appcationTitle = "Trigger Demo of WPF-UI";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = [
            new NavigationViewItem("Home", SymbolRegular.Home12,typeof(BlankPage)),
            new NavigationViewItemSeparator(),
            new NavigationViewItem()
            {
                Content = "CollectionA",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Radar20 },
                MenuItemsSource = new object[]
                {
                    new NavigationViewItem("ItemA", SymbolRegular.TextFont24, typeof(BlankPage)),
                    new NavigationViewItem("ItemB", SymbolRegular.Color24, typeof(BlankPage))
                },
                TargetPageType = typeof(BlankPage)
            }
            ];

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems =
            [
            new NavigationViewItem("Settings and About", SymbolRegular.Settings24, typeof(BlankPage))
            ];
    }
}
