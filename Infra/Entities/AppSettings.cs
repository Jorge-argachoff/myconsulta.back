﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Entities
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int ExpiracaoHoras { get; set; }

        public string Emissor { get; set; }

        public string ValidoEm { get; set; }
    }
}
