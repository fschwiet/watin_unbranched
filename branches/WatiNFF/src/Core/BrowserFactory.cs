#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Interfaces;
using WatiN.Core.Mozilla;

namespace WatiN.Core
{
    /// <summary>
    /// Represents a Browser used for running tests, it can run tests using <see cref="IE"/>, or <see cref="FireFox"/>. 
    /// </summary>
    public sealed class BrowserFactory
    {
        #region Private static fields

        private static Settings settings = new Settings();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserFactory"/> class.
        /// </summary>
        private BrowserFactory()
        {
        }

        #endregion

        #region Public static properties

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public static Settings Settings
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                settings = value;

                // #TODO remove if IE refactored and no longer has a settings property ?
                IE.Settings = settings;
            }
            get { return settings; }
        }

        #endregion Public static properties

        #region Public static methods

        /// <summary>
        /// Creates a new instance of a <see cref="IBrowser"/> either <see cref="IE"/> or <see cref="FireFox"/> based on the value of <see cref="BrowserType"/>.
        /// The default value if <see cref="BrowserType.InternetExplorer"/>.
        /// </summary>
        /// <returns></returns>
        public static IBrowser Create()
        {
            return Create(Settings.BrowserType);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IBrowser"/> for the specified browser type.
        /// </summary>
        /// <param name="browserType">Type of browser.</param>
        /// <returns></returns>
        public static IBrowser Create(BrowserType browserType)
        {
            switch(browserType)
            {
                case BrowserType.InternetExplorer:
                    return new IE();
                case BrowserType.FireFox:
                    return new FireFox();
                default:
                    throw new ArgumentOutOfRangeException("browserType");
            }
        }

        #endregion

    }
}
