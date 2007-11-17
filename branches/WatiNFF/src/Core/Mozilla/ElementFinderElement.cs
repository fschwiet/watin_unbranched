
using System;   
using System.Configuration;
using WatiN.Core;

namespace WatiN.Core.Mozilla
{
	public class ElementFinder
	{
		private string _tagname;
		private AttributeConstraint _constaint;
		private FireFoxClientPort _clientPort;
		
		public ElementFinder(String tagname, AttributeConstraint constraint, FireFoxClientPort clientPort)
		{
			_tagname = tagname;
			_constraint = constraint;
			_clientPort = clientPort;
		}
		
		public Element FindFirst()
		{
			string command = string.Format("{0}._elements = {0}.document.getElementsByTagName(\"{1}\"); ", FireFoxClientPort.DocumentVariableName, _tagname);
			command = command + string.Format("{0}._elementindex = 0; ", FireFoxClientPort.DocumentVariableName);
			command = command + string.Format("return {0}._elements.length; ", FireFoxClientPort.DocumentVariableName);

            this.ClientPort.Write(getAttributeWrite);
            int numberOfElements = int.Parse(this.clientPort.LastResponse);

            for(int index = 0; index < numberOfElements; index++)
            {
	            string getAttributeWrite = string.Format("{0}.getAttribute(\"{1}\");", this.LocationToElementSuffix, attributeName);
	            this.ClientPort.Write(getAttributeWrite);
	
	            string attributeValue = null;
	            if (!this.ClientPort.LastResponseIsNull)
	            {
	                attributeValue = this.ClientPort.LastResponse;
	            }
	            
	            Console.WriteLine(attributeValue);
            }
			return null;
		}
	}
}

