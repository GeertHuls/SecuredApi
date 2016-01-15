using System;
using System.Collections.Generic;

namespace IdentityServer.Services
{
    public class TwoFactorTokenService
    {
        private class TwoFactorCode
        {
            public string Code { get; set; }
            public DateTime CanBeVerifiedUntil { get; set; }
            public bool IsVerified { get; set; }

            public TwoFactorCode(string code)
            {
                Code = code;
                CanBeVerifiedUntil = DateTime.Now.AddMinutes(5);
                IsVerified = false;
            }
        }

        private static readonly Dictionary<string, TwoFactorCode> TwoFactorCodeDictionary
            = new Dictionary<string, TwoFactorCode>();

        public void GenerateTwoFactorCodeFor(string subject)
        {
            const string dummyCode = "123";
            var twoFactorCode = new TwoFactorCode(dummyCode);

            TwoFactorCodeDictionary[subject] = twoFactorCode;
        }

        public bool VerifyTwoFactorCodeFor(string subject, string code)
        {
            TwoFactorCode twoFactorCodeFromDictionary;

            if (!TwoFactorCodeDictionary.TryGetValue(subject, out twoFactorCodeFromDictionary)) return false;

            if (twoFactorCodeFromDictionary.CanBeVerifiedUntil <= DateTime.Now ||
                twoFactorCodeFromDictionary.Code != code) return false;

            twoFactorCodeFromDictionary.IsVerified = true;
            return true;
        }

        public bool HasVerifiedTwoFactorCode(string subject)
        {
            TwoFactorCode twoFactorCodeFromDictionary;

            return TwoFactorCodeDictionary
                .TryGetValue(subject, out twoFactorCodeFromDictionary) && twoFactorCodeFromDictionary.IsVerified;
        }
    }
}