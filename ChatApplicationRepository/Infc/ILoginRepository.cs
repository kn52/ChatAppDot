using ChatApplicationModel.Dto;
using ChatApplicationModel.Model;

namespace ChatApplicationRepository.Infc
{
    public interface ILoginRepository
    {
        User Login(LoginDto loginDto);
        string Registration(User user);
        string GenerateJSONWebToken(int id);
    }
}
