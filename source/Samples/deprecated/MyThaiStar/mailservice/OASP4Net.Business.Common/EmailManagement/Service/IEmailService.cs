using System.Threading.Tasks;
using OASP4Net.Business.Common.EmailManagement.Dto;

namespace OASP4Net.Business.Common.EmailManagement.Service
{
    public interface IEmailService
    {
        Task<bool> Send(EmailDto emailDto);
    }
}
