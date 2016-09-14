//---------------------------------------------------------------------------
//
// <copyright file="DevicesListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class DevicesListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public DevicesListPage()
        {
			ViewModel = ViewModelFactory.NewList(new DevicesSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("9baee65b-f783-4de1-b001-37beb65e2503");
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
