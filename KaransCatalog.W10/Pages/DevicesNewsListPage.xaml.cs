//---------------------------------------------------------------------------
//
// <copyright file="DevicesNewsListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>9/14/2016 6:54:25 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using KaransCatalog.Sections;
using KaransCatalog.ViewModels;
using AppStudio.Uwp;

namespace KaransCatalog.Pages
{
    public sealed partial class DevicesNewsListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public DevicesNewsListPage()
        {
			ViewModel = ViewModelFactory.NewList(new DevicesNewsSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("faa41fdf-496e-41ee-b5f2-055edd439e40");
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
