﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WatiN.Core.Constraints;

namespace WatiN.Core
{
    /// <summary>
    /// Expresses an algorithm for finding elements.
    /// </summary>
    public abstract class ElementFinder
    {
        private readonly IList<ElementTag> elementTags;
        private readonly BaseConstraint findBy;

        /// <summary>
        /// Creates an element finder.
        /// </summary>
        /// <param name="elementTags">The element tags considered by the finder, or null if all tags considered</param>
        /// <param name="findBy">The constraint used by the finder to filter elements, or null if no additional constraint</param>
        protected ElementFinder(IList<ElementTag> elementTags, BaseConstraint findBy)
        {
            this.elementTags = elementTags ?? new[] { ElementTag.Any };
            this.findBy = findBy ?? new AlwaysTrueConstraint();
        }

        /// <summary>
        /// Returns true if there exists at least one element that matches the finder's constraint.
        /// </summary>
        /// <returns>True if there is at least one matching element</returns>
        public bool Exists()
        {
            return FindFirst() != null;
        }

        /// <summary>
        /// Finds the first element that matches the finder's constraint.
        /// </summary>
        /// <returns>The first matching element, or null if none</returns>
        public Element FindFirst()
        {
            foreach (var element in FindAll())
                return element;
            return null;
        }

        /// <summary>
        /// Finds all elements that match the finder's constraint.
        /// </summary>
        /// <returns>An enumeration of all matching elements</returns>
        public IEnumerable<Element> FindAll()
        {
            Constraint.Reset();
            return FindAllImpl();
        }

        /// <summary>
        /// Creates a new finder filtered by an additional constraint.
        /// </summary>
        /// <param name="constraint">The additional constraint</param>
        /// <returns>The filtered element finder</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="constraint"/> is null</exception>
        public ElementFinder Filter(BaseConstraint constraint)
        {
            if (constraint == null)
                throw new ArgumentNullException("constraint");

            return FilterImpl(constraint);
        }

        /// <summary>
        /// Gets the read-only list of tags considered by the finder.
        /// </summary>
        public IList<ElementTag> ElementTags
        {
            get { return new ReadOnlyCollection<ElementTag>(elementTags); }
        }

        /// <summary>
        /// Gets the constraint used by the finder to filter elements.
        /// </summary>
        public BaseConstraint Constraint
        {
            get { return findBy; }
        }

        /// <summary>
        /// Returns a string representation of the element tags.
        /// </summary>
        public string ElementTagsToString()
        {
            return ElementTag.ElementTagsToString(elementTags);
        }

        /// <summary>
        /// Returns a string representation of the constraint.
        /// </summary>
        public string ConstraintToString()
        {
            return findBy.ConstraintToString();
        }

        /// <summary>
        /// Creates a new finder filtered by an additional constraint.
        /// </summary>
        /// <param name="findBy">The additional constraint, not null</param>
        /// <returns>The filtered element finder</returns>
        protected abstract ElementFinder FilterImpl(BaseConstraint findBy);

        /// <summary>
        /// Finds all elements that match the finder's constraint.
        /// </summary>
        /// <remarks>
        /// The constraint is automatically reset before this method is called.
        /// </remarks>
        /// <returns>An enumeration of all matching elements</returns>
        protected abstract IEnumerable<Element> FindAllImpl();
    }
}