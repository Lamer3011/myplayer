using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;

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
			openFileDialog.Filter = "Media files (*.mp3)|*.mp3|All files (*.*)|*.*";//Відкривання файлу
			if (openFileDialog.ShowDialog() == true)
            {
				mediaPlayer.Open(new Uri(openFileDialog.FileName));
				string file_name = openFileDialog.SafeFileName;
				name_label.Content = file_name;
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
				string pos = mediaPlayer.Position.ToString(@"mm\:ss");		//Хвилини та секунди в лейблі
				string maxtime = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");

				lblStatus.Content = String.Format("{0} / {1}", pos, maxtime);
			}
			else
            {
				lblStatus.Content = "No file selected...";			//Помилка
			}
		}

		private void Play_btn_Click(object sender, RoutedEventArgs e) 
		{
            mediaPlayer.Play();                                     //Старт пісні
		}

		private void Pause_btn_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Pause();									//Пауза пісні
		}

		private void Stop_btn_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Stop();										//Зупинити пісню
		}

        private void voulme_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
			mediaPlayer.Volume = (double)voulme.Value;				//Змінити гучність
		}
        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
			if (e.Delta > 0)
            {
				mediaPlayer.Volume += 0.01;
				voulme.Value += 0.01;
			}
																	//Змінити гучність мишкою
			else if (e.Delta < 0)
            {
				mediaPlayer.Volume -= 0.01;
				voulme.Value -= 0.01;
			}
		}
    }
}
