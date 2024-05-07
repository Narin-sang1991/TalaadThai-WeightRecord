using Cet.Core;
using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain
{
    public class MeasuringMoveItem : HwEntity
    {
        protected MeasuringMoveItem() { }

        public MeasuringMoveItem(Measuring iParent, GatewayItemType iGatewayType)
        {
            Id = Guid.NewGuid();

            if (iParent == null)
                throw new DomainException("Measuring movement is required !");
            Measuring = iParent;
            GatewayItemTypeValue = (int)iGatewayType;
            IsDeleted = false;
            ProcessPlans = new List<ProcessPlan>();
        }


        #region Properties
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Guid measuringId;
        public Guid MeasuringId
        {
            get { return measuringId; }
            private set { measuringId = value; }
        }
        private Measuring measuring;
        public virtual Measuring Measuring
        {
            get { return measuring; }
            private set { measuring = value; }
        }

        private long seqNo;
        public long SeqNo
        {
            get { return seqNo; }
            private set { seqNo = value; }
        }

        private string productBarcode;
        public string ProductBarcode
        {
            get { return productBarcode; }
            private set { productBarcode = value; }
        }

        private decimal? coilNetWeight;
        public decimal? CoilNetWeight
        {
            get { return coilNetWeight; }
            private set { coilNetWeight = value; }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            private set { productName = value; }
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            private set { unitPrice = value; }
        }

        private decimal unitPerRatio;
        public decimal UnitPerRatio
        {
            get { return unitPerRatio; }
            private set { unitPerRatio = value; }
        }

        private decimal netWeight;
        public decimal NetWeight
        {
            get { return netWeight; }
            private set { netWeight = value; }
        }

        private decimal tareWeight;
        public decimal TareWeight
        {
            get { return tareWeight; }
            private set { tareWeight = value; }
        }

        private Guid weightUnitId;
        public Guid WeightUnitId
        {
            get { return weightUnitId; }
            private set { weightUnitId = value; }
        }
        private Unit weightUnit;
        public virtual Unit WeightUnit
        {
            get { return weightUnit; }
            private set { weightUnit = value; }
        }

        private int gatewayItemTypeValue;
        public int GatewayItemTypeValue
        {
            get { return gatewayItemTypeValue; }
            private set { gatewayItemTypeValue = value; }
        }

        [NotMapped]
        public GatewayItemType GatewayType
        {
            get
            {
                return (GatewayItemType)GatewayItemTypeValue;
            }
        }

        private bool isDeleted;
        public bool IsDeleted
        {
            get { return isDeleted; }
            private set { isDeleted = value; }
        }

        public virtual ICollection<ProcessPlan> ProcessPlans { get; private set; }
        #endregion

        #region Method
        public void SetSeqNo(long iSeqNo)
        {
            SeqNo = iSeqNo;
        }

        public void SetProductData(string barcode, string name, decimal iUnitPrice)
        {
            ProductBarcode = barcode;
            ProductName = name;
            UnitPrice = iUnitPrice;
        }

        public void SetProductName(string name)
        {
            ProductName = name;
        }

        public void SetCoilNetWeight(decimal? iCoilNetWeight)
        {
            CoilNetWeight = iCoilNetWeight;
        }

        public void SetWeightData(decimal iNetWeight, decimal iTareWeight, Unit iWeightUnit, decimal iUnitPerRatio)
        {
            NetWeight = iNetWeight;
            TareWeight = iTareWeight;
            WeightUnit = iWeightUnit;
            UnitPerRatio = iUnitPerRatio;
        }

        public void SetRemark(string iRemark)
        {
            SetNoteData(iRemark);
        }

        public void ClearRelated(ProcessPlan processPlan)
        {
            if (processPlan.CoilNo == this.ProductBarcode)
            {
                this.ProductBarcode = this.ProductName = string.Empty;
                this.CoilNetWeight = default(decimal?);
                SetNoteData(string.Empty);
            }
        }

        public void VirtualRemove()
        {
            this.IsDeleted = true;
        }

        #endregion
    }

    public class MeasuringMoveItemRemoveEvent : IDomainEvent
    {
        public MeasuringMoveItemRemoveEvent(MeasuringMoveItem item)
        {
            MeasuringMoveItem = item;
        }

        public MeasuringMoveItem MeasuringMoveItem { get; private set; }
    }



}
