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
    public class ReviewsAndNewsSection : Section<ReviewsAndNews1Schema>
    {
		private LocalStorageDataProvider<ReviewsAndNews1Schema> _dataProvider;

		public ReviewsAndNewsSection()
		{
			_dataProvider = new LocalStorageDataProvider<ReviewsAndNews1Schema>();
		}

		public override async Task<IEnumerable<ReviewsAndNews1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/ReviewsAndNews.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
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

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<ReviewsAndNews1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<ReviewsAndNews1Schema>
                {
                    Title = "Reviews and News",

                    Page = typeof(Pages.ReviewsAndNewsListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Content.ToSafeString();
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
                var bindings = new List<Action<ItemViewModel, ReviewsAndNews1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Review and News";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Image.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<ReviewsAndNews1Schema>>
                {
                    ActionConfig<ReviewsAndNews1Schema>.Link("Go to source", (item) => item.Url.ToSafeString()),
                };

                return new DetailPageConfig<ReviewsAndNews1Schema>
                {
                    Title = "Reviews and News",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
