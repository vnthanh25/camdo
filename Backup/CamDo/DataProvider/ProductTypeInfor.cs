using System;
using System.Collections.Generic;
using System.Text;

namespace CamDo.DataProvider
{
    public class ProductTypeInfor
    {
        #region Properties 
        int productTypeId = 0;
        public int ProductTypeId
        {
            set { productTypeId = value; }
            get { return productTypeId; }
        }

        string productTtype = "";
        public string ProductType
        {
            set { productTtype = value; }
            get { return productTtype; }
        }
        #endregion Properties
    }
}
