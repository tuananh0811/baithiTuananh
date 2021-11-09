using baithiTuananh.Entities;
using baithiTuananh.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace baithiTuananh
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ContactModel contactModel = new ContactModel();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += AfterLoadingComponent;
        }

        private void AfterLoadingComponent(object sender, RoutedEventArgs e)
        {
            LoadingContactList();
        }

        public void LoadingContactList()
        {
            contactList.ItemsSource = contactModel.FindAll();
        }

        public async void SaveNewContact(object sender, RoutedEventArgs e)
        {
            var newContact = new ContactEntities
            {
                FullName = nameTxt.Text,
                PhoneNumber = phoneTxt.Text,
            };
            ContentDialog dialog = new ContentDialog();
            if (contactModel.Save(newContact))
            {
                dialog.Title = "Notification";
                dialog.Content = "Save successful";
                dialog.CloseButtonText = "Close";
                await dialog.ShowAsync();
            }
            else
            {
                dialog.Title = "Notification";
                dialog.Content = " Failed";
                dialog.CloseButtonText = "Close";
                await dialog.ShowAsync();
            }
            LoadingContactList();
        }

        public async void SearchContactByName(object sender, RoutedEventArgs e)
        {
            var contact = contactModel.FindByName(nameSearchTxt.Text);
            if (contact != null)
            {
                phoneSearchTxt.Text = contact.PhoneNumber;
            }
            else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.Title = "Notification";
                dialog.Content = "Contact not found";
                dialog.CloseButtonText = "Close";
                await dialog.ShowAsync();
            }
        }
    }
}
