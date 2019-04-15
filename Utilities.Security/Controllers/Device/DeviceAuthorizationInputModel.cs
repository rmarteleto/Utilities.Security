using Utilities.Security.Controllers.Consent;

namespace Utilities.Security.Controllers.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}