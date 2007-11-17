
using System;   
using System.Configuration;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
	public class ElementFinder
	{
		private string _tagname;
		private AttributeConstraint _constraint;
		private FireFoxClientPort _clientPort;
		
		public ElementFinder(String tagname, AttributeConstraint constraint, FireFoxClientPort clientPort)
		{
			_tagname = tagname;
			if (UtilityClass.IsNullOrEmpty(_tagname))
			{
				_tagname = "*";
			}
			_constraint = constraint;
			_clientPort = clientPort;
		}
		
		/// <summary>
		/// Finds the first element that matches the given constaints
		/// </summary>
		/// <returns>A javascript variable name with a reference to the matching element, or null of no match is found.</returns>
		public string FindFirst()
		{
			string elementArrayName = FireFoxClientPort.CreateVariableName();
			
			string command = string.Format("{0} = {1}.getElementsByTagName(\"{2}\"); ", elementArrayName, FireFoxClientPort.DocumentVariableName, _tagname);
			command = command + string.Format("{0}.length;", elementArrayName);			
        	_clientPort.Write(command);

			int numberOfElements = int.Parse(_clientPort.LastResponse);
			
			for (int index = 0; index < numberOfElements; index++)
			{	            
				string indexedElementVariableName = string.Format("{0}[{1}]", elementArrayName, index);
	            FireFoxElementAttributeBag attributebag = new FireFoxElementAttributeBag(indexedElementVariableName, _clientPort);
	            if (_constraint.Compare(attributebag))
	            {
	            	string elementVariableName = FireFoxClientPort.CreateVariableName();
					command = string.Format("{0}={1};", elementVariableName, indexedElementVariableName);
		        	_clientPort.Write(command);
		        	
					return elementVariableName;
	            }
			}
			
			return null;
		}
	}
	
	public class FireFoxElementAttributeBag : IAttributeBag
	{
		private Element element;
		
		public FireFoxElementAttributeBag(string elementVariable, FireFoxClientPort clientPort)
		{
			element = new Element(elementVariable, clientPort);
		}	
		
		public string GetValue(string attributename)
		{
			return element.GetAttributeValue(attributename);
		}
	}
}

