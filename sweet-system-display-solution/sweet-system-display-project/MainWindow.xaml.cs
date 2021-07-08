using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;

namespace sweet_system_display_project
{
    public class InstalledProgram
    {
        public string DisplayName { get; set; }
        public string Version { get; set; }
        public string InstalledDate { get; set; }
        public string Publisher { get; set; }
        public string UnninstallCommand { get; set; }
        public string ModifyPath { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<InstalledProgram> InstalledPrograms { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            InstalledPrograms = GetInstalledApps();
        }

        public List<InstalledProgram> GetInstalledApps()
        {
            var installedprograms = new List<InstalledProgram>();
            var registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using(RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach(string subkey_name in key.GetSubKeyNames())
                {
                    using(RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if(subkey.GetValue("DisplayName") != null)
                        {
                            installedprograms.Add(new InstalledProgram
                            {
                                DisplayName = (string)subkey.GetValue("DisplayName"),
                                Version = (string)subkey.GetValue("DisplayVersion"),
                                InstalledDate = (string)subkey.GetValue("InstallDate"),
                                Publisher = (string)subkey.GetValue("Publisher"),
                                UnninstallCommand = (string)subkey.GetValue("UninstallString"),
                                ModifyPath = (string)subkey.GetValue("ModifyPath")
                            });
                        }
                    }
                }
            }

            return installedprograms;
        }
    }
}
