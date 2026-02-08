using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;


namespace BGMPlayer
{
    /// <summary>
    /// AddPlaylist.xaml の相互作用ロジック
    /// </summary>
    public partial class AddPlaylist : Window
    {
        public AddPlaylist()
        {
            InitializeComponent();
        }

        public void AddToPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            string title = PlaylistNameTextBox.Text;
            string url = PlaylistUrlTextBox.Text;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("タイトルとURLの両方を入力してください。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string filePath = "playlist.json";
                List<VideoItem> playlist;

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath, Encoding.UTF8);
                    Playlist existingPlaylist = JsonSerializer.Deserialize<Playlist>(json);
                    playlist = existingPlaylist.videos ?? new List<VideoItem>();
                }
                else
                {
                    playlist = new List<VideoItem>();
                }

                playlist.Add(new VideoItem
                {
                    VideoTitle = title,
                    VideoUrl = url
                });

                Playlist updatedPlaylist = new Playlist
                {
                    videos = playlist
                };
                string updatedJson = JsonSerializer.Serialize(updatedPlaylist, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, updatedJson, Encoding.UTF8);

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.UpdatePlaylist();

                MessageBox.Show("ビデオが追加されました。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                PlaylistNameTextBox.Clear();
                PlaylistUrlTextBox.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
