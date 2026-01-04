using System.Text;
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

            VideoItem[] VideoList = new VideoItem[]
            {
                new VideoItem("【音楽的同位体】月光 / V.I.P", "https://youtu.be/K0QKls5uVMM"),
                new VideoItem("Stella", "https://youtu.be/CYZfhj-LDRk?list=RDCYZfhj-LDRk"),
            };
            playlistBox.ItemsSource = VideoList;
            playlistBox.DisplayMemberPath = "VideoTitle";
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
    }
}