using ObjednavkovySystem.Services;
using System.Threading.Tasks;
using System.Windows;

namespace ObjednavkovySystem
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await Task.Run(async () =>
            {
                await SyncService.Instance().SyncAsync();
            });
        }
    }
}