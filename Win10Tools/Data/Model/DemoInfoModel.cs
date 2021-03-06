﻿using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Win10Tools.Data
{
    public class DemoInfoModel : ViewModelBase
    {
        public string Key { get; set; }

        private string _title;

        public string Title
        {
            get => _title;
#if netle40
            set => Set(nameof(Title), ref _title, value);
#else
            set => Set(ref _title, value);
#endif
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
#if netle40
            set => Set(nameof(SelectedIndex), ref _selectedIndex, value);
#else
            set => Set(ref _selectedIndex, value);
#endif   
        }

        public IList<DemoItemModel> DemoItemList { get; set; }
    }
}