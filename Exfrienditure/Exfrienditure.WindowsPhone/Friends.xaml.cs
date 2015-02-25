using Exfrienditure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Exfrienditure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Friends : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Friends()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            add(false);
        }
        int no = 1; private IList<Contact> contacts;
        private async void Contact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var contactPicker = new Windows.ApplicationModel.Contacts.ContactPicker();
                contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Email);
                //contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);
                this.contacts = await contactPicker.PickContactsAsync();
                add(true);
            }
            catch (Exception e1) { }
        }
        private List<TranslateTransform> dragTranslation = new List<TranslateTransform>();
        void Drag_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TextBox ell = sender as TextBox;
            ell.IsEnabled = false;
            int i = Convert.ToInt16(ell.Tag);
            dragTranslation[i].X += e.Delta.Translation.X;
            if (dragTranslation[i].X > 120 || dragTranslation[i].X < -80)
                Stacks.Children.Remove(ell);
        }
        void Drag_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            TextBox ell = sender as TextBox;
            int i = Convert.ToInt16(ell.Tag);
            dragTranslation[i].X= 0;
            ell.IsEnabled = true;
        }
        private void add(bool flag)
        {
            if (flag)
            {
                no += contacts.Count;
                
                foreach (var i in contacts)
                {
                    TextBox f = new TextBox() { Tag = Stacks.Children.Count-5, ManipulationMode = ManipulationModes.TranslateX };
                    f.Text = i.DisplayName;
                    f.ManipulationDelta += Drag_ManipulationDelta;
                    f.ManipulationCompleted += Drag_ManipulationCompleted;
                    TranslateTransform drag = new TranslateTransform();
                    this.dragTranslation.Add(drag);
                    f.RenderTransform = this.dragTranslation.ElementAt(Stacks.Children.Count-5);
                    Stacks.Children.Add(f);
                }
            }
            else
            {
                no++; TextBox f = new TextBox() { Tag = Stacks.Children.Count - 5, ManipulationMode = ManipulationModes.TranslateX };
                f.PlaceholderText = "Friend " + no;
                f.ManipulationDelta += Drag_ManipulationDelta;
                f.ManipulationCompleted += Drag_ManipulationCompleted;
                TranslateTransform drag = new TranslateTransform();
                this.dragTranslation.Add(drag);
                f.RenderTransform = this.dragTranslation.ElementAt(Stacks.Children.Count - 5);
                Stacks.Children.Add(f);
            }
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Stacks.Children.Count <= 5) return;
                Stacks.Children.RemoveAt(Stacks.Children.Count - 1);
                no--;
            }
            catch (Exception) { }
        }
        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            String filename = trip.Text;
            if (filename.Equals("")) filename = "Trip";
            try
            {
                StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                StorageFile storageFile = await localFolder.CreateFileAsync(filename + ".csv", CreationCollisionOption.GenerateUniqueName);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    try
                    {
                        writer.WriteLine(no.ToString()+",0");
                        for (int i = 4; i < Stacks.Children.Count;i++ )
                        {
                            TextBox fl=(TextBox)Stacks.Children.ElementAt(i);
                            writer.Write(fl.Text + Environment.NewLine);
                        }
                    }
                    catch (Exception) { }
                    // MessageDialogHelper.Show("Success", "Added"); 
                }
                filename = storageFile.DisplayName;
                /*storageFile = await localFolder.CreateFileAsync("Exfrienditure.csv", CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                //int no = 0;
                using (StreamReader reader = new StreamReader(readStream))
                {
                    no = Convert.ToInt16(reader.ReadLine());
                }
                storageFile = await localFolder.CreateFileAsync("Exfrienditure.csv", CreationCollisionOption.OpenIfExists);
                readStream = await storageFile.OpenStreamForReadAsync();
                string[] trips=new string[no];
                using (StreamReader reader = new StreamReader(readStream))
                {
                    reader.ReadLine();
                    for (int i = 0; i < no; i++)
                    {
                        trips[i] = reader.ReadLine();
                    }
                }*/
                storageFile = await localFolder.CreateFileAsync("Exfrienditure.csv", CreationCollisionOption.OpenIfExists);
                writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine(MainPage.no+1);
                    for (int i = 0; i < MainPage.no; i++)
                    {
                        writer.WriteLine(MainPage.trips[i]);//Trip[i].Name);
                    }
                    writer.WriteLine(filename);
                }
                this.Frame.Navigate(typeof(Timeline),filename+".csv");
            }
            catch (Exception e1) { MessageDialogHelper.Show(e1.ToString(), "Error"); }
        }
    }
}
