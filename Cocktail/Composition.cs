// ====================================================================================================================
//   Copyright (c) 2012 IdeaBlade
// ====================================================================================================================
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//   WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
//   OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//   OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
// ====================================================================================================================
//   USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
//   http://cocktail.ideablade.com/licensing
// ====================================================================================================================

using IdeaBlade.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cocktail
{
    /// <summary>
    ///   Sets up a composition container and provides means to interact with the container.
    /// </summary>
    public static partial class Composition
    {
        private static ICompositionProvider _provider;

        static Composition()
        {
            EntityManager.EntityManagerCreated += OnEntityManagerCreated;
        }

        private static void OnEntityManagerCreated(object sender, EntityManagerCreatedEventArgs args)
        {
            if (!args.EntityManager.IsClient)
                return;

            var locator = new PartLocator<IAuthenticationService>(false, () => args.EntityManager.CompositionContext);
            if (locator.IsAvailable)
                args.EntityManager.AuthenticationContext = locator.GetPart().AuthenticationContext;
        }

        /// <summary>
        /// Sets the current <see cref="ICompositionProvider"/>.
        /// </summary>
        public static void SetProvider(ICompositionProvider compositionProvider)
        {
            if (compositionProvider == null)
                throw new ArgumentNullException(StringResources.CompositionProviderCannotBeNull);
            _provider = compositionProvider;
            ProviderChanged(null, EventArgs.Empty);
        }

        /// <summary>
        ///   Returns an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"> Type of the requested instance. </typeparam>
        public static T GetInstance<T>() where T : class
        {
            return GetLazyInstance<T>().Value;
        }

        /// <summary>
        ///   Returns an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"> Type of the requested instance. </typeparam>
        /// <returns>Null if instance is not present in the container.</returns>
        public static T TryGetInstance<T>() where T : class
        {
            return Provider.TryGetInstance<T>();
        }

        /// <summary>
        ///   Returns all instances of the specified type.
        /// </summary>
        /// <typeparam name="T"> Type of the requested instances. </typeparam>
        public static IEnumerable<T> GetInstances<T>() where T : class
        {
            return Provider.GetInstances<T>();
        }

        /// <summary>
        ///   Returns an instance of the provided type or with the specified contract name or both.
        /// </summary>
        /// <param name="serviceType"> The type of the requested instance. If no type is specified the contract name will be used.</param>
        /// <param name="contractName"> The contract name of the instance requested. If no contract name is specified, the type will be used. </param>
        public static object GetInstance(Type serviceType, string contractName)
        {
            return GetLazyInstance(serviceType, contractName).Value;
        }

        /// <summary>
        ///   Returns an instance of the specified type.
        /// </summary>
        /// <param name="serviceType"> The type of the requested instance. If no type is specified the contract name will be used.</param>
        /// <param name="contractName"> The contract name of the instance requested. If no contract name is specified, the type will be used. </param>
        /// <returns>Null if instance is not present in the container.</returns>
        public static object TryGetInstance(Type serviceType, string contractName)
        {
            return Provider.TryGetInstance(serviceType, contractName);
        }

        /// <summary>
        ///   Returns all instances of the provided type.
        /// </summary>
        /// <param name="serviceType"> The type of the requested instance. If no type is specified the contract name will be used.</param>
        /// <param name="contractName"> The contract name of the instance requested. If no contract name is specified, the type will be used. </param>
        public static IEnumerable<object> GetInstances(Type serviceType, string contractName)
        {
            return Provider.GetInstances(serviceType, contractName);
        }

        /// <summary>
        ///   Returns a lazy instance of the specified type.
        /// </summary>
        /// <typeparam name="T"> Type of the requested instance. </typeparam>
        public static Lazy<T> GetLazyInstance<T>() where T : class
        {
            return Provider.GetInstance<T>();
        }

        /// <summary>
        ///   Returns a lazy instance of the provided type or with the specified contract name or both.
        /// </summary>
        /// <param name="serviceType"> The type of the requested instance. If no type is specified the contract name will be used.</param>
        /// <param name="contractName"> The contract name of the instance requested. If no contract name is specified, the type will be used. </param>
        public static Lazy<object> GetLazyInstance(Type serviceType, string contractName)
        {
            return Provider.GetInstance(serviceType, contractName);
        }

        /// <summary>
        ///  Returns a factory that creates new instances of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of instance the factory creates.</typeparam>
        public static ICompositionFactory<T> GetInstanceFactory<T>() where T : class
        {
            return Provider.GetInstanceFactory<T>();
        }

        /// <summary>
        ///  Returns a factory that creates new instances of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of instance the factory creates.</typeparam>
        /// <returns>Null if the container cannot provide a factory for the specified type.</returns>
        public static ICompositionFactory<T> TryGetInstanceFactory<T>() where T : class
        {
            return Provider.TryGetInstanceFactory<T>();
        }

        /// <summary>Manually performs property dependency injection on the provided instance.</summary>
        /// <param name="instance">The instance needing property injection.</param>
        public static void BuildUp(object instance)
        {
            Provider.BuildUp(instance);
        }

        /// <summary>
        /// Event triggered after a new CompositionProvider was assigned through <see cref="SetProvider"/>.
        /// </summary>
        internal static event EventHandler<EventArgs> ProviderChanged = delegate { };

        internal static ICompositionProvider Provider
        {
            get
            {
                if (_provider == null)
                    throw new InvalidOperationException(StringResources.CompositionProviderNotConfigured);
                return _provider;
            }
        }
    }
}