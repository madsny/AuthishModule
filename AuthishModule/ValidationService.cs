using System.Configuration;
using log4net;

namespace AuthishModule
{
    static class ValidationService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthishHandler));
        private static readonly string AuthishPassword = ConfigurationManager.AppSettings["AuthishPassword"];

        public static bool PasswordIsCorrect(string password)
        {
            var passwordIsCorrect = !string.IsNullOrEmpty(password) && password == AuthishPassword;
            if (!passwordIsCorrect)
            {
                Log.Info(string.Format("Authish logon failed, user password: '{0}', authish wants: '{1}", password, AuthishPassword));
            }

            return passwordIsCorrect;
        }

        public static bool IsAuthishPasswordMissing()
        {
            return string.IsNullOrEmpty(AuthishPassword);
        }
    }
}