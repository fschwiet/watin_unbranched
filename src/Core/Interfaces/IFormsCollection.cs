namespace WatiN.Core.Interfaces
{
    public interface IFormsCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Form"/> at the specified index.
        /// </summary>
        /// <value></value>
        IForm this[int index] { get; }

        IFormsCollection Filter(AttributeConstraint findBy);
    }
}