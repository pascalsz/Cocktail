//====================================================================================================================
//Copyright (c) 2012 IdeaBlade
//====================================================================================================================
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
//and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//====================================================================================================================
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
//the Software.
//====================================================================================================================
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
//OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================

using System.ComponentModel.Composition;
using IdeaBlade.EntityModel;
using IdeaBlade.Validation;

namespace Cocktail
{
    /// <summary>Internal use.</summary>
    [InheritedExport]
    public abstract class EntityManagerInterceptor
    {
    }

    /// <summary>Provides the means to safely and conveniently handle events from an EntityManager without risking memory-leaks.</summary>
    /// <typeparam name="T">The EntityManager type for which the events should be intercepted.</typeparam>
    /// <remarks>To handle events, create a new class extending EntityManagerInterceptor&lt;T&gt; and override the respective method. Multiple
    /// EntityManagerInterceptors are supported.</remarks>
    public abstract class EntityManagerInterceptor<T> : EntityManagerInterceptor
        where T : EntityManager
    {
        /// <summary>Called whenever the entityManager is cleared.</summary>
        /// <param name="source">The EntityManager on which a EntityManager.Clear() has been called.</param>
        /// <param name="args">The original event arguments.</param>
        public virtual void OnCleared(T source, EntityManagerClearedEventArgs args)
        {
        }

        /// <summary>Called whenever an entity's state has changed in any significant manner.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnEntityChanged(T source, EntityChangedEventArgs args)
        {
        }

        /// <summary>Called whenever an entity's state is changing in any significant manner.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnEntityChanging(T source, EntityChangingEventArgs args)
        {
        }

        /// <summary>Called when an error occurs while accessing the EntityServer or backend data source.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnEntityServerError(T source, EntityServerErrorEventArgs args)
        {
        }

        /// <summary>Called before the EntityManager fetches data from an EntityServer. Will only be called if the query will not be satisfied out of
        /// the local cache.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnFetching(T source, EntityFetchingEventArgs args)
        {
        }

        /// <summary>Occurs after the EntityManager has completed processing of a query.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnQueried(T source, EntityQueriedEventArgs args)
        {
        }

        /// <summary>Called before the EntityManager executes a query.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnQuerying(T source, EntityQueryingEventArgs args)
        {
        }

        /// <summary>Called after the EntityManager has persisted changed entities.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnSaved(T source, EntitySavedEventArgs args)
        {
        }

        /// <summary>Called when the EntityManager is preparing to save changes.</summary>
        /// <param name="args">The original event arguments.</param>
        /// <param name="source">The source of the event.</param>
        public virtual void OnSaving(T source, EntitySavingEventArgs args)
        {
        }

        /// <summary>
        /// Override to perform custom validation on a given entity.
        /// </summary>
        /// <param name="entity">The entity to be validated</param>
        /// <param name="validationErrors">A collection to add the validation results</param>
        public virtual void Validate(object entity, VerifierResultCollection validationErrors)
        {
        }
    }
}