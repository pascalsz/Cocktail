﻿//====================================================================================================================
// Copyright (c) 2012 IdeaBlade
//====================================================================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================
// USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
// http://cocktail.ideablade.com/licensing
//====================================================================================================================

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Caliburn.Micro;
using Cocktail;
using Common;
using Common.EntityManagerProviders;
using DomainModel;
using Security;

namespace TempHire
{
    public class AppBootstrapper : BootstrapperBase<HarnessViewModel>
    {
        [Import]
        public IEntityManagerProvider<TempHireEntities> EntityManagerProvider;

        protected override IEnumerable<IResult> ConfigureAsync()
        {
            yield return EntityManagerProvider.InitializeFakeBackingStoreAsync();
        }

        protected override void PrepareCompositionContainer(CompositionBatch batch)
        {
            base.PrepareCompositionContainer(batch);

            batch.AddExportedValue<IEntityManagerProvider<TempHireEntities>>(new DevTempHireEntityManagerProvider());
            batch.AddExportedValue<IEntityManagerProvider<SecurityEntities>>(new DevSecurityEntityManagerProvider());
        }
    }
}