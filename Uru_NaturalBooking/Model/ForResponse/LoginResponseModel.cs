using Domain;
using System;


namespace Model.ForResponse
{
    public class LoginResponseModel: ModelBaseForResponse<UserSession, LoginResponseModel>
    {
        public string UserName { get; set; }
        public string Mail { get; set; }
        public Guid Id { get; set; }
        public string Token { get; set; }

        public Guid IdUser { get; set; }

        protected override LoginResponseModel SetModel(UserSession entity)
        {
            Id = entity.Id;
            UserName = entity.User.UserName;
            Mail = entity.User.Mail;
            Token = entity.Token;
            IdUser = entity.User.Id;
            return this;
        }

        public override bool Equals(object obj)
        {
            return obj is LoginResponseModel response &&
                   UserName.Equals(response.UserName) &&
                   Mail.Equals(response.Mail)&&
                   Token.Equals(response.Token);
        }
    }
}
