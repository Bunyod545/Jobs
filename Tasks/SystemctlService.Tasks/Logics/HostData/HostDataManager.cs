using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemctlService.Tasks.Logics.HostData.Models;
using Jobs.Tasks.Common.Helpers;
using Newtonsoft.Json;

namespace SystemctlService.Tasks.Logics.HostData
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// 
        /// </summary>
        public static string HostsPath => Path.Combine(AppData, "HostData");

        /// <summary>
        /// 
        /// </summary>
        public static string HostsFile => Path.Combine(HostsPath, "hosts.data");

        /// <summary>
        /// 
        /// </summary>
        static HostDataManager()
        {
            var directory = new DirectoryInfo(HostsPath);
            if (!directory.Exists)
                directory.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public static HostLoginInfo GetHostLoginInfo(string host, string login)
        {
            return GetHostInfo(host)?.LoginInfos?.FirstOrDefault(f => f.Login == login);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static List<HostLoginInfo> GetHostLoginsInfos(string host)
        {
            return GetHostInfo(host)?.LoginInfos ?? new List<HostLoginInfo>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static HostInfo GetHostInfo(string host)
        {
            return GetHostsInfos().FirstOrDefault(f => f.Host == host);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public static void SubmitHostInfo(HostSubmitInfo info)
        {
            var hosts = GetHostsInfos();
            var host = hosts.FirstOrDefault(f => f.Host == info.Host);

            if (host == null)
            {
                host = new HostInfo();
                host.Host = info.Host;
                host.LoginInfos = new List<HostLoginInfo>();
                hosts.Add(host);
            }

            var login = host.LoginInfos.FirstOrDefault(f => f.Login == info.Login);
            if (login == null)
            {
                login = new HostLoginInfo();
                login.Login = info.Login;
                host.LoginInfos.Add(login);
            }

            login.Password = info.Password;
            SaveHostsInfos(hosts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<HostInfo> GetHostsInfos()
        {
            if (!File.Exists(HostsFile))
                return new List<HostInfo>();

            var encryptedJson = File.ReadAllText(HostsFile);
            if (string.IsNullOrEmpty(encryptedJson))
                return new List<HostInfo>();

            var json = EncrytionHelper.Decrypt(encryptedJson);
            return JsonConvert.DeserializeObject<List<HostInfo>>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hosts"></param>
        public static void SaveHostsInfos(List<HostInfo> hosts)
        {
            var json = JsonConvert.SerializeObject(hosts);
            var encryptedJson = EncrytionHelper.Encrypt(json);

            File.WriteAllText(HostsFile, encryptedJson);
        }
    }
}
