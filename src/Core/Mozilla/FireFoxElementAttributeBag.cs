using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    public class FireFoxElementAttributeBag : IAttributeBag
    {
        private readonly Element element;
		
        public FireFoxElementAttributeBag(string elementVariable, FireFoxClientPort clientPort)
        {
            this.element = new Element(elementVariable, clientPort);
        }	
		
        public string GetValue(string attributename)
        {
            return this.element.GetAttributeValue(attributename);
        }
    }
}