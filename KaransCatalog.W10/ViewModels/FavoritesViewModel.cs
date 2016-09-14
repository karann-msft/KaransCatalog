using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Input;
using KaransCatalog.Services;
using KaransCatalog.Sections;
namespace KaransCatalog.ViewModels
{
    public class FavoritesViewModel : PageViewModelBase
    {
        public ListViewModel Devices { get; private set; }
        public ListViewModel Smartphones { get; private set; }
        public ListViewModel Laptops { get; private set; }
        public ListViewModel Xbox { get; private set; }

        public FavoritesViewModel()
        {
			Title = "My devices";

            Devices = ViewModelFactory.NewList(new DevicesSection());
            Smartphones = ViewModelFactory.NewList(new SmartphonesSection());
            Laptops = ViewModelFactory.NewList(new LaptopsSection());
            Xbox = ViewModelFactory.NewList(new XboxSection());

			ShowRoamingWarning = Singleton<UserFavorites>.Instance.RoamingQuota == 0;                       
        }     

		#region	ShowRoamingWarning
        private bool _showRoamingWarning;

        public bool ShowRoamingWarning
        {
            get { return _showRoamingWarning; }
            set { SetProperty(ref _showRoamingWarning, value); }
        }
		#endregion

		#region	HasItems
		private bool _hasItems = true;

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            this.HasItems = true;
            List<Task> loadDataTasks = new List<Task>();

            if (Singleton<UserFavorites>.Instance.Sections != null)
            {
                foreach (var favInSection in Singleton<UserFavorites>.Instance.Sections)
                {
                    var vm = GetSectionViewModel(favInSection.Name);

                    if (vm != null)
                    {
                        loadDataTasks.Add(vm.FilterDataAsync(favInSection.ItemsId));
                    }
                } 
            }

            await Task.WhenAll(loadDataTasks);
            this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Devices;
            yield return Smartphones;
            yield return Laptops;
            yield return Xbox;
        }

		private ListViewModel GetSectionViewModel(string sectionName)
        {
            return GetViewModels().FirstOrDefault(vm => vm.SectionName == sectionName);
        }
    }
}
