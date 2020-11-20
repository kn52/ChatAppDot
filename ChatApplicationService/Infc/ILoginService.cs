using ChatApplicationModel.Dto;
using ChatApplicationModel.Model;

namespace ChatApplicationService.Infc
{
    public interface ILoginService
    {
        User Login(LoginDto loginDto);
        string Registration(User user);
        string GenerateJSONWebToken(int id);
    }
}
