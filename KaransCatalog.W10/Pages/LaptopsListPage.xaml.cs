//---------------------------------------------------------------------------
//
// <copyright file="LaptopsListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class LaptopsListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public LaptopsListPage()
        {
			ViewModel = ViewModelFactory.NewList(new LaptopsSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("df156f2b-4665-4341-a69f-53251a5f6964");
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
