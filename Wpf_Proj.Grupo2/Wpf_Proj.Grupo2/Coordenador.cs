//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wpf_Proj.Grupo2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Coordenador
    {
        public int id_docente { get; set; }
        public Nullable<int> id_departamento { get; set; }
        public Nullable<System.DateTime> data_inicio { get; set; }
        public Nullable<System.DateTime> data_fim { get; set; }
    
        public virtual Departamento Departamento { get; set; }
        public virtual Docente Docente { get; set; }
        public virtual Docente Docente1 { get; set; }
    }
}
