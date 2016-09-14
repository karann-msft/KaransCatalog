using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp;
using Windows.ApplicationModel.Appointments;
using System.Linq;

using KaransCatalog.Navigation;
using KaransCatalog.ViewModels;

namespace KaransCatalog.Sections
{
    public class LaptopsSection : Section<Laptops1Schema>
    {
		private LocalStorageDataProvider<Laptops1Schema> _dataProvider;

		public LaptopsSection()
		{
			_dataProvider = new LocalStorageDataProvider<Laptops1Schema>();
		}

		public override async Task<IEnumerable<Laptops1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Laptops.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Laptops1Schema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<Laptops1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Laptops1Schema>
                {
                    Title = "Laptops",

                    Page = typeof(Pages.LaptopsListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.LaptopsDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Laptops1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Laptops1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Laptops1Schema>>
                {
                };

                return new DetailPageConfig<Laptops1Schema>
                {
                    Title = "Laptops",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
