using System.Windows.Input;
using System.Windows.Navigation;

namespace Tron.Client.Application.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        private NavigationService _nav;

        private string _volumeSetting;
        public string VolumeSetting
        {
            get => _volumeSetting;
            set
            {
                SetProperty(ref _volumeSetting, value);
            }
        }

        public ICommand SwitchVolumeSettingCommand { get; }

        public ICommand GoBackCommand { get; }

        internal SettingsViewModel(NavigationService nav)
        {
            _nav = nav;

            SwitchVolumeSettingCommand = new RelayCommand(OnSwitchVolumeSetting);
            GoBackCommand = new RelayCommand(OnGoBack);

            VolumeSetting = "ON";
        }

        private void OnSwitchVolumeSetting()
        {
            VolumeSetting = VolumeSetting == "ON" ? "OFF" : "ON";
        }

        private void OnGoBack()
        {
            _nav.GoBack();
        }
    }
}
