using Exfrienditure.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Exfrienditure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Transactions : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Transactions()
        {
            this.InitializeComponent();
            var app = Application.Current as App;
            if (app != null)
            {
                app.FilesPicked += OnFilesPicked;
            }   
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
        String fileName="";
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
            fileName=e.Parameter.ToString();
            Title.Text = "Transaction:"+(Timeline.selected+1).ToString();
            fill(e.Parameter.ToString());
            this.navigationHelper.OnNavigatedTo(e);
        }
        int ntran = 0, no = 0;
        List<string> list = new List<string>();
        private async void fill(String filename)
        {
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
                    if (Timeline.selected != ntran) Next.Visibility = Visibility.Collapsed;
                    for (int i = 0; i < no; i++)
                    {
                        list.Add(reader.ReadLine());
                        Thickness th;
                        if(i==0)th = new Thickness(0, 5, 0, 5);
                        else th=new Thickness(0,10,0,10);
                        InputScope inputScope = new InputScope();
                        InputScopeName inputScopeName = new InputScopeName();
                        inputScopeName.NameValue= InputScopeNameValue.NumberFullWidth;
                        inputScope.Names.Add(inputScopeName);
                        String str = list.ElementAt(i),prc="";
                        if (ntran != 0)
                        {
                            prc = str.Substring(str.IndexOf(',') + 1);
                            if (ntran != 0)
                            {
                                for (int l = 0; l < Timeline.selected; l++)
                                {
                                    prc = prc.Substring(prc.IndexOf(',') + 1);

                                }
                                if(prc.IndexOf(',')!=-1)
                                prc = prc.Substring(0, prc.IndexOf(','));
                            }

                            str = str.Substring(0, str.IndexOf(','));
                        }
                        if (prc.Length == 0) prc = "0";
                        
                            TextBlock txt1 = new TextBlock() { Margin = th, Text = str, FontSize = 25, Height = 40 };
                            if (Timeline.selected == ntran)
                            {
                                TextBox txt2 = new TextBox() { Text = "0", Height = 40, InputScope = inputScope };
                                Prices.Children.Add(txt2);
                            }
                            else
                            {
                                TextBlock txt2 = new TextBlock() { Margin = new Thickness(0,10,0,10), Text = prc, FontSize = 25, Height = 40 };
                                Prices.Children.Add(txt2);
                            }
                            Stacks.Children.Add(txt1);
                            
                    }
                    for (int i = 0; i < ntran; i++)
                        list.Add(reader.ReadLine());
                    for (int i = 0; i < ntran; i++)
                    {   list.Add(reader.ReadLine());
                    if (Timeline.selected == i)
                    {
                        path = list[no+ntran+i];
                        path = path.Substring(path.LastIndexOf("\\")+1);
                        StorageFolder stf= await KnownFolders.PicturesLibrary.GetFolderAsync("Camera Roll");
                        StorageFile file= await stf.GetFileAsync(path);
                        
                        if (file != null)
                        {
                            using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                            {
                                // Set the image source to the selected bitmap 
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                                await bitmapImage.SetSourceAsync(fileStream);
                                Image.Source = bitmapImage; Image.Visibility = Visibility.Visible;
                            }
                        }
                        //Image.Source = imgSource;
                        
                    }
                    }
                    
                    
                }
            }
            catch (Exception eq) { return; }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        Geolocator geo = null;
        string path = "";
        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                StorageFile storageFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine(no + "," + (ntran + 1));
                    try
                    {
                        for (int i = 0; i < no; i++)
                        {
                            TextBox bx = (TextBox)Prices.Children.ElementAt(i + 1);
                            writer.Write(list.ElementAt(i) + "," + bx.Text + Environment.NewLine);
                        }
                        if (geo == null)
                        {
                            geo = new Geolocator();
                        }
                        try
                        {
                            Geoposition pos = await geo.GetGeopositionAsync();
                            if (geo.LocationStatus == PositionStatus.Disabled)
                            {
                                MessageDialogHelper.Show("Location has been disabled in Settings.", "No Location.");
                                for(int i=0;i<ntran;i++)
                                    writer.WriteLine(list.ElementAt(no+i));
                                writer.WriteLine(DateTime.Now.ToString() + ",0,0,0");
                            }
                            else
                            {
                                for (int i = 0; i < ntran; i++)
                                    writer.WriteLine(list.ElementAt(no+i));
                                writer.WriteLine(DateTime.Now.ToString()+","+pos.Coordinate.Point.Position.Latitude.ToString() + "," + pos.Coordinate.Point.Position.Longitude.ToString() + "," + pos.Coordinate.Accuracy.ToString());
                            }
                        }
                        catch (Exception)
                        {
                            MessageDialogHelper.Show("Location is not available. Check whether it is enabled in the Settings.", "No Location.");
                            for (int i = 0; i < ntran; i++)
                                writer.WriteLine(list.ElementAt(no+i));
                            writer.WriteLine(DateTime.Now.ToString() + ",0,0,0");
                        }
                        for (int i = 0; i < ntran; i++)
                            writer.WriteLine(list.ElementAt(no+ntran + i));
                        writer.WriteLine(path);
                    }

                    catch (Exception eq) { }
                    // MessageDialogHelper.Show("Success", "Added"); 
                }
                NavigationHelper.GoBack();
            }
            catch (Exception) { }
        }
        
        private async void OnFilesPicked(IReadOnlyList<StorageFile> files)
        {
            Image.Source = null;
            if (files.Count > 0)
            {
                var imageFile = files.FirstOrDefault(f => SupportedImageFileTypes.Contains(f.FileType.ToLower()));
                if (imageFile != null)
                {
                    var bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(await imageFile.OpenReadAsync());
                    Image.Source = bitmapImage;
                    Image.Visibility = Visibility.Visible;
                    path=imageFile.Path;
                }
            }
        }

        private static readonly IEnumerable<string> SupportedImageFileTypes = new List<string> { ".jpeg", ".jpg", ".png" };

        private void BtnFileOpenPickerPhotosClick(object sender, RoutedEventArgs e)
        {
            TriggerPicker(SupportedImageFileTypes);
        }

        private static void TriggerPicker(IEnumerable<string> fileTypeFilers, bool shouldPickMultiple = false)
        {
            var fop = new FileOpenPicker();
            foreach (var fileType in fileTypeFilers)
            {
                fop.FileTypeFilter.Add(fileType);
            }fop.PickSingleFileAndContinue();
        }
    
    }
}
