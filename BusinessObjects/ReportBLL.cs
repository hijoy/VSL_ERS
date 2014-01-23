using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects {
    class ReportBLL {

        public ERS.CityDataTable GetProducts() {
            return new MasterDataBLL().GetCityPaged(28, 0, 20, null);
        }
    }
}
