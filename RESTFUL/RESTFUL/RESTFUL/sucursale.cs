//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RESTFUL
{
    using System;
    using System.Collections.Generic;
    
    public partial class sucursale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sucursale()
        {
            this.cajasxsucursals = new HashSet<cajasxsucursal>();
            this.empleadosxsucursals = new HashSet<empleadosxsucursal>();
            this.productosxsucursals = new HashSet<productosxsucursal>();
            this.ventas = new HashSet<venta>();
            this.rolesxsucursals = new HashSet<rolesxsucursal>();
        }
    
        public int idsucursal { get; set; }
        public int idempresa { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string imagen { get; set; }
        public Nullable<int> estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cajasxsucursal> cajasxsucursals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empleadosxsucursal> empleadosxsucursals { get; set; }
        public virtual empresa empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productosxsucursal> productosxsucursals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<venta> ventas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rolesxsucursal> rolesxsucursals { get; set; }
    }
}
