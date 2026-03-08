using System;
using Server;
using Server.Accounting;

namespace Server.Misc
{
    public static class AdminPasswordReset
    {
        // ────────────────────────────────────────────────
        //   CHANGE THESE VALUES
        // ────────────────────────────────────────────────
        private const string TargetUsername = "admin";                  // Case-insensitive usually, but keep lowercase
        private const string NewPlainPassword = "frisky";   // ← CHANGE THIS
        private const bool OnlyIfNoOtherStaff = false;                  // true = skip if any other GM+ exists

        [CallPriority(5)] // Run fairly early during load
        public static void Configure()
        {
            EventSink.WorldLoad += OnWorldLoad;
        }

        private static void OnWorldLoad()
        {
            Account target = Accounts.GetAccount(TargetUsername) as Account;

            if (target == null)
            {
                // Create fresh admin/owner account
                target = new Account(TargetUsername, NewPlainPassword)
                {
                    AccessLevel = AccessLevel.Owner,
                    Banned = false
                };

                System.Console.WriteLine($"[Admin Reset] Created new owner account: {TargetUsername} / {NewPlainPassword}");
                return;
            }

            // Optional: skip if other staff-level accounts exist (safety)
            if (OnlyIfNoOtherStaff)
            {
                foreach (Account acc in Accounts.GetAccounts())
                {
                    if (acc != target && acc.AccessLevel >= AccessLevel.GameMaster)
                    {
                        System.Console.WriteLine("[Admin Reset] Skipped - other staff account(s) found.");
                        return;
                    }
                }
            }

            // Reset password safely
            target.SetPassword(NewPlainPassword);

            target.AccessLevel = AccessLevel.Owner; // enforce
            target.Banned = false;

            System.Console.WriteLine($"[Admin Reset] Password reset for '{TargetUsername}' to '{NewPlainPassword}'");
        }
    }
}