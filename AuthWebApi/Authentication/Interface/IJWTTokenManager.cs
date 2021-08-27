namespace AuthWebApi.Authentication.Interface
{
    public interface IJWTTokenManager
    {
        string GetToken(string userName, string passWord);
    }
}