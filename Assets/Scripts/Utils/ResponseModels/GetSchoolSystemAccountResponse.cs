using Modules.SchoolSystem.View.DataModels.Interfaces;
using Utils.ResponseModels.Enums;

namespace Utils.ResponseModels
{
    public class GetSchoolSystemAccountResponse
    {
        public string ErrorDescription { get; set; } = string.Empty;
        public StatusCode StatusCode { get; set; }
        public ISchoolSystemAccount Data { get; set; }
    }
}