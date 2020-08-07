using System;
using System.Runtime.InteropServices;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using HandyControl.Data;
using Win10Tools.Tools;
using Win10Tools.UserControl;
using Win10Tools.Window;

namespace Win10Tools.ViewModel
{
    public class SetIMEViewModel
    {
        private readonly string _token;

        public SetIMEViewModel()
        {

        }

        public SetIMEViewModel(string token)
        {
            _token = token;
        }

        #region Window

        public RelayCommand ChangeIMEOrder => new Lazy<RelayCommand>(() =>
            new RelayCommand(() =>
            {
                //Growl.Info(Langs.Lang.GrowlInfo, _token);
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Registry Code
                    //Dialog.Show(new TextDialog());

                    string path = @"Keyboard Layout\Preload";
                    string key1 = "1";
                    string value1 = "00000409";
                    string key2 = "2";
                    string value2 = "00000804";

                    //获取注册表值（32位和64位都可以）
                    //string value = RegUtil.GetRegistryValue(path, key);
                    //修改注册表
                    if (RegUtil.SetRegistryKey("HKEY_CURRENT_USER", path, key1, value1))
                    {
                        if (RegUtil.SetRegistryKey("HKEY_CURRENT_USER", path, key2, value2))
                        {
                             Dialog.Show(new TextDialog("操作成功！"));
                        }
                    }
                    

                }
            }
            )).Value;

        #endregion


        //public RelayCommand NewWindowCmd => new Lazy<RelayCommand>(() =>
        //    new RelayCommand(() => new GrowlDemoWindow
        //    {
        //        Owner = Application.Current.MainWindow
        //    }.Show())).Value;
    }
}