using Exfrienditure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.Xaml.Shapes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Exfrienditure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Timeline : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        String filename = "";
        public Timeline()
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
        int no = 0; int ntran = 0;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            filename = e.Parameter.ToString();
            String time = "", lat = "", lon = "", acc = "";
            string[] tilo;
            try
            {
                StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                StorageFile storageFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String s = reader.ReadLine();
                    no = Convert.ToInt16(s.Substring(0, s.IndexOf(',')));
                    ntran = Convert.ToInt16(s.Substring(s.IndexOf(',') + 1));
                    tilo = new string[ntran];
                    for (int i = 0; i < no; i++) reader.ReadLine();
                    for (int i = 0; i < ntran; i++) tilo[i]=reader.ReadLine();
                }
                Ellipse[] ell = new Ellipse[ntran + 1];
                ell[0] = new Ellipse() { Height = 50, Width = 50, Fill = new SolidColorBrush(Windows.UI.Colors.Red) };
                ell[0].Tapped += new TappedEventHandler(Ellipse_Tapped);
                ell[0].Tag = ntran;
                TimeLine.Children.Add(ell[0]);
                TextBlock txt = new TextBlock() { Height = 100, Text = Environment.NewLine+" Add a transaction...", FontSize = 20 };
                Locat.Children.Add(txt);
                for (int i = ntran-1; i >=0; i--)
                {
                    time = tilo[i].Substring(0, tilo[i].IndexOf(",")); tilo[i] = tilo[i].Substring(tilo[i].IndexOf(",") + 1);
                    lat = tilo[i].Substring(0, tilo[i].IndexOf(",")); tilo[i] = tilo[i].Substring(tilo[i].IndexOf(",") + 1);
                    lon = tilo[i].Substring(0, tilo[i].IndexOf(",")); tilo[i] = tilo[i].Substring(tilo[i].IndexOf(",") + 1);
                    acc = tilo[i];
                    Rectangle rect = new Rectangle() { Height = 50, Width = 5, Fill = new SolidColorBrush(Windows.UI.Colors.White) };
                    ell[i] = new Ellipse() { Height = 50, Width = 50, Fill = new SolidColorBrush(Windows.UI.Colors.Green) };
                    ell[i].Tapped += new TappedEventHandler(Ellipse_Tapped);
                    ell[i].Tag = i;
                    TimeLine.Children.Add(rect);
                    TimeLine.Children.Add(ell[i]);
                   
                    txt = new TextBlock() { Height=100, Text = "When? :" + time + Environment.NewLine + "Latitude: " + lat + Environment.NewLine + "Longitude: " + lon , FontSize = 20 };
                    Locat.Children.Add(txt);
                }
                
            }
            catch (Exception eq) { return; }
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        public static int selected = 0;
        private void Ellipse_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Ellipse ell=sender as Ellipse;
            selected=Convert.ToInt16(ell.Tag);
            this.Frame.Navigate(typeof(Transactions),filename);
        }

        private void Close_Trip(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Result), filename);
        }
    }
}
