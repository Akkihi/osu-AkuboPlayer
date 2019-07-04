using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using osu_akuboplayer.Properties;
using static System.Windows.Controls.DockPanel;
using System.Windows.Threading;
using System.Timers;

namespace osu_akuboplayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static FolderBrowserDialog fbd = new FolderBrowserDialog();
        public MainWindow()
        {

            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
        }

        public static string path = fbd.SelectedPath + @"\Songs";
        private void Import_Click(object sender, RoutedEventArgs e)
        {

           

            System.Windows.Forms.MessageBox.Show("Choose the path");



            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Playlist.Items.Clear();
                System.Windows.Forms.MessageBox.Show(fbd.SelectedPath);

                DirectoryInfo dirInfo = new DirectoryInfo(fbd.SelectedPath);

                var Song = new Song(dirInfo);

                




                //Playlist.Items.Add(Song.Artist + " - " + Song.Title);
                for (int i = 0; i < Song.Files.Count; i++)
                {
                    Playlist.Items.Add(Song.Files[i]);   //(Song.Artist + " - " + Song.Title);

                }

            }

                                          
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            {
                string txtOrig = Search.Text;

                Playlist.FindName(txtOrig);
  
            }
        }
            
        DispatcherTimer timer = new DispatcherTimer();


        private void PlaySong_Click(object sender, RoutedEventArgs e)
        {
            if((Playlist.Items.Count != 0) && (Playlist.SelectedIndex != -1))
            {
                string current = Song.mp3[Playlist.SelectedIndex];
                Player.Play(current, Player.Volume);
                DispatcherTimer timer = new DispatcherTimer();
                timer.Start();
                
                
                NowLengh.Text = TimeSpan.FromSeconds(Player.GetOfPosTrack(Player.Stream)).ToString();
                MaxLengh.Text = TimeSpan.FromSeconds(Player.GetTimeStream(Player.Stream)).ToString();
                sliderlengh.Value = Player.GetOfPosTrack(Player.Stream);
                sliderlengh.Maximum = Player.GetTimeStream(Player.Stream);
                timer.Tick += new EventHandler(timer_Tick);






            }
         }

        private void StopSong_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Player.Stop();
            MaxLengh.Text = "Stopped";
            sliderlengh.Value = 0;

        }

        private void Sliderlengh_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Player.PosScroll(Player.Stream, Player.GetOfPosTrack(Player.Stream));
        }
        public static double mili;
        private void timer_Tick(object sender, EventArgs e)
        {
            
            mili = 1;
            timer.Interval = TimeSpan.FromMilliseconds(mili);
            NowLengh.Text = TimeSpan.FromSeconds(Player.GetOfPosTrack(Player.Stream)).ToString();
            sliderlengh.Value = Player.GetOfPosTrack(Player.Stream);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.SetVolumeToStream(Player.Stream, (int)SliderVol.Value);
        }
    }
}
