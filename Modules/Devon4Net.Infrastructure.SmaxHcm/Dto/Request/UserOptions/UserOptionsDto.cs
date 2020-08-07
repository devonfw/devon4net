using System;
using System.Collections.Generic;
using System.Text;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.UserOptions
{

    public class UserOptionsDto
    {
        public List<Complextypeproperty> complexTypeProperties { get; set; }
    }

    public class Complextypeproperty
    {
        public UserComplexProperties properties { get; set; }
    }

    public class UserComplexProperties
    {
        public Optionset5fccc5624dddba17da397224c5d4ed0b_C OptionSet5FCCC5624DDDBA17DA397224C5D4ED0B_c { get; set; }
        public Optionsetb9c2fb0d304a86418c651492d37a5e72_C OptionSetB9C2FB0D304A86418C651492D37A5E72_c { get; set; }
        public string changedUserOptionsForSimulation { get; set; }
        public string PropertyamazonResourceProvider55F063755603608CB8287224C5D426DE_c { get; set; }
        public string Propertyregion55F063755603608CB8287224C5D426DE_c { get; set; }
        public string PropertyPlatformType55F063755603608CB8287224C5D426DE_c { get; set; }
        public string Propertykeypair55F063755603608CB8287224C5D426DE_c { get; set; }
        public string PropertyvpcId55F063755603608CB8287224C5D426DE_c { get; set; }
        public string Propertyimage55F063755603608CB8287224C5D426DE_c { get; set; }
        public string PropertysubnetId55F063755603608CB8287224C5D426DE_c { get; set; }
    }

    public class Optionset5fccc5624dddba17da397224c5d4ed0b_C
    {
        public bool Option55F063755603608CB8287224C5D426DE_c { get; set; }
    }

    public class Optionsetb9c2fb0d304a86418c651492d37a5e72_C
    {
        public bool Option90AB77B8232585150B951492D37AE403_c { get; set; }
    }
}
