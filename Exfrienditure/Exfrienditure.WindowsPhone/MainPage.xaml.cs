using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Exfrienditure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //read(); run(); 
            this.NavigationCacheMode = NavigationCacheMode.Required;
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Screen.Children.Count == 1)
            {read(); run();}
            else
            {

            }
        }
       // public static List<Trips> Trip=new List<Trips>();
        public static int no = 0;
        public static string[] trips;

        private async void read()
        {
            try
            {
                StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                StorageFile storageFile = await localFolder.CreateFileAsync("Exfrienditure.csv", CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    no = Convert.ToInt16(reader.ReadLine());
                    trips=new string[no];
                    //Trips[] ob1 = new Trips[no];
                    for (int i = 0; i < no; i++)
                    {
                        //ob1[i] = new Trips();
                        //ob1[i].Name = 
                         trips[i]=   reader.ReadLine();
                         
                       // ob1[i].Color = colorNames[(i + 20) % (colorNames.Length)];
                       // Trip.Add(ob1[i]);
                    }
                }
            }
            catch (Exception eq) { }
            if (no == 0)
            {
                StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                StorageFile storageFile = await localFolder.CreateFileAsync("Exfrienditure.csv", CreationCollisionOption.OpenIfExists);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
           // Trip.Reverse();
           // trips.Reverse();
            //listBox.ItemsSource = Trip;
          //  FlipView ob = new FlipView() { Height=150,Width=300};
          //  Screen.Children.Add(Trip);
        }
        static string[] colorNames =
    {
	"AliceBlue","AntiqueWhite","Aqua","Aquamarine","Azure",
    "Beige","Bisque","Black","BlanchedAlmond","Blue","BlueViolet","Brown","BurlyWood",
    "CadetBlue","Chartreuse","Chocolate","Coral","CornflowerBlue","Cornsilk","Crimson","Cyan",
     "DarkBlue","DarkCyan","DarkGoldenrod","DarkGray","DarkGreen","DarkKhaki","DarkMagenta",
        "DarkOliveGreen","DarkOrange","DarkOrchid","DarkRed","DarkSalmon","DarkSeaGreen","DarkSlateBlue",
        "DarkSlateGray","DarkTurquoise","DarkViolet","DeepPink","DeepSkyBlue","DimGray","DodgerBlue",
        "Firebrick","FloralWhite","ForestGreen","Fuchsia","Gainsboro","GhostWhite","Gold","Goldenrod","Gray",
        "Green","GreenYellow","Honeydew","HotPink","IndianRed","Indigo","Ivory","Khaki","Lavender","LavenderBlush",
        "LawnGreen","LemonChiffon","LightBlue","LightCoral","LightCyan","LightGoldenrodYellow","LightGray","LightGreen",
        "LightPink","LightSalmon","LightSeaGreen","LightSkyBlue","LightSlateGray","LightSteelBlue","LightYellow","Lime",
        "LimeGreen","Linen","Magenta","Maroon","MediumAquamarine","MediumBlue","MediumOrchid","MediumPurple","MediumSeaGreen",
        "MediumSlateBlue","MediumSpringGreen","MediumTurquoise","MediumVioletRed","MidnightBlue","MintCream","MistyRose",
        "Moccasin","NavajoWhite","Navy","OldLace","Olive","OliveDrab","Orange","OrangeRed","Orchid","PaleGoldenrod","PaleGreen", 
        "PaleTurquoise",  "PaleVioletRed", "PapayaWhip", "PeachPuff",  "Peru", "Pink", "Plum", "PowderBlue","Purple","Red", 
        "RosyBrown", "RoyalBlue","SaddleBrown", "Salmon",  "SandyBrown", "SeaGreen", "SeaShell",  "Sienna",  "Silver", 
        "SkyBlue",  "SlateBlue",  "SlateGray", "Snow","SpringGreen","SteelBlue", "Tan", "Teal", "Thistle", "Tomato", 
        "Transparent","Turquoise","Violet","Wheat","White","WhiteSmoke", "Yellow","YellowGreen"
    };
        private void Add_Click(object sender, RoutedEventArgs e)
        {
           //this.Frame.Navigate(typeof(BasicPage1)); 
            this.Frame.Navigate(typeof(Friends));
        }
        private Windows.UI.Color ColorName(string s)
        {
            switch (s)
            {
                case "AliceBlue": return Windows.UI.Colors.AliceBlue;
                case "AntiqueWhite": return Windows.UI.Colors.AntiqueWhite;
                case "Aqua": return Windows.UI.Colors.Aqua;
                case "Aquamarine": return Windows.UI.Colors.Aquamarine;
                case "Azure": return Windows.UI.Colors.Azure;
                case "Beige": return Windows.UI.Colors.Beige;
                case "Bisque": return Windows.UI.Colors.Bisque;
                case "Black": return Windows.UI.Colors.Black;
                case "BlanchedAlmond": return Windows.UI.Colors.BlanchedAlmond;
                case "Blue": return Windows.UI.Colors.Blue;
                case "BlueViolet": return Windows.UI.Colors.BlueViolet;
                case "Brown": return Windows.UI.Colors.Brown;
                case "BurlyWood": return Windows.UI.Colors.BurlyWood;
                case "CadetBlue": return Windows.UI.Colors.CadetBlue;
                case "Chartreuse": return Windows.UI.Colors.Chartreuse;
                case "Chocolate": return Windows.UI.Colors.Chocolate;
                case "Coral": return Windows.UI.Colors.Coral;
                case "CornflowerBlue": return Windows.UI.Colors.CornflowerBlue;
                case "Cornsilk": return Windows.UI.Colors.Cornsilk;
                case "Crimson": return Windows.UI.Colors.Crimson;
                case "Cyan": return Windows.UI.Colors.Cyan;
                case "DarkBlue": return Windows.UI.Colors.DarkBlue;
                case "DarkCyan": return Windows.UI.Colors.DarkCyan;
                case "DarkGoldenrod": return Windows.UI.Colors.DarkGoldenrod;
                case "DarkGray": return Windows.UI.Colors.DarkGray;
                case "DarkGreen": return Windows.UI.Colors.DarkGreen;
                case "DarkKhaki": return Windows.UI.Colors.DarkKhaki;
                case "DarkMagenta": return Windows.UI.Colors.DarkMagenta;
                case "DarkOliveGreen": return Windows.UI.Colors.DarkOliveGreen;
                case "DarkOrange": return Windows.UI.Colors.DarkOrange;
                case "DarkOrchid": return Windows.UI.Colors.DarkOrchid;
                case "DarkRed": return Windows.UI.Colors.DarkRed;
                case "DarkSalmon": return Windows.UI.Colors.DarkSalmon;
                case "DarkSeaGreen": return Windows.UI.Colors.DarkSeaGreen;
                case "DarkSlateBlue": return Windows.UI.Colors.DarkSlateBlue;
                case "DarkSlateGray": return Windows.UI.Colors.DarkSlateGray;
                case "DarkTurquoise": return Windows.UI.Colors.DarkTurquoise;
                case "DarkViolet": return Windows.UI.Colors.DarkViolet;
                case "DeepPink": return Windows.UI.Colors.DeepPink;
                case "DeepSkyBlue": return Windows.UI.Colors.DeepSkyBlue;
                case "DimGray": return Windows.UI.Colors.DimGray;
                case "DodgerBlue": return Windows.UI.Colors.DodgerBlue;
                case "Firebrick": return Windows.UI.Colors.Firebrick;
                case "FloralWhite": return Windows.UI.Colors.FloralWhite;
                case "ForestGreen": return Windows.UI.Colors.ForestGreen;
                case "Fuchsia": return Windows.UI.Colors.Fuchsia;
                case "Gainsboro": return Windows.UI.Colors.Gainsboro;
                case "GhostWhite": return Windows.UI.Colors.GhostWhite;
                case "Gold": return Windows.UI.Colors.Gold;
                case "Goldenrod": return Windows.UI.Colors.Goldenrod;
                case "Gray": return Windows.UI.Colors.Gray;
                case "Green": return Windows.UI.Colors.Green;
                case "GreenYellow": return Windows.UI.Colors.GreenYellow;
                case "Honeydew": return Windows.UI.Colors.Honeydew;
                case "HotPink": return Windows.UI.Colors.HotPink;
                case "IndianRed": return Windows.UI.Colors.IndianRed;
                case "Indigo": return Windows.UI.Colors.Indigo;
                case "Ivory": return Windows.UI.Colors.Ivory;
                case "Khaki": return Windows.UI.Colors.Khaki;
                case "Lavender": return Windows.UI.Colors.Lavender;
                case "LavenderBlush": return Windows.UI.Colors.LavenderBlush;
                case "LawnGreen": return Windows.UI.Colors.LawnGreen;
                case "LemonChiffon": return Windows.UI.Colors.LemonChiffon;
                case "LightBlue": return Windows.UI.Colors.LightBlue;
                case "LightCoral": return Windows.UI.Colors.LightCoral;
                case "LightCyan": return Windows.UI.Colors.LightCyan;
                case "LightGoldenrodYellow": return Windows.UI.Colors.LightGoldenrodYellow;
                case "LightGray": return Windows.UI.Colors.LightGray;
                case "LightGreen": return Windows.UI.Colors.LightGreen;
                case "LightPink": return Windows.UI.Colors.LightPink;
                case "LightSalmon": return Windows.UI.Colors.LightSalmon;
                case "LightSeaGreen": return Windows.UI.Colors.LightSeaGreen;
                case "LightSkyBlue": return Windows.UI.Colors.LightSkyBlue;
                case "LightSlateGray": return Windows.UI.Colors.LightSlateGray;
                case "LightSteelBlue": return Windows.UI.Colors.LightSteelBlue;
                case "LightYellow": return Windows.UI.Colors.LightYellow;
                case "Lime": return Windows.UI.Colors.Lime;
                case "LimeGreen": return Windows.UI.Colors.LimeGreen;
                case "Linen": return Windows.UI.Colors.Linen;
                case "Magenta": return Windows.UI.Colors.Magenta;
                case "Maroon": return Windows.UI.Colors.Maroon;
                case "MediumAquamarine": return Windows.UI.Colors.MediumAquamarine;
                case "MediumBlue": return Windows.UI.Colors.MediumBlue;
                case "MediumOrchid": return Windows.UI.Colors.MediumOrchid;
                case "MediumPurple": return Windows.UI.Colors.MediumPurple;
                case "MediumSeaGreen": return Windows.UI.Colors.MediumSeaGreen;
                case "MediumSlateBlue": return Windows.UI.Colors.MediumSlateBlue;
                case "MediumSpringGreen": return Windows.UI.Colors.MediumSpringGreen;
                case "MediumTurquoise": return Windows.UI.Colors.MediumTurquoise;
                case "MediumVioletRed": return Windows.UI.Colors.MediumVioletRed;
                case "MidnightBlue": return Windows.UI.Colors.MidnightBlue;
                case "MintCream": return Windows.UI.Colors.MintCream;
                case "MistyRose": return Windows.UI.Colors.MistyRose;
                case "Moccasin": return Windows.UI.Colors.Moccasin;
                case "NavajoWhite": return Windows.UI.Colors.NavajoWhite;
                case "Navy": return Windows.UI.Colors.Navy;
                case "OldLace": return Windows.UI.Colors.OldLace;
                case "Olive": return Windows.UI.Colors.Olive;
                case "OliveDrab": return Windows.UI.Colors.OliveDrab;
                case "Orange": return Windows.UI.Colors.Orange;
                case "OrangeRed": return Windows.UI.Colors.OrangeRed;
                case "Orchid": return Windows.UI.Colors.Orchid;
                case "PaleGoldenrod": return Windows.UI.Colors.PaleGoldenrod;
                case "PaleGreen": return Windows.UI.Colors.PaleGreen;
                case "PaleTurquoise": return Windows.UI.Colors.PaleTurquoise;
                case "PaleVioletRed": return Windows.UI.Colors.PaleVioletRed;
                case "PapayaWhip": return Windows.UI.Colors.PapayaWhip;
                case "PeachPuff": return Windows.UI.Colors.PeachPuff;
                case "Peru": return Windows.UI.Colors.Peru;
                case "Pink": return Windows.UI.Colors.Pink;
                case "Plum": return Windows.UI.Colors.Plum;
                case "PowderBlue": return Windows.UI.Colors.PowderBlue;
                case "Purple": return Windows.UI.Colors.Purple;
                case "Red": return Windows.UI.Colors.Red;
                case "RosyBrown": return Windows.UI.Colors.RosyBrown;
                case "RoyalBlue": return Windows.UI.Colors.RoyalBlue;
                case "SaddleBrown": return Windows.UI.Colors.SaddleBrown;
                case "Salmon": return Windows.UI.Colors.Salmon;
                case "SandyBrown": return Windows.UI.Colors.SandyBrown;
                case "SeaGreen": return Windows.UI.Colors.SeaGreen;
                case "SeaShell": return Windows.UI.Colors.SeaShell;
                case "Sienna": return Windows.UI.Colors.Sienna;
                case "Silver": return Windows.UI.Colors.Silver;
                case "SkyBlue": return Windows.UI.Colors.SkyBlue;
                case "SlateBlue": return Windows.UI.Colors.SlateBlue;
                case "SlateGray": return Windows.UI.Colors.SlateGray;
                case "Snow": return Windows.UI.Colors.Snow;
                case "SpringGreen": return Windows.UI.Colors.SpringGreen;
                case "SteelBlue": return Windows.UI.Colors.SteelBlue;
                case "Tan": return Windows.UI.Colors.Tan;
                case "Teal": return Windows.UI.Colors.Teal;
                case "Thistle": return Windows.UI.Colors.Thistle;
                case "Tomato": return Windows.UI.Colors.Tomato;
                case "Transparent": return Windows.UI.Colors.Transparent;
                case "Turquoise": return Windows.UI.Colors.Turquoise;
                case "Violet": return Windows.UI.Colors.Violet;
                case "Wheat": return Windows.UI.Colors.Wheat;
                case "White": return Windows.UI.Colors.White;
                case "WhiteSmoke": return Windows.UI.Colors.WhiteSmoke;
                case "Yellow": return Windows.UI.Colors.Yellow;
                case "YellowGreen": return Windows.UI.Colors.YellowGreen;
            }
            return Windows.UI.Colors.Black;
        }

        private void lstColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  this.Frame.Navigate(typeof(Timeline),(Trip[listBox.SelectedIndex].Name+".csv"));
        }
        private async void run()
        {
            try
            {
                TextBlock ob7 = new TextBlock() { Height = 150,Width=200 };
            Screentxt.Children.Add(ob7);
            StackPanel ob8 = new StackPanel() { Height = 5,  Background = new SolidColorBrush(Windows.UI.Colors.White) };
            StackPanel ob9 = new StackPanel() { Height = 5, Background = new SolidColorBrush(Windows.UI.Colors.White) };
            Screen.Children.Add(ob8);
            Screentxt.Children.Add(ob9);
            int ntran = 0, nt = 0;
            string path;
            List<List<FlipViewItem>> og = new List<List<FlipViewItem>>();
       
                for (int z = no-1; z >=0; z--)
                {
                    string[] tilo;
                    List<FlipViewItem> ob5 = new List<FlipViewItem>();
                    og.Add(ob5);
                    StorageFolder localFolder = KnownFolders.DocumentsLibrary;
                    StorageFile storageFile = await localFolder.CreateFileAsync(/*Trip[0].Name*/trips[z] + ".csv", CreationCollisionOption.OpenIfExists);
                    Stream readStream = await storageFile.OpenStreamForReadAsync();
                    using (StreamReader reader = new StreamReader(readStream))
                    {
                        String s = reader.ReadLine();
                        bool rndflag = false;
                        nt = Convert.ToInt16(s.Substring(0, s.IndexOf(',')));
                        ntran = Convert.ToInt16(s.Substring(s.IndexOf(',') + 1));
                        for (int i = 0; i < nt; i++)
                        {
                            reader.ReadLine();
                        }
                        tilo = new string[ntran];
                        for (int i = 0; i < ntran; i++)
                        {
                            tilo[i]=reader.ReadLine();
                        }
                        
                        for (int i = 0; i < ntran; i++)
                        {
                            try
                            {
                                path = reader.ReadLine();
                                path = path.Substring(path.LastIndexOf("\\") + 1);
                                StorageFolder stf = await KnownFolders.PicturesLibrary.GetFolderAsync("Camera Roll");
                                StorageFile file = await stf.GetFileAsync(path);
                                FlipViewItem ob = new FlipViewItem();
                                if (file != null)
                                {
                                    using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                                    {
                                        // Set the image source to the selected bitmap 
                                        BitmapImage bitmapImage = new BitmapImage();
                                        bitmapImage.DecodePixelWidth = 600; //match the target Image.Width, not shown
                                        await bitmapImage.SetSourceAsync(fileStream);
                                        ImageBrush ok = new ImageBrush();
                                        ok.ImageSource = bitmapImage;
                                        ob.Background = ok;
                                    }
                                    og[no-1-z].Add(ob);
                                }
                            }
                            catch (Exception)
                            {
                                FlipViewItem ob = new FlipViewItem();
                                rndflag=true;
                                //Random rnd = new Random();
                                //ob.Background = new SolidColorBrush(ColorName(colorNames[rnd.Next(0,colorNames.Length-1)]));
                                og[no-1-z].Add(ob);
                            }
                            
                        }
                        Grid ob1 = new Grid() { Height = 150, Width = 200 };
                        FlipView ob2 = new FlipView() { Height = 150, Width = 200 };
                        StackPanel ob3 = new StackPanel() { Background = new SolidColorBrush(Windows.UI.Colors.White), Height = 30, VerticalAlignment = VerticalAlignment.Bottom };
                        TextBlock ob4 = new TextBlock() { FontSize = 25, Foreground = new SolidColorBrush(Windows.UI.Colors.Black), TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 0, 0, 0) };
                        TextBlock ob6 = new TextBlock() {Height=150, Width=180,FontSize = 15, Foreground = new SolidColorBrush(Windows.UI.Colors.White), TextAlignment = TextAlignment.Left, TextWrapping=TextWrapping.Wrap };
                        BasicProperties basicProperties = await storageFile.GetBasicPropertiesAsync();
                        ob6.Text = "Date Created: " + storageFile.DateCreated.ToString() + Environment.NewLine + "Last modified on: " + basicProperties.DateModified;
                        Screentxt.Children.Add(ob6);
                        if (rndflag)
                        {
                            rndflag = false;
                            List<MapControl> mp = new List<MapControl>();
                            for (int i = 0; i < ntran; i++)
                            {
                                tilo[i] = tilo[i].Substring(tilo[i].IndexOf(",") + 1);
                                double lat = Convert.ToDouble(tilo[i].Substring(0, tilo[i].IndexOf(","))); tilo[i] = tilo[i].Substring(tilo[i].IndexOf(",") + 1);
                                double lon = Convert.ToDouble(tilo[i].Substring(0, tilo[i].IndexOf(",")));
                                MapControl MapControl1 = new MapControl() { Height = 150, Width = 300 };
                                MapControl1.Center = new Geopoint(new BasicGeoposition() { Latitude = lat, Longitude = lon });
                                MapControl1.ZoomLevel = 12;
                                MapControl1.LandmarksVisible = true;
                                mp.Add(MapControl1);
                            }
                            ob2.ItemsSource = mp;
                        }
                        else ob2.ItemsSource = og[no-1-z];
                        ob4.Text = trips[z];
                        ob1.Children.Add(ob2);
                        ob3.Children.Add(ob4);
                        ob1.Children.Add(ob3);
                        ob1.Tag = z;
                        ob1.Tapped += new TappedEventHandler(Grid_Tapped);
                        StackPanel ob18 = new StackPanel() { Height = 5, Width=200, Background= new SolidColorBrush(Windows.UI.Colors.White) };
                        StackPanel ob19 = new StackPanel() { Height = 5, Width = 180, Background = new SolidColorBrush(Windows.UI.Colors.White) };
                        Screen.Children.Add(ob1);
                        Screen.Children.Add(ob18);
                        Screentxt.Children.Add(ob19);
                    }
                }
            }
            catch (Exception eq) { }
       timerrun();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Grid ell=sender as Grid;
            this.Frame.Navigate(typeof(Timeline), (trips[Convert.ToInt32(ell.Tag)] + ".csv"));
        }
        private void timerrun()
        {
            try
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += (sender, e) =>
                {
                   // bool flag = true;
                    foreach (var i in Screen.Children)
                    {
                        Grid ob1;
                        //if (flag) { flag = false; continue; }
                        try { ob1 = (Grid)i; }
                        catch (InvalidCastException ed) { continue; }
                        FlipView ob2 = (FlipView)ob1.Children.ElementAt(0);
                        int k = ob2.SelectedIndex;
                        if (k < ob2.Items.Count - 1) k++;
                        else k = 0;
                        ob2.SelectedIndex = k;
                        //flag = true;
                    }
                };
                timer.Start();
            }
            catch (Exception eq) { }
        }

    }
}
