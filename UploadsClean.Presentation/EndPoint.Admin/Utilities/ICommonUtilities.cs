
using EndPoint.Admin.Models.Role;

namespace UploadClean.Application.Service
{
    public interface ICommonUtilities
    {
        public IList<ActionAndControllerName> AreaAndActionAndControllerNamesList();
        public IList<string> GetAllAreasNames();
        public string DataBaseRoleValidationGuid();
    }
}
