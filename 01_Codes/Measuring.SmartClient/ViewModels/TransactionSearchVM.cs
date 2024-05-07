using Cet.Core.Data;
using Cet.SmartClient.Client;
using Measurement;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measuring.SmartClient.ViewModels
{
    public class TransactionSearchVM<SearchResult, SearchCriterai> : SearchOnlyVMBase<SearchResult, SearchResult, SearchCriterai>
            where SearchResult : MeasuringSearchData, new()
            where SearchCriterai : MeasuringCriteria, new()
    {
        public TransactionSearchVM(IUnityContainer container)
            : base(container)
        {
            ExportExcelCommand = new DelegateCommand(ExportExcel);
        }

        #region Properties
        public DelegateCommand ExportExcelCommand { get; set; }

        private string imgBg;
        public string ImgBg
        {
            get { return imgBg; }
            protected set
            {
                imgBg = value;
                OnPropertyChanged("ImgBg");
            }
        }
        #endregion

        protected override void PrepareDefaultSortingCriteria(SortingCriteria sortingCriteria)
        {
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "No" });
        }

        public override void ClearCriteria()
        {
            base.ClearCriteria();
            OnPropertyChanged("SearchCriteria");
        }


        public virtual IList<SearchResult> SearchAll()
        {
            throw new NotImplementedException();
        }


        public virtual void ItemGoTo(SearchResult iMovementData)
        {
            MessageBox.Show("Found not mothod.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected virtual void ExportExcel()
        {
            MessageBox.Show("Found not mothod.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

    }


}

