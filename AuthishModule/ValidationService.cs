using System;
using System.Configuration;
using System.Linq;

namespace AuthishModule
{
    static class ValidationService
    {
        private static readonly string AuthishPassword = ConfigurationManager.AppSettings["AuthishPassword"];
        private static readonly string[] AuthishWhitelistedPaths = (ConfigurationManager.AppSettings["AuthishWhitelistedPaths"] ?? "").Replace(" ", "").Split(',');
        private static readonly string[] AuthishWhitelistedStartOfPaths = (ConfigurationManager.AppSettings["AuthishWhitelistedStartOfPaths"] ?? "").Replace(" ", "").Split(',');


        public static bool PasswordIsCorrect(string password)
        {
            var passwordIsCorrect = AuthishPassword == "" || password == AuthishPassword;
            if (!passwordIsCorrect)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Authish logon failed, user password: '{0}', authish wants: '{1}", password, AuthishPassword));
            }

            return passwordIsCorrect;
        }

        public static bool IsAuthishPasswordMissing()
        {
            return AuthishPassword == null;
        }

        public static bool PathIsWhitelisted(string path)
        {
            var isWhitelistedPath = AuthishWhitelistedPaths.Any(p => p.Equals(path, StringComparison.InvariantCultureIgnoreCase));
            var isWhitelistedStartOfPath = AuthishWhitelistedStartOfPaths.Any(p => path.StartsWith(p, StringComparison.InvariantCultureIgnoreCase));
            return isWhitelistedPath || isWhitelistedStartOfPath;
        }
    }
}