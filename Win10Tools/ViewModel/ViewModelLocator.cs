using System;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Win10Tools.Service;

namespace Win10Tools.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<DataService>();
            var dataService = SimpleIoc.Default.GetInstance<DataService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register(() => new SetIMEViewModel(), "SetIME");
        }

        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() =>
            Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public SetIMEViewModel SetIME => SimpleIoc.Default.GetInstance<SetIMEViewModel>("SetIME");
    }
}
