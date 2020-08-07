using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Runtime;
using System.Security.Authentication;
using System.Threading;
using System.Windows;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using Win10Tools.Data;
using Win10Tools.Tools;

namespace Win10Tools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //[SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private static Mutex AppMutex;

        public App()
        {
            var cachePath = $"{AppDomain.CurrentDomain.BaseDirectory}Cache";
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            ProfileOptimization.SetProfileRoot(cachePath);
            ProfileOptimization.StartProfile("Profile");
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            AppMutex = new Mutex(true, "Win10Tools", out var createdNew);

            if (!createdNew)
            {
                var current = Process.GetCurrentProcess();

                foreach (var process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        Win32Helper.SetForegroundWindow(process.MainWindowHandle);
                        break;
                    }
                }
                Shutdown();
            }
            else
            {
                var splashScreen = new SplashScreen("Resources/Img/Cover.png");
                splashScreen.Show(true);

                base.OnStartup(e);

                UpdateRegistry();

                ShutdownMode = ShutdownMode.OnMainWindowClose;
                GlobalData.Init();
                ConfigHelper.Instance.SetLang(GlobalData.Config.Lang);

                if (GlobalData.Config.Skin != SkinType.Default)
                {
                    UpdateSkin(GlobalData.Config.Skin);
                }

                ConfigHelper.Instance.SystemVersionInfo = CommonHelper.GetSystemVersionInfo();
                ConfigHelper.Instance.SetWindowDefaultStyle();
                ConfigHelper.Instance.SetNavigationWindowDefaultStyle();

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SslProtocols)0x00000C00;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            GlobalData.Save();
        }

        internal void UpdateSkin(SkinType skin)
        {
            SharedResourceDictionary.SharedDictionaries.Clear();
            var skins0 = Resources.MergedDictionaries[0];
            skins0.MergedDictionaries.Clear();
            skins0.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            skins0.MergedDictionaries.Add(ResourceHelper.GetSkin(typeof(App).Assembly, "Resources/Themes", skin));

            var skins1 = Resources.MergedDictionaries[1];
            skins1.MergedDictionaries.Clear();
            skins1.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
            skins1.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Win10Tools;component/Resources/Themes/Theme.xaml")
            });

            Current.MainWindow?.OnApplyTemplate();
        }

        private void UpdateRegistry()
        {
            var processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null)
            {
                //var registryFilePath = $"{Path.GetDirectoryName(processModule.FileName)}\\Registry.reg";
                //if (!File.Exists(registryFilePath))
                //{
                //    var streamResourceInfo = GetResourceStream(new Uri("pack://application:,,,/Resources/Registry.txt"));
                //    if (streamResourceInfo != null)
                //    {
                //        using var reader = new StreamReader(streamResourceInfo.Stream);
                //        var registryStr = reader.ReadToEnd();
                //        var newRegistryStr = registryStr.Replace("#", processModule.FileName.Replace("\\", "\\\\"));
                //        File.WriteAllText(registryFilePath, newRegistryStr);
                //        Process.Start(new ProcessStartInfo("cmd", $"/c {registryFilePath}")
                //        {
                //            UseShellExecute = false,
                //            CreateNoWindow = true
                //        });
                //    }
                //}
            }
        }
    }
}
