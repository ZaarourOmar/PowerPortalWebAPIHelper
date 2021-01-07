﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPortalWebAPIHelper.Models
{
    public enum WebAPISiteSettingTypes { EnabledSetting, FieldsSetting, InnerError }


    public enum APIOperationTypes2 { Create, Update, Delete }
    public enum APIOperationTypes { BasicCreate, BasicUpdate,UpdateSingle, BasicDelete,DeleteSingle,Associate, Disassociate }
}
