using System.Windows.Navigation;
using System.Windows.Input;
using Tron.Client.Application.Views;

namespace Tron.Client.Application.ViewModels.Menu
{
    internal class SettingsViewModel : BaseViewModel
    {
        // NAV
        private readonly NavigationService _nav;

        // SETTINGS
        private string? _volumeSetting;
        public string? VolumeSetting
        {
            get => _volumeSetting;
            set => SetProperty(ref _volumeSetting, value);
        }

        // COMMANDS
        public ICommand SwitchVolumeSettingCommand { get; }
        public ICommand GoBackCommand { get; }

        internal SettingsViewModel(NavigationService nav)
        {
            _nav = nav;

            VolumeSetting = "ON";

            SwitchVolumeSettingCommand = new RelayCommand(OnSwitchVolumeSetting);
            GoBackCommand = new RelayCommand(OnGoBack);
        }

        private void OnSwitchVolumeSetting()
        {
            if (VolumeSetting == "ON")
            {
                VolumeSetting = "OFF";
                //((App)(System.Windows.Application.Current)).Volume = false;
            }
            else
            {
                VolumeSetting = "ON";
                //((App)(System.Windows.Application.Current)).Volume = true;
            }
        }

        private void OnGoBack() => _nav.Navigate(new MainMenuPage(_nav));
    }
}
