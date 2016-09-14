using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;

using KaransCatalog.Navigation;
using KaransCatalog.ViewModels;

namespace KaransCatalog.Sections
{
    class DevicesSectionRelated : Section<ReviewsAndNews1Schema>
    {
		private LocalStorageDataProvider<ReviewsAndNews1Schema> _dataProvider;

		public DevicesSectionRelated()
        {
            _dataProvider = new LocalStorageDataProvider<ReviewsAndNews1Schema>();
        }

        public override async Task<IEnumerable<ReviewsAndNews1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var selected = connectedItem as Devices1Schema;
			if(selected == null)
			{
				return new ReviewsAndNews1Schema[0];
			}

			var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/ReviewsAndNews.json"
            };
			//avoid pagination because in memory filter
			var result = await _dataProvider.LoadDataAsync(config, int.MaxValue);
			return result
					.Where(r => r.Device.ToSafeString() == selected.Title.ToSafeString())
					.ToList();
        }

        public override async Task<IEnumerable<ReviewsAndNews1Schema>> GetNextPageAsync()
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

        public override ListPageConfig<ReviewsAndNews1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<ReviewsAndNews1Schema>
                {
                    Title = "News & Reviews",

                    LayoutBindings = (viewModel, item) =>
					{
						viewModel.Title = item.Title.ToSafeString();
						viewModel.SubTitle = item.Content.ToSafeString();
						viewModel.Description = null;
						viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
					},
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.ReviewsAndNewsDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<ReviewsAndNews1Schema> DetailPage
        {
            get
            {
                return null;
            }
        }
    }
}
