// Example of code generation with Tobago

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace <Tobago.ProjectName>.DataAccessLayer.Entities
{
    [Table("$Class.Name$")]
    public partial class $Class.Name$
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public $Class.Name$()
        {
<Tobago.Loop(Class.NavigableAssociations, ConstructorInitializers)>
        }

<Tobago.Loop(Class.Attributes, Attributes)><Tobago.Loop(Class.NavigableAssociations, Associations)>    }
}

