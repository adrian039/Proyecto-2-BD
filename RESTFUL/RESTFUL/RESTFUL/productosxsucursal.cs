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
    
    public partial class productosxsucursal
    {
        public int idsucursal { get; set; }
        public int idproducto { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<int> precio { get; set; }
        public Nullable<int> estado { get; set; }
    
        public virtual producto producto { get; set; }
        public virtual sucursale sucursale { get; set; }
    }
}
