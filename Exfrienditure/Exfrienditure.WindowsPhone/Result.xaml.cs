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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Exfrienditure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Result : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Result()
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
        String fileName = "";
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
            fileName = e.Parameter.ToString();
            fill(e.Parameter.ToString()); this.navigationHelper.OnNavigatedTo(e);
        }
        int ntran = 0, no = 0;
        List<string> list = new List<string>();
        
        private async void fill(String filename)
        {
            try
            {
                Thickness th = new Thickness(0, 10, 0, 10);
                StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                StorageFile storageFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String s = reader.ReadLine();
                    no = Convert.ToInt16(s.Substring(0, s.IndexOf(',')));
                    int[] exp = new int[no];
                    string[] names = new string[no];
                    ntran = Convert.ToInt16(s.Substring(s.IndexOf(',') + 1));
                    for (int i = 0; i < no; i++)
                    {
                        list.Add(reader.ReadLine());
                        String str = list.ElementAt(i), prc = "";
                        prc = str.Substring(str.IndexOf(',')+1);
                        str = str.Substring(0,str.IndexOf(','));
                        int sum = 0;
                        for(int k=0;k<ntran-1;k++)
                        {
                            string sf = prc.Substring(0,prc.IndexOf(','));
                            prc = prc.Substring(prc.IndexOf(',')+1);
                            sum += (Convert.ToInt32(sf));
                        }
                        sum += Convert.ToInt32(prc);
                        exp[i] = sum;
                        names[i] = str;
                        TextBlock txt1 = new TextBlock() { Margin = th, Text = str, FontSize = 25, Height = 40 };
                        TextBlock txt2 = new TextBlock() { Margin = new Thickness(0, 10, 0, 10), Text = sum.ToString(), FontSize = 25, Height = 40 };
                        Prices.Children.Add(txt2);
                        Stacks.Children.Add(txt1);

                    } int add = 0,min=exp[0],imin=0;
                    for (int i = 0; i < no ; i++) { add += exp[i]; if (min > exp[i]){min = exp[i];imin=i;} }
                    for (int i = 0; i < no; i++)
                    {
                          
                            exp[i] = exp[i]-(add / no);
                            TextBlock txt1 = new TextBlock() { Margin = th, Text = names[i], FontSize = 25, Height = 40 };
                            TextBlock txt2 = new TextBlock() { Margin = new Thickness(0, 10, 0, 10), Text = exp[i].ToString(), FontSize = 25, Height = 40 };
                            sPrices.Children.Add(txt2);
                            sStacks.Children.Add(txt1);
                    }
                    for (int i = 0; i < no ; i++)
                    {
                        if (i == imin) continue;
                        
                        TextBlock txt1= new TextBlock();
                        if(exp[i]>0)
                        {txt1 = new TextBlock() { Margin = th, Text = names[imin]+" owes "+exp[i]+" bucks to "+names[i]+".", FontSize = 25,TextWrapping=TextWrapping.Wrap};rStacks.Children.Add(txt1);}
                        else if (exp[i] < 0)
                        { txt1 = new TextBlock() { Margin = th, Text = names[i] + " owes " + (-exp[i]) + " bucks to " + names[imin] + ".", FontSize = 25, TextWrapping = TextWrapping.Wrap }; rStacks.Children.Add(txt1); }
                        
                    }
                    if (rStacks.Children.Count == 0)
                    {
                        TextBlock txt1 = new TextBlock() { Margin = th, Text = "Nobody owes anyone anything.", FontSize = 25, TextWrapping = TextWrapping.Wrap };
                        rStacks.Children.Add(txt1);
                    }
                }
            }
            catch (Exception eq) {  }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
