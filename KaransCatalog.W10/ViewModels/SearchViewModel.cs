using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KaransCatalog.Sections;
namespace KaransCatalog.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Karan's Catalog";
            Devices = ViewModelFactory.NewList(new DevicesSection());
            Smartphones = ViewModelFactory.NewList(new SmartphonesSection());
            Laptops = ViewModelFactory.NewList(new LaptopsSection());
            Xbox = ViewModelFactory.NewList(new XboxSection());
            ReviewsAndNews = ViewModelFactory.NewList(new ReviewsAndNewsSection());
            DevicesNews = ViewModelFactory.NewList(new DevicesNewsSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Devices { get; private set; }
        public ListViewModel Smartphones { get; private set; }
        public ListViewModel Laptops { get; private set; }
        public ListViewModel Xbox { get; private set; }
        public ListViewModel ReviewsAndNews { get; private set; }
        public ListViewModel DevicesNews { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Devices;
            yield return Smartphones;
            yield return Laptops;
            yield return Xbox;
            yield return ReviewsAndNews;
            yield return DevicesNews;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
