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

namespace WatiN.Core.Interfaces
{
    /// <summary>
    /// Defines behaviour specific to an HTML button
    /// </summary>
    public interface IButton : IElement
    {
        /// <summary>
        /// The text displayed at the button.
        /// </summary>
        /// <value>The displayed text.</value>
        string Value { get; }

        /// <summary>
        /// The text displayed at the button (alias for the Value property).
        /// </summary>
        /// <value>The displayed text.</value>
        new string Text { get; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        string ToString();
    }
}