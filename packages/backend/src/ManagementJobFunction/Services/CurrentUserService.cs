using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementJobFunction.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; set; }

        public string SpotifyAccessToken
        {
            get
            {
                return "BQAB1txdQtIX40nP11G7gMjJSWynXUWAJmqz0N56m2Iw0Rq8WZJATt79rkagwxbg2mzkgCGXxwVyeHdjb5KWEVBajvBmFo1CUDjnig0-QDDdCS_gZxIn0QQqtDWD-UMHhCL2UoEkwoYbcPcTh8hI9g-Fy5RiQDa_kd0jcDnVVOZs9xc-UBwQYAoaDNKcSWDhbSCfP2Z2CiQQRmXSBph8KUDuJ2nykeWdPNsnnmw3Y1CaAoDEBVW4-D_TLxMsAnH1xZSoX-l7gpUX86aGpFSq";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
