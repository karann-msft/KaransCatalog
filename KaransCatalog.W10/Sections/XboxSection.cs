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
    public class XboxSection : Section<Xbox1Schema>
    {
		private LocalStorageDataProvider<Xbox1Schema> _dataProvider;

		public XboxSection()
		{
			_dataProvider = new LocalStorageDataProvider<Xbox1Schema>();
		}

		public override async Task<IEnumerable<Xbox1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Xbox.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Xbox1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<Xbox1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Xbox1Schema>
                {
                    Title = "Xbox",

                    Page = typeof(Pages.XboxListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.XboxDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Xbox1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Xbox1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Xbox1Schema>>
                {
                };

                return new DetailPageConfig<Xbox1Schema>
                {
                    Title = "Xbox",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
