﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.ThirdParty.UEditor.NetCore.Models.Result
{
    public class UploadResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public string OriginFileName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
