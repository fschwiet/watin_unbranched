#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
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

using WatiN.Core.Interfaces;

namespace WatiN.Core.Constraints
{
	public class ElementConstraint : BaseConstraint 
	{
		private readonly ICompareElement _comparer;

		public ElementConstraint(ICompareElement comparer)
		{
			_comparer = comparer;
		}

		protected override bool DoCompare(IAttributeBag attributeBag)
		{
			ElementAttributeBag elementAttributeBag = attributeBag as ElementAttributeBag;
			if(elementAttributeBag != null)
			{
				return _comparer.Compare(elementAttributeBag.ElementTyped);
			}
			else
			{
				throw new Exceptions.WatiNException("This Constaint class can only be used to compare against an element");
			}
		}

		public override string ConstraintToString()
		{
			return "Custom constraint";
		}
	}
}
