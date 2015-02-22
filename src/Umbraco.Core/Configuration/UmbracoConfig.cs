﻿using System;
using System.Configuration;
using Umbraco.Core.Configuration.Dashboard;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;

namespace Umbraco.Core.Configuration
{
    /// <summary>
    /// The gateway to all umbraco configuration
    /// </summary>
    public class UmbracoConfig
    {
        #region Singleton

        private static readonly Lazy<UmbracoConfig> Lazy = new Lazy<UmbracoConfig>(() => new UmbracoConfig());

        public static UmbracoConfig For
        {
            get { return Lazy.Value; }            
        }

        #endregion

        /// <summary>
        /// Default constructor 
        /// </summary>
        private UmbracoConfig()
        {
            if (_umbracoSettings == null)
            {
                var umbracoSettings = ConfigurationManager.GetSection("umbracoConfiguration/settings") as IUmbracoSettingsSection;                
                SetUmbracoSettings(umbracoSettings);
            }

            if (_dashboardSection == null)
            {
                var dashboardConfig = ConfigurationManager.GetSection("umbracoConfiguration/dashBoard") as IDashboardSection;                
                SetDashboardSettings(dashboardConfig);
            }
        }

        /// <summary>
        /// Constructor - can be used for testing
        /// </summary>
        /// <param name="umbracoSettings"></param>
        /// <param name="dashboardSettings"></param>
        public UmbracoConfig(IUmbracoSettingsSection umbracoSettings, IDashboardSection dashboardSettings)
        {
            SetUmbracoSettings(umbracoSettings);
            SetDashboardSettings(dashboardSettings);
        }

        private IDashboardSection _dashboardSection;
        private IUmbracoSettingsSection _umbracoSettings;

        /// <summary>
        /// Gets the IDashboardSection
        /// </summary>
        public IDashboardSection DashboardSettings()
        {
            if (_dashboardSection == null)
            {
                var ex = new ConfigurationErrorsException("Could not load the " + typeof(IDashboardSection) + " from config file, ensure the web.config and Dashboard.config files are formatted correctly");
                LogHelper.Error<UmbracoConfig>("Config error", ex);
                throw ex;
            }

            return _dashboardSection;
        }        

        //ONLY for unit testing
        internal void SetDashboardSettings(IDashboardSection value)
        {
            _dashboardSection = value;
        }

        //ONLY for unit testing
        internal void SetUmbracoSettings(IUmbracoSettingsSection value)
        {
            _umbracoSettings = value;
        }

        /// <summary>
        /// Gets the IUmbracoSettings
        /// </summary>
        public IUmbracoSettingsSection UmbracoSettings()
        {
            if (_umbracoSettings == null)
            {
                var ex = new ConfigurationErrorsException("Could not load the " + typeof (IUmbracoSettingsSection) + " from config file, ensure the web.config and umbracoSettings.config files are formatted correctly");
                LogHelper.Error<UmbracoConfig>("Config error", ex);
                throw ex;
            }

            return _umbracoSettings;
        }
        
      

        //TODO: Add other configurations here !
    }
}