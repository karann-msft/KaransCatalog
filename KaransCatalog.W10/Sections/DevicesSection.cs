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
    public class DevicesSection : Section<Devices1Schema>
    {
		private LocalStorageDataProvider<Devices1Schema> _dataProvider;

		public DevicesSection()
		{
			_dataProvider = new LocalStorageDataProvider<Devices1Schema>();
		}

		public override async Task<IEnumerable<Devices1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Devices.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Devices1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<Devices1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Devices1Schema>
                {
                    Title = "Devices",

                    Page = typeof(Pages.DevicesListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Thumbnail.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.DevicesDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Devices1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Devices1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Devices1Schema>>
                {
                };

                return new DetailPageConfig<Devices1Schema>
                {
                    Title = "Devices",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
