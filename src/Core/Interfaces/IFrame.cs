
namespace WatiN.Core.Interfaces
{
    public interface IFrame : IDocument, IAttributeBag
    {
        string Name { get; }

        string Id { get; }

        string GetAttributeValue(string attributename);
    }
}