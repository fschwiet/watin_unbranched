namespace WatiN.Core.Interfaces
{
    public interface IForm : IElement
    {
        void Submit();

        string Name { get; }
    }
}