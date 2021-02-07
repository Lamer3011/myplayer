using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Threading;

namespace myplayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Media files (*.mp3)|*.mp3|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
            {
				mediaPlayer.Open(new Uri(openFileDialog.FileName));
				string file_name = openFileDialog.SafeFileName;
				this.Title = file_name + " MyPlayer";
			}
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (mediaPlayer.Source != null)
            {
				lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
			}
			else
            {
				lblStatus.Content = "No file selected...";
			}
		}

		private void Play_btn_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Play();
		}

		private void Pause_btn_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Pause();
		}

		private void Stop_btn_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Stop();
		}

        private void voulme_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
			mediaPlayer.Volume = (double)voulme.Value;
		}
    }
}
