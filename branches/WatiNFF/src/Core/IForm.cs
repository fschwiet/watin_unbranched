namespace WatiN.Core
{
    public interface IForm
    {
        void Submit();

        string Name { get; }
    }
}