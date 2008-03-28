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

using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests
{
#if NET20
  using NUnit.Framework;
  using NUnit.Framework.SyntaxHelpers;
  using WatiN.Core.Comparers;

  [TestFixture]
  public class PredicateComparerTests
  {
    private bool _called;
    private string _value;
    private bool _returnValue;
  
    [SetUp]
    public void SetUp()
    {
      _called = false;
      _value = null;
    }
  	
    [Test]
    public void StringPredicateShouldBeCalledAndReturnTrue()
    {
      _returnValue = true;
      PredicateComparer comparer = new PredicateComparer(CallThisMethod);
  		
      Assert.That(comparer.Compare("test value"), Is.True);
      Assert.That(_called, Is.True);
      Assert.That(_value, Is.EqualTo("test value"));
    }
  	
    [Test]
    public void StringPredicateShouldBeCalledAndReturnFalse()
    {
      _returnValue = false;
      PredicateComparer comparer = new PredicateComparer(CallThisMethod);
  		
      Assert.That(comparer.Compare("some input"), Is.False);
      Assert.That(_called, Is.True);
      Assert.That(_value, Is.EqualTo("some input"));
    }

    public bool CallThisMethod(string value)
    {
      _called = true;
      _value = value;
      return _returnValue;
    }
 
    [Test]
    public void ElementPredicateShouldBeCalledAndReturnTrue()
    {
      _returnValue = true;
      PredicateComparer comparer = new PredicateComparer(CallElementCompareMethod);
  		 
      Assert.That(comparer.Compare(new Element(null,(INativeElementFinder) null)), Is.True);
      Assert.That(_called, Is.True);
    }
  	
    [Test]
    public void ElementPredicateShouldBeCalledAndReturnFalse()
    {
      _returnValue = false;
      PredicateComparer comparer = new PredicateComparer(CallElementCompareMethod);

      Assert.That(comparer.Compare(new Element(null, (INativeElementFinder)null)), Is.False);
      Assert.That(_called, Is.True);
    }

    public bool CallElementCompareMethod(Element element)
    {
    	_called = true;
    	return _returnValue;
    }
  }
#endif
}
