﻿using System.Collections.Generic;


namespace Win10Tools.Data
{
    public class DemoDataModel
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public string Remark { get; set; }

        public DemoType Type { get; set; }

        public string ImgPath { get; set; }

        public List<DemoDataModel> DataList { get; set; }
    }
}