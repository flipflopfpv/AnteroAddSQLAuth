using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace AllMax.Antero
{
    // Token: 0x02000103 RID: 259
    public static class RegistryValues
    {
        // Token: 0x1700014F RID: 335
        // (get) Token: 0x060008AE RID: 2222 RVA: 0x00048AA0 File Offset: 0x00046CA0
        // (set) Token: 0x060008AF RID: 2223 RVA: 0x00048B08 File Offset: 0x00046D08
        public static string AnteroDbLocation
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        return registryKey.GetValue("AnteroDbLocation", "").ToString();
                    }
                }
                catch
                {
                }
                return "";
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        registryKey.SetValue("AnteroDbLocation", value, RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }

        // Token: 0x17000150 RID: 336
        // (get) Token: 0x060008B0 RID: 2224 RVA: 0x00048B60 File Offset: 0x00046D60
        // (set) Token: 0x060008B1 RID: 2225 RVA: 0x00048BC8 File Offset: 0x00046DC8
        public static string AnteroDbName
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        return registryKey.GetValue("AnteroDbName", "").ToString();
                    }
                }
                catch
                {
                }
                return "";
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        registryKey.SetValue("AnteroDbName", value, RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }

        // Token: 0x17000151 RID: 337
        // (get) Token: 0x060008B2 RID: 2226 RVA: 0x00048C20 File Offset: 0x00046E20
        // (set) Token: 0x060008B3 RID: 2227 RVA: 0x00048C8C File Offset: 0x00046E8C
        public static bool WOCompVerifyPrompt
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Options"))
                    {
                        return registryKey.GetValue("WOCompVerifyPrompt", "1").ToString().Equals("1");
                    }
                }
                catch
                {
                }
                return true;
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Options"))
                    {
                        registryKey.SetValue("WOCompVerifyPrompt", value ? "1" : "0", RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }

        // Token: 0x17000152 RID: 338
        // (get) Token: 0x060008B4 RID: 2228 RVA: 0x00048CF4 File Offset: 0x00046EF4
        // (set) Token: 0x060008B5 RID: 2229 RVA: 0x00048D60 File Offset: 0x00046F60
        public static bool CheckForUpdates
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Options"))
                    {
                        return registryKey.GetValue("CheckForUpdates", "1").ToString().Equals("1");
                    }
                }
                catch
                {
                }
                return true;
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Options"))
                    {
                        registryKey.SetValue("CheckForUpdates", value ? "1" : "0", RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }

        // Token: 0x17000153 RID: 339
        // (get) Token: 0x060008B6 RID: 2230 RVA: 0x00048DC8 File Offset: 0x00046FC8
        // (set) Token: 0x060008B7 RID: 2231 RVA: 0x00048E34 File Offset: 0x00047034
        public static bool UseExceptionReporting
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Options"))
                    {
                        return registryKey.GetValue("ExceptionReporting", "1").ToString().Equals("1");
                    }
                }
                catch
                {
                }
                return true;
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Options"))
                    {
                        registryKey.SetValue("ExceptionReporting", value ? "1" : "0", RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }
        //Added by Justin Oberdorf
        public static string AnteroDbUsername
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        return registryKey.GetValue("AnteroDbUsername", "").ToString();
                    }
                }
                catch
                {
                }
                return "";
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        registryKey.SetValue("AnteroDbUsername", value, RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }
        public static string AnteroDbPassword
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        return DecryptString(registryKey.GetValue("AnteroDbPassword", "").ToString(), "justinoberdorf");
                    }
                }
                catch
                {
                }
                return "";
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        registryKey.SetValue("AnteroDbPassword", EncryptString(value,"justinoberdorf"), RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }
        // Token: 0x1700016C RID: 364
        // (get) Token: 0x060009FA RID: 2554 RVA: 0x000542FC File Offset: 0x000524FC
        // (set) Token: 0x060009FB RID: 2555 RVA: 0x00054368 File Offset: 0x00052568
        public static bool AnteroDbSpecifyAuth
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        return registryKey.GetValue("AnteroDbSpecifyAuth", "1").ToString().Equals("1");
                    }
                }
                catch
                {
                }
                return true;
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        registryKey.SetValue("AnteroDbSpecifyAuth", value ? "1" : "0", RegistryValueKind.String);
                    }
                }
                catch
                {
                }
            }
        }
        public static UInt16 AnteroDbPort
        {
            get
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        return Convert.ToUInt16(registryKey.GetValue("AnteroDbPort", 1434));
                    }
                }
                catch
                {
                }
                return 1433;
            }
            set
            {
                try
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\AllMaxSoftware\\Antero\\Main"))
                    {
                        registryKey.SetValue("AnteroDbPort", value, RegistryValueKind.DWord);
                    }
                }
                catch
                {
                }
            }
        }
        private static string DecryptString(string Message, string Passphrase)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                UTF8Encoding utf8Encoding = new UTF8Encoding();
                MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
                byte[] key = md5CryptoServiceProvider.ComputeHash(utf8Encoding.GetBytes(Passphrase));
                TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
                tripleDESCryptoServiceProvider.Key = key;
                tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
                tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
                byte[] array = Convert.FromBase64String(Message);
                byte[] bytes;
                try
                {
                    bytes = tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
                }
                finally
                {
                    tripleDESCryptoServiceProvider.Clear();
                    md5CryptoServiceProvider.Clear();
                }
                return utf8Encoding.GetString(bytes);
            }
            return null;
        }
        private static string EncryptString(string Message, string Passphrase)
        {
            UTF8Encoding utf8Encoding = new UTF8Encoding();
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] key = md5CryptoServiceProvider.ComputeHash(utf8Encoding.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = key;
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
            byte[] bytes = utf8Encoding.GetBytes(Message);
            byte[] inArray;
            try
            {
                inArray = tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            }
            finally
            {
                tripleDESCryptoServiceProvider.Clear();
                md5CryptoServiceProvider.Clear();
            }
            return Convert.ToBase64String(inArray);
        }
    }
}
