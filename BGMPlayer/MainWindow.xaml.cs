using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;

namespace BGMPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string filepath = "playlist.json";
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath, Encoding.UTF8);
                Playlist playlist = JsonSerializer.Deserialize<Playlist>(json);
                playlistBox.ItemsSource = playlist.videos;
                playlistBox.DisplayMemberPath = "VideoTitle";
            }
            else
            {
                MessageBox.Show("Playlistが見つかりません");
            }
        }
        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (webView != null && webView.CoreWebView2 != null)
                {
                    webView.CoreWebView2.Navigate(addressBar.Text);
                }
            }
            catch
            {
                return;
            }
        }

        private void playlistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(playlistBox.SelectedItem != null)
            {
                var selectedItem = playlistBox.SelectedItem as VideoItem;
                if (selectedItem != null)
                {
                    addressBar.Text = selectedItem.VideoUrl;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var AddPlaylistWindow = new AddPlaylist();
            AddPlaylistWindow.Owner = this;
            AddPlaylistWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (playlistBox.SelectedItem != null)
            {
                var selectedItem = playlistBox.SelectedItem as VideoItem;
                if (selectedItem != null)
                {
                    string filePath = "playlist.json";
                    List<VideoItem> playlist = new List<VideoItem>();

                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath, Encoding.UTF8);
                        Playlist existingPlaylist = JsonSerializer.Deserialize<Playlist>(json);
                        playlist = existingPlaylist.videos ?? new List<VideoItem>();
                    }
                    playlist.Remove(selectedItem);

                    Playlist updatedPlaylist = new Playlist
                    {
                        videos = playlist
                    };
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    string updatedJson = JsonSerializer.Serialize(updatedPlaylist, options);
                    File.WriteAllText(filePath, updatedJson, Encoding.UTF8);

                    UpdatePlaylist();
                    MessageBox.Show("選択した動画をプレイリストから削除しました。", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public void UpdatePlaylist()
        {
            try
            {
                string filepath = "playlist.json";
                if (File.Exists(filepath))
                {
                    string json = File.ReadAllText(filepath, Encoding.UTF8);
                    Playlist playlist = JsonSerializer.Deserialize<Playlist>(json);

                    playlistBox.ItemsSource = null;
                    playlistBox.ItemsSource = playlist.videos;
                }
                else
                {
                    MessageBox.Show("Playlistが見つかりません", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"プレイリストの更新中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}