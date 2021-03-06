﻿using Shadowsocks.Controller;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Shadowsocks
{
    public class Utils
    {
        public static void ReleaseMemory()
        {
            // release any unused pages
            // making the numbers look good in task manager
            // this is totally nonsense in programming
            // but good for those users who care
            // making them happier with their everyday life
            // which is part of user experience
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle,
                (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        }

        public static string UnGzip(byte[] buf)
        {
            byte[] buffer = new byte[1024];
            int n;
            using (MemoryStream sb = new MemoryStream())
            {
                using (GZipStream input = new GZipStream(new MemoryStream(buf),
                    CompressionMode.Decompress, false))
                {
                    while ((n = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sb.Write(buffer, 0, n);
                    }
                }
                return System.Text.Encoding.UTF8.GetString(sb.ToArray());
            }
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
            UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        private static void Assert(bool condition)
        {
            if (!condition)
            {
                throw new Exception(I18N.GetString("assertion failure"));
            }
        }

        public static void IsSsServerInfoValid(SsServerInfo server)
        {
            Utils.IsPortValid(server.server_port);
            Utils.IsPasswordNotEmpty(server.password);
            Utils.IsServerIPNotEmpty(server.server);
            Utils.IsServerIPNotEmpty(server.method);
        }

        public static void IsPortValid(int port)
        {
            if (port <= 0 || port > 65535)
            {
                throw new ArgumentException(I18N.GetString("Port out of range"));
            }
            if (port == 8123)
            {
                throw new ArgumentException(I18N.GetString("Port is not allowed to be 8123"));
            }
        }

        public static void IsPasswordNotEmpty(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(I18N.GetString("Password can not be blank"));
            }
        }

        public static void IsServerIPNotEmpty(string server)
        {
            if (string.IsNullOrEmpty(server))
            {
                throw new ArgumentException(I18N.GetString("Server IP can not be blank"));
            }
        }

        public static void IsEncryptionNotEmpty(string encryption)
        {
            if (string.IsNullOrEmpty(encryption))
            {
                throw new ArgumentException(I18N.GetString("Encryption method can not be blank"));
            }
        }

        public static string SingleMatchGroupWithRegex(string regexWithSingleMatchGroup, string rawText)
        {
            var match = new Regex(regexWithSingleMatchGroup, RegexOptions.Singleline).Match(rawText).Groups;
            var result = match[1].ToString();
            return result;
        }

        public static KeyValuePair<string, string> SingleMatchTwoGroupsWithRegex(string regexWithSingleMatchGroup, string rawText)
        {
            var match = new Regex(regexWithSingleMatchGroup, RegexOptions.Singleline).Match(rawText).Groups;
            var result = new KeyValuePair<string, string>(match[1].ToString(), match[2].ToString());
            return result;
        }

        public static List<string> MultiMatchSingleGroupWithRegex(string regexWithSingleMatchGroup, string rawText)
        {
            var results = new List<string>();
            var matches = new Regex(regexWithSingleMatchGroup, RegexOptions.Singleline).Matches(rawText);
            foreach (Match match in matches)
            {
                results.Add(match.Groups[1].ToString());
            }

            return results;
        }
    }
}
