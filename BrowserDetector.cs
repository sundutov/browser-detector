public static class BrowserDetector
    {
        public static BrowserInfo GetBrowserInfo(string userAgent)
        {
            Ensure.IsNotNullOrWhiteSpace(userAgent, nameof(userAgent));

            var browserInfo = new BrowserInfo();
            var splitedUserAgentString = userAgent.Split(' ');

            var platformInfo = userAgent.Substring(userAgent.IndexOf("(", StringComparison.Ordinal) + 1, userAgent.IndexOf(")", StringComparison.Ordinal) - userAgent.IndexOf("(", StringComparison.Ordinal));
            var splittedPlatformInfo = platformInfo.Split(';');

            #region Browser detection

            if (userAgent.Contains("Edge"))
            {
                browserInfo.BrowserName = "Edge";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Edge");
            }
            else if (userAgent.Contains("BlackBerry") || userAgent.Contains("BB"))
            {
                browserInfo.BrowserName = "BlackBerry browser";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Version");
            }
            else if (userAgent.Contains("Surf"))
            {
                browserInfo.BrowserName = "Surf";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Surf");
            }
            else if (userAgent.Contains("Camino"))
            {
                browserInfo.BrowserName = "Camino";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Camino");
            }
            else if (userAgent.Contains("K-Meleon"))
            {
                browserInfo.BrowserName = "K-Meleon";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "K-Meleon");
            }
            else if (userAgent.Contains("PaleMoon"))
            {
                browserInfo.BrowserName = "Pale Moon";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "PaleMoon");
            }
            else if (userAgent.Contains("OPR"))
            {
                browserInfo.BrowserName = "Opera";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "OPR");
            }
            else if (userAgent.Contains("Opera"))
            {
                browserInfo.BrowserName = "Opera";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Version");
            }
            else if (userAgent.Contains("Firefox"))
            {
                browserInfo.BrowserName = "Firefox";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Firefox");
            }
            else if (userAgent.Contains("Trident"))
            {
                browserInfo.BrowserName = "IE";
                browserInfo.BrowserVersion = "11.0";
                if (userAgent.Contains("MSIE")) browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "MSIE", " ");
            }
            else if (userAgent.Contains("Silk"))
            {
                browserInfo.BrowserName = "Silk";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Silk");
            }
            else if (userAgent.Contains("SeaMonkey"))
            {
                browserInfo.BrowserName = "SeaMonkey";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "SeaMonkey");
            }
            else if (userAgent.Contains("Chrome"))
            {
                browserInfo.BrowserName = "Chrome";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Chrome");
            }
            else if (userAgent.Contains("Maxthon"))
            {
                browserInfo.BrowserName = "Maxthon";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Maxthon");
            }
            else if (userAgent.Contains("Vivaldi"))
            {
                browserInfo.BrowserName = "Vivaldi";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Vivaldi");
            }
            else if (userAgent.Contains("Safari"))
            {
                browserInfo.BrowserName = "Safari";
                browserInfo.BrowserVersion = GetBrowserVersion(splitedUserAgentString, "Version");
            }

            #endregion

            #region Platfrom detection

            if (userAgent.Contains("Linux")) browserInfo.Platform = "Linux";
            if (userAgent.Contains("Android")) browserInfo.Platform = "Android";
            if (userAgent.Contains("BlackBerry") || userAgent.Contains("BB")) browserInfo.Platform = "BlackBerry";
            if (userAgent.Contains("Windows Phone")) browserInfo.Platform = "Windows Phone";
            if (userAgent.Contains("Mac OS")) browserInfo.Platform = "Mac OS";
            if (userAgent.Contains("Windows NT 5.1") || userAgent.Contains("Windows NT 5.2")) browserInfo.Platform = "Windows XP";
            if (userAgent.Contains("Windows NT 6.0")) browserInfo.Platform = "Windows Vista";
            if (userAgent.Contains("Windows NT 6.1")) browserInfo.Platform = "Windows 7";
            if (userAgent.Contains("Windows NT 6.2")) browserInfo.Platform = "Windows 8";
            if (userAgent.Contains("Windows NT 6.3")) browserInfo.Platform = "Windows 8.1";
            if (userAgent.Contains("Windows NT 10")) browserInfo.Platform = "Windows 10";
            if (userAgent.Contains("FreeBSD")) browserInfo.Platform = "FreeBSD";
            if (userAgent.Contains("OpenBSD")) browserInfo.Platform = "OpenBSD";
            if (userAgent.Contains("NetBSD")) browserInfo.Platform = "NetBSD";
            if (userAgent.Contains("SunOS")) browserInfo.Platform = "SunOS";
            if (userAgent.Contains("iPad")) browserInfo.Platform = "iPad";
            if (userAgent.Contains("iPod")) browserInfo.Platform = "iPod";
            if (userAgent.Contains("iPhone")) browserInfo.Platform = "iPhone";

            if (browserInfo.Platform == "iPad" || browserInfo.Platform == "iPod" || browserInfo.Platform == "iPhone")
                browserInfo.Platform += " " + platformInfo.Replace("_", ".").Split().FirstOrDefault(x => x.Contains('.'));

            if (browserInfo.Platform == "Android") browserInfo.Platform = splittedPlatformInfo.FirstOrDefault(x => x.Contains("Android"));

            if (browserInfo.Platform == "Windows Phone") browserInfo.Platform = splittedPlatformInfo.FirstOrDefault(x => x.Contains("Windows Phone"));

            #endregion

            #region Platform Bitness detection

            browserInfo.PlatformBitness = "32-bit";

            if (userAgent.Contains("WOW64") || userAgent.Contains("x86_64") || userAgent.Contains("Win64") || userAgent.Contains("IA64") || userAgent.Contains("amd64"))
                browserInfo.PlatformBitness = "64-bit";

            if (browserInfo.Platform == "Mac OS")
            {
                var value = splittedPlatformInfo.FirstOrDefault(x => x.Contains("Mac OS"));

                browserInfo.Platform = value.Replace("_", ".");
                var macOSVersion = double.Parse(platformInfo.Split().FirstOrDefault(x => x.Contains(".")));

                if (macOSVersion >= 10.8)
                {
                    browserInfo.PlatformBitness = "64-bit";
                }
            }

            #endregion

            return browserInfo;
        }

        private static string GetBrowserVersion(string[] splitedUserAgentString, string versionDetector, string separator = "/")
        {
            var browserNameAndVersion = splitedUserAgentString.FirstOrDefault(x => x.Contains(versionDetector));
            return browserNameAndVersion?.Substring(browserNameAndVersion.IndexOf(separator, StringComparison.Ordinal) + 1);
        }
    }

    public sealed class BrowserInfo
    {
        public string BrowserName { get; set; } = "n/a";
        public string BrowserVersion { get; set; } = "n/a";
        public string Platform { get; set; } = "n/a";
        public string PlatformBitness { get; set; } = "n/a";

        public override string ToString() => $"{BrowserName} {BrowserVersion} ({Platform} {PlatformBitness})";
    }
