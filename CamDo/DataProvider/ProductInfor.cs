using System;
using System.Collections.Generic;
using System.Text;

namespace CamDo.DataProvider
{
    public class ProductInfor
    {
        #region Properties 8
        int productId = 0;
        public int ProductId
        {
            set { productId = value; }
            get { return productId; }
        }

        int productIndex = 0;
        public int ProductIndex
        {
            set { productIndex = value; }
            get { return productIndex; }
        }

        string productName = "";
        public string ProductName
        {
            set { productName = value; }
            get { return productName; }
        }

        string productGold = "";
        public string ProductGold
        {
            set { productGold = value; }
            get { return productGold; }
        }

        string productMobile = "";
        public string ProductMobile
        {
            set { productMobile = value; }
            get { return productMobile; }
        }

        int productAmount = 0;
        public int ProductAmount
        {
            set { productAmount = value; }
            get { return productAmount; }
        }

        int productTypeId = 0;
        public int ProductTypeId
        {
            set { productTypeId = value; }
            get { return productTypeId; }
        }

        Decimal productMoney = 0;
        public Decimal ProductMoney
        {
            set { productMoney = value; }
            get { return productMoney; }
        }

        DateTime productFirstDate = DateTime.Now;
        public DateTime ProductFirstDate
        {
            set { productFirstDate = value; }
            get { return productFirstDate; }
        }

        DateTime productLastDate = DateTime.Now;
        public DateTime ProductLastDate
        {
            set { productLastDate = value; }
            get { return productLastDate; }
        }

        string productTtype = "";
        public string ProductType
        {
            set { productTtype = value; }
            get { return productTtype; }
        }

        DateTime productFinishDate = DateTime.Now;
        public DateTime ProductFinishDate
        {
            set { productFinishDate = value; }
            get { return productFinishDate; }
        }
        #endregion Properties
    }
}
