using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects {
    public partial class AuthorizationDS {
        public partial class OrganizationUnitRow {
            public OrganizationUnitRow[] GetChildren() {
                return (OrganizationUnitRow[])this.tableOrganizationUnit.Select("ParentOrganizationUnitId = " + this.OrganizationUnitId);
            }
            public OrganizationUnitRow[] GetActiveChildren() {
                return (OrganizationUnitRow[])this.tableOrganizationUnit.Select("IsActive = 'true' and ParentOrganizationUnitId = " + this.OrganizationUnitId);
            }
        }
    }
}
