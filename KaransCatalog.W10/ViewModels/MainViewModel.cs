using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.Rss;
using AppStudio.DataProviders.LocalStorage;
using KaransCatalog.Sections;


namespace KaransCatalog.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel Devices { get; private set; }
        public ListViewModel Smartphones { get; private set; }
        public ListViewModel Laptops { get; private set; }
        public ListViewModel Xbox { get; private set; }
        public ListViewModel DevicesNews { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Karan's Catalog";
            Devices = ViewModelFactory.NewList(new DevicesSection(), visibleItems);
            Smartphones = ViewModelFactory.NewList(new SmartphonesSection(), visibleItems);
            Laptops = ViewModelFactory.NewList(new LaptopsSection(), visibleItems);
            Xbox = ViewModelFactory.NewList(new XboxSection(), visibleItems);
            DevicesNews = ViewModelFactory.NewList(new DevicesNewsSection(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Devices;
            yield return Smartphones;
            yield return Laptops;
            yield return Xbox;
            yield return DevicesNews;
        }
    }
}
