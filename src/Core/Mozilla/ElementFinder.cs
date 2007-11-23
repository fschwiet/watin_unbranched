
using System;   
using System.Configuration;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
	public class ElementFinder
	{
		private readonly string tagName;
		private readonly string type;
		private readonly AttributeConstraint constraint;
		private readonly FireFoxClientPort clientPort;
		
		public ElementFinder(string tagname, AttributeConstraint constraint, FireFoxClientPort clientPort) : this (tagname, null, constraint, clientPort)
		{}
		
		public ElementFinder(string tagname, string type, AttributeConstraint constraint, FireFoxClientPort clientPort)
		{
			this.type = type;
			this.tagName = tagname;
            if (UtilityClass.IsNullOrEmpty(this.tagName))
			{
                this.tagName = "*";
			}
			this.constraint = constraint;
			this.clientPort = clientPort;
		}

		/// <summary>
		/// Finds the first element that matches the given constaints
		/// </summary>
		/// <returns>A javascript variable name with a reference to the matching element, or null of no match is found.</returns>
		public string FindFirst()
		{
			string elementArrayName = FireFoxClientPort.CreateVariableName();

            string command = string.Format("{0} = {1}.getElementsByTagName(\"{2}\"); ", elementArrayName, FireFoxClientPort.DocumentVariableName, this.tagName);
            if (this.type != null)
            {
            	string typeArrayName = FireFoxClientPort.CreateVariableName();
            	command = command + string.Format("{0} = new Array();", typeArrayName);
            	command = command + string.Format("for(i=0;i<{0}.length;i++)", elementArrayName);
                command = command + "{";
     		  	command = command + string.Format("if({0}[i].type.toLowerCase() ==\"{1}\")", elementArrayName, this.type.ToLower());
                command = command + "{";
            	command = command + string.Format("{0}.push({1}[i]);", typeArrayName, elementArrayName);
                command = command + "}}";
            	command = command + string.Format("{0} = {1};", elementArrayName, typeArrayName);
            	command = command + string.Format("{0} = null;", typeArrayName);
            }
            command = command + string.Format("{0}.length;", elementArrayName);
            Console.WriteLine(command);
            this.clientPort.Write(command);

            int numberOfElements = int.Parse(this.clientPort.LastResponse);
			
			for (int index = 0; index < numberOfElements; index++)
			{	            
				string indexedElementVariableName = string.Format("{0}[{1}]", elementArrayName, index);
                FireFoxElementAttributeBag attributebag = new FireFoxElementAttributeBag(indexedElementVariableName, this.clientPort);
                if (this.constraint.Compare(attributebag))
	            {
	            	string elementVariableName = FireFoxClientPort.CreateVariableName();
					command = string.Format("{0}={1};", elementVariableName, indexedElementVariableName);
                    this.clientPort.Write(command);
		        	
					return elementVariableName;
	            }
			}
			
			return null;
		}
	}
}

