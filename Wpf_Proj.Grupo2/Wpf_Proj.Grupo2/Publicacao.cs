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
    
    public partial class Publicacao
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public System.DateTime data_finalizacao { get; set; }
        public string local_realizacao { get; set; }
        public string tipo { get; set; }
    
        public virtual Autoria_Publicacao Autoria_Publicacao { get; set; }
    }
}
