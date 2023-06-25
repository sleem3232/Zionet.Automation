namespace Zionet.Automation.Framework.Common.Enums.Auth0
{
    public class Auth0Enums
    {
        public enum Auth0Type
        {
            Google,
            Facebook,
            Email,
        }

        public enum input
        {
            Email,
            Password
        }

        public enum Auth0Buttons
        {
            Continue,
            SignUp
        }

        public enum Login_Email
        {
            InputEmail,
            InputEmailFake
        }

        public enum Login_Password
        {
            InputPassword,
            InputPasswordUc,
            InputPasswordLc,
            InputPasswordEmpty

        }
        public enum SignUp_Email
        {
            InputEmailNew,
        }

        public enum SignUp_Password
        {
            InputPasswordUperCase,
            InputPasswordLowerCase,
            InputPasswordNumbers,
            InputPasswordSpecialCharacters,
            InputPasswordMix
        }
    }
}
