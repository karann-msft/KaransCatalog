//---------------------------------------------------------------------------
//
// <copyright file="ReviewsAndNewsListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>9/14/2016 6:54:25 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using KaransCatalog.Sections;
using KaransCatalog.ViewModels;
using AppStudio.Uwp;

namespace KaransCatalog.Pages
{
    public sealed partial class ReviewsAndNewsListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public ReviewsAndNewsListPage()
        {
			ViewModel = ViewModelFactory.NewList(new ReviewsAndNewsSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("98d343cd-0b25-4ba8-80bd-4f99c363a17c");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
