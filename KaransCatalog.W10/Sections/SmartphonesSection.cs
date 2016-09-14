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
    public class SmartphonesSection : Section<Smartphones1Schema>
    {
		private LocalStorageDataProvider<Smartphones1Schema> _dataProvider;

		public SmartphonesSection()
		{
			_dataProvider = new LocalStorageDataProvider<Smartphones1Schema>();
		}

		public override async Task<IEnumerable<Smartphones1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Smartphones.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Smartphones1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<Smartphones1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Smartphones1Schema>
                {
                    Title = "Smartphones",

                    Page = typeof(Pages.SmartphonesListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.SmartphonesDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Smartphones1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Smartphones1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Smartphones1Schema>>
                {
                };

                return new DetailPageConfig<Smartphones1Schema>
                {
                    Title = "Smartphones",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
