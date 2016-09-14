//---------------------------------------------------------------------------
//
// <copyright file="FavoritesPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>9/14/2016 6:54:25 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;

using KaransCatalog.ViewModels;

namespace KaransCatalog.Pages
{
    public sealed partial class FavoritesPage : Page
    {
        public FavoritesPage()
        {
            this.ViewModel = new FavoritesViewModel();
            this.InitializeComponent();
        }
        public FavoritesViewModel ViewModel { get; private set; }
		protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
			base.OnNavigatedTo(e);
            await ViewModel.LoadDataAsync();
            ShellPage.Current.ShellControl.SelectItem("Favorites");			
        }
    }    
}
