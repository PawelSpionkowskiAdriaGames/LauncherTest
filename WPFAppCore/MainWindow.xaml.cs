using System.Diagnostics;
using System.Windows;

using Squirrel;

namespace WPFAppCore
{
    public partial class MainWindow : Window
    {
        UpdateManager manager;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            manager = await UpdateManager
                .GitHubUpdateManager(@"https://github.com/PawelSpionkowskiAdriaGames/LauncherTest");

            CurrentVersionTextBox.Text = manager.CurrentlyInstalledVersion().ToString();

        }

        private async void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            var updateInfo = await manager.CheckForUpdate();

            if (updateInfo.ReleasesToApply.Count > 0)
            {
                UpdateButton.IsEnabled = true;
            }
            else
            {
                UpdateButton.IsEnabled = false;
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await manager.UpdateApp();

            MessageBox.Show("Updated succesfuly!");
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
