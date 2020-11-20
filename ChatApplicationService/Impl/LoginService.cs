using ChatApplicationRepository.Infc;
using ChatApplicationService.Infc;
using ChatApplicationModel.Dto;
using ChatApplicationModel.Model;

namespace ChatApplicationService.Impl
{
    public class LoginService : ILoginService
    {
        public LoginService(ILoginRepository repository)
        {
            this.LoginRepository = repository;
        }
        public ILoginRepository LoginRepository { get; set; }

        public string GenerateJSONWebToken(int id)
        {
            return LoginRepository.GenerateJSONWebToken(id);
        }

        public User Login(LoginDto loginDto)
        {
            return LoginRepository.Login(loginDto);
        }
        public string Registration(User user)
        {
            return LoginRepository.Registration(user);
        }
    }
}
