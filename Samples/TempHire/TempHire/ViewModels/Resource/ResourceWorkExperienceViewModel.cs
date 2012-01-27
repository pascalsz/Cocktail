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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Cocktail;
using Common.Errors;
using Common.Messages;
using Common.Repositories;
using DomainModel;

namespace TempHire.ViewModels.Resource
{
    [Export(typeof(IResourceDetailSection)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourceWorkExperienceViewModel : ResourceScreenBase, IResourceDetailSection, IHandle<SavedMessage>
    {
        [ImportingConstructor]
        public ResourceWorkExperienceViewModel(IRepositoryManager<IResourceRepository> repositoryManager,
                                               IErrorHandler errorHandler)
            : base(repositoryManager, errorHandler)
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            DisplayName = "Work Experience";
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            EventFns.Subscribe(this);
        }

        public bool IsEmpty
        {
            get { return Resource == null || Resource.WorkExperience.Count == 0; }
        }

        public override DomainModel.Resource Resource
        {
            get { return base.Resource; }
            set
            {
                if (base.Resource != null)
                    base.Resource.WorkExperience.CollectionChanged -= WorkExperienceCollectionChanged;

                if (value != null)
                    value.WorkExperience.CollectionChanged += WorkExperienceCollectionChanged;

                base.Resource = value;
                NotifyOfPropertyChange(() => IsEmpty);
                NotifyOfPropertyChange(() => WorkExperienceSorted);
            }
        }

        public IEnumerable<WorkExperienceItem> WorkExperienceSorted
        {
            get
            {
                if (Resource != null)
                    return Resource.WorkExperience.OrderBy(e => e.From);
                return new WorkExperienceItem[0];
            }
        }

        #region IHandle<SavedMessage> Members

        public void Handle(SavedMessage message)
        {
            if (message.Entities.Any(e => e == Resource))
                NotifyOfPropertyChange(() => WorkExperienceSorted);
        }

        #endregion

        #region IResourceDetailSection Members

        public int Index
        {
            get { return 20; }
        }

        void IResourceDetailSection.Start(Guid resourceId)
        {
            Start(resourceId);
        }

        #endregion

        public void Add()
        {
            Resource.AddWorkExperience();
        }

        public void Delete(WorkExperienceItem workExperience)
        {
            Resource.DeleteWorkExperience(workExperience);
        }

        private void WorkExperienceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => IsEmpty);
            NotifyOfPropertyChange(() => WorkExperienceSorted);
        }
    }
}