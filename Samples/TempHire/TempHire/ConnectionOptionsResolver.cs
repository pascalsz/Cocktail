﻿// ====================================================================================================================
//  Copyright (c) 2012 IdeaBlade
// ====================================================================================================================
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//  WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
//  OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//  OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
// ====================================================================================================================
//  USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
//  http://cocktail.ideablade.com/licensing
// ====================================================================================================================

using Cocktail;
using Security.Composition;

namespace TempHire
{
    public class ConnectionOptionsResolver : IConnectionOptionsResolver
    {
        #region IConnectionOptionsResolver Members

        public ConnectionOptions GetConnectionOptions(string name)
        {
#if FAKESTORE
            // Replace default connection info in order to connect to fake store
            if (name == ConnectionOptions.Default.Name)
                return ConnectionOptions.Fake
                    .WithCompositionContext(CompositionContextResolver.TempHireFake.Name)
                    .WithName(ConnectionOptions.Default.Name);
#endif
            return null;
        }

        #endregion
    }
}