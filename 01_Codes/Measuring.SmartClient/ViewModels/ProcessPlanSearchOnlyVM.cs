using Cet.Core.Data;
using Cet.SmartClient.Client;
using Measurement;
using Measurement.AppServiceContract;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Measuring.SmartClient.ViewModels
{
    public class ProcessPlanSearchOnlyVM : SearchOnlyVMBase<ProcessPlanData, ProcessPlanData, ProcessPlanCriteria>
    {

        public ProcessPlanSearchOnlyVM(IUnityContainer container)
            : base(container)
        {
            this.PageSize = 20;
            this.PageIndex = 0;
            MinimumLengthAutoTextSearch = this.Container.Resolve<byte>("MinimumLengthAutoTextSearch");
        }

        protected override IList<ProcessPlanData> SearchInternal(ProcessPlanCriteria searchCriteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var serviceClient = this.Container.Resolve<IProcessPlanService>();
            return serviceClient.FindProcessPlan(searchCriteria, sortingCriteria, pagingCriteria);
        }

        protected override int CountItemsInternal(ProcessPlanCriteria searchCriteria)
        {
            return PageSize;
        }

        protected override void PrepareDefaultSortingCriteria(SortingCriteria sortingCriteria)
        {
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "SeqNo" });
        }

        public override void ClearCriteria()
        {
            base.ClearCriteria();
        }

        private string inputtext;
        public string Inputtext
        {
            get { return inputtext; }
            set
            {
                inputtext = value;
                OnPropertyChanged("Inputtext");
            }
        }

        private int minimumLengthAutoTextSearch;
        public int MinimumLengthAutoTextSearch
        {
            get { return minimumLengthAutoTextSearch; }
            private set { minimumLengthAutoTextSearch = value; }
        }

        private ICommand autoCompleteSearchCommand;
        public ICommand AutoCompleteSearchCommand
        {
            get
            {
                if (autoCompleteSearchCommand == null)
                    autoCompleteSearchCommand = new DelegateCommand<object>(AutoCompleteSearch);

                return autoCompleteSearchCommand;
            }
        }


        public void AutoCompleteSearch(object parameter)
        {
            if (parameter == null) return;
            var text = parameter.ToString().Replace(Environment.NewLine, string.Empty);
            SearchCriteria.AutoCompletedText = text;
            if (!string.IsNullOrEmpty(SearchCriteria.AutoCompletedText)
                && SearchCriteria.AutoCompletedText.Length >= MinimumLengthAutoTextSearch
                )
            {

                PageSize = 20;
                EnablePaging = true;
                Search();

            }
            else
            {
                SearchResult.Clear();
            }
        }


    }
}
