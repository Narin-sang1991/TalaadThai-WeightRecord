using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public static class ConfigurationExtension
    {
        #region Email Smtp
        public static string GetMailServer(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailServer") ? string.Empty : source.Configurations["MailServer"];
        }

        public static int GetMailPort(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailPort") ? 0 : Convert.ToInt32(source.Configurations["MailPort"]);
        }
        public static bool GetMailEnabledSSL(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailEnabledSSL") ? false : Convert.ToBoolean(source.Configurations["MailEnabledSSL"]);
        }
        public static int GetMailTimeout(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailTimeout") ? 100000 : Convert.ToInt32(source.Configurations["MailTimeout"]) * 1000;
        }
        public static string GetMailLogin(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailLogin") ? string.Empty : source.Configurations["MailLogin"];
        }
        public static string GetMailPassword(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailPassword") ? string.Empty : source.Configurations["MailPassword"];
        }
        public static string GetMailName(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailName") ? string.Empty : source.Configurations["MailName"];
        }
        public static bool GetMailIsWithCredentials(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("MailIsWithCredentials") ? true : Convert.ToBoolean(source.Configurations["MailIsWithCredentials"]);
        }
        public static string GetDefaultMailContent(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("DefaultMailContent") ? string.Empty : source.Configurations["DefaultMailContent"];
        }

        public static int GetEmailConfirmationMinuteTime(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("EmailConfirmationTime") ? 60 : Convert.ToInt32(source.Configurations["EmailConfirmationTime"]);
        }
        #endregion

        #region Email POP
        public static string GetPOPMailServer(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("POPMailServer") ? string.Empty : source.Configurations["POPMailServer"];
        }

        public static int GetPOPMailPort(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("POPMailPort") ? 0 : Convert.ToInt32(source.Configurations["POPMailPort"]);
        }

        public static string GetIgnoredEmailSuffix(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("IgnoredEmailSuffix") ? string.Empty : source.Configurations["IgnoredEmailSuffix"];
        }

        public static string GetEmailRegisterConfirmTemplate(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("EmailRegisterConfirmTemplate") ? string.Empty : source.Configurations["EmailRegisterConfirmTemplate"];
        }
        #endregion

        #region Application Setting
        public static string GetSvrPathUrl(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("SvrPathUrl") ? string.Empty : source.Configurations["SvrPathUrl"];
        }

        public static string GetResetPasswordPath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("ResetPasswordPath") ? string.Empty : source.Configurations["ResetPasswordPath"];
        }
        public static string GetPrefixFileFullPath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("PrefixFileFullPath") ? string.Empty : source.Configurations["PrefixFileFullPath"];
        }
        #endregion

        #region Securities Config
        public static int GetInactiveMinuteTime(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("InactiveTime") ? 300 : Convert.ToInt32(source.Configurations["InactiveTime"]);
        }

        public static bool GetIsRequireUpperChar(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("RequireUpperChar") ? false : Convert.ToBoolean(source.Configurations["RequireUpperChar"]);
        }

        public static bool GetIsRequireLowerChar(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("RequireLowerChar") ? true : Convert.ToBoolean(source.Configurations["RequireLowerChar"]);
        }

        public static bool GetIsRequireNumeric(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("RequireNumeric") ? false : Convert.ToBoolean(source.Configurations["RequireNumeric"]);
        }

        public static bool GetIsRequireSpecialChar(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("RequireSpecialChar") ? false : Convert.ToBoolean(source.Configurations["RequireSpecialChar"]);
        }

        public static int GetPasswordLength(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("PasswordLength") ? 8 : Convert.ToInt32(source.Configurations["PasswordLength"]);
        }
        #endregion

        #region SmsService Config
        public static string GetOTPAppLogin(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPAppLogin") ? string.Empty : source.Configurations["OTPAppLogin"];
        }

        public static string GetOTPAppSecret(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPAppSecret") ? string.Empty : source.Configurations["OTPAppSecret"];
        }

        public static int GetOTPLimitTime(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPLimitTime") ? 5 : Convert.ToInt32(source.Configurations["OTPLimitTime"]);
        }

        public static string GetOTPServiceURL(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPServiceURL") ? string.Empty : source.Configurations["OTPServiceURL"];
        }

        public static string GetOTPRequestFormatPath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPRequestFormatPath") ? string.Empty : source.Configurations["OTPRequestFormatPath"];
        }

        public static string GetOTPRequestParams(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPRequestParams") ? string.Empty : source.Configurations["OTPRequestParams"];
        }

        public static string GetOTPVerifierFormatPath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPVerifierFormatPath") ? string.Empty : source.Configurations["OTPVerifierFormatPath"];
        }

        public static string GetOTPVerifierParams(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPVerifierParams") ? string.Empty : source.Configurations["OTPVerifierParams"];
        }

        public static string GetSmsServiceURL(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("SmsServiceURL") ? string.Empty : source.Configurations["SmsServiceURL"];
        }

        public static string GetSmsServiceNodePath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("SmsServiceNodePath") ? "/SMS" : source.Configurations["SmsServiceNodePath"];
        }

        public static string GetSmsServiceSubNodePath(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("SmsServiceSubNodePath") ? "/SMS/QUEUE" : source.Configurations["SmsServiceSubNodePath"];
        }

        public static int GetInternalOTPLength(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("InternalOTPLength") ? 4 : Convert.ToInt32(source.Configurations["InternalOTPLength"]);
        }

        public static int GetInternalOTPLimitValue(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("InternalOTPLimitValue") ? 10000 : Convert.ToInt32(source.Configurations["InternalOTPLimitValue"]);
        }

        public static string GetOTPStandardFormat(this IConfiguration source)
        {
            return !source.Configurations.ContainsKey("OTPStandardFormat") ? "MaMaBetting OTP : {0}. Expires in {1} minutes." : source.Configurations["OTPStandardFormat"];
        }

        #endregion

    }
}
