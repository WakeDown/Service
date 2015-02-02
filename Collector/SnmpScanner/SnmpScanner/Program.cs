using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnmpScanner.Models;

namespace SnmpScanner
{
    static class Program
    {
        public const string progVersion = "1.0";
        public static bool _threadMustStop;
        public static bool _dbIsBuzy;

        public static Settings AppSettings
        {
            get
            {
                return new Settings();
            }
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //args = new[] { "frm" };

            _threadMustStop = false;
            _dbIsBuzy = false;

            if (args.Any())
            {
                switch (args[0])
                {
                    default:
                        StartWinService();
                        break;
                    case "install":
                        SelfInstaller.InstallMe();
                        break;
                    case "uninstall":
                        SelfInstaller.UninstallMe();
                        break;
                    case "frm":
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Form1());
                        break;
                }
            }
            else
            {
                StartWinService();
            }
        }

        public static void StartWinService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new CounterService() };
            ServiceBase.Run(ServicesToRun);
        }

        public static int GetExchangeDelayValue()
        {
            int exchangeDelayVlaue = AppSettings.MinExchangeDelay;

            int currExchangeDelayVlaue;
            int.TryParse(ConfigurationManager.AppSettings["exchangeDelay"], out currExchangeDelayVlaue);

            if (exchangeDelayVlaue > currExchangeDelayVlaue)
            {
                return exchangeDelayVlaue;

            }
            else
            {
                return currExchangeDelayVlaue;
            }
        }
    }

    public static class SelfInstaller
    {
        private static readonly string _exePath =
            Assembly.GetExecutingAssembly().Location;
        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { _exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { "/u", _exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
