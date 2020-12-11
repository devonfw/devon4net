using System.Collections.Generic;

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
        public Optionset5Fccc5624Dddba17Da397224C5D4Ed0BC OptionSet5FCCC5624DDDBA17DA397224C5D4ED0B_c { get; set; }
        public Optionsetb9C2Fb0D304A86418C651492D37A5E72C OptionSetB9C2FB0D304A86418C651492D37A5E72_c { get; set; }
        public string changedUserOptionsForSimulation { get; set; }
        public string PropertyamazonResourceProvider55F063755603608CB8287224C5D426DE_c { get; set; }
        public string Propertyregion55F063755603608CB8287224C5D426DE_c { get; set; }
        public string PropertyPlatformType55F063755603608CB8287224C5D426DE_c { get; set; }
        public string Propertykeypair55F063755603608CB8287224C5D426DE_c { get; set; }
        public string PropertyvpcId55F063755603608CB8287224C5D426DE_c { get; set; }
        public string Propertyimage55F063755603608CB8287224C5D426DE_c { get; set; }
        public string PropertysubnetId55F063755603608CB8287224C5D426DE_c { get; set; }
    }

    public class Optionset5Fccc5624Dddba17Da397224C5D4Ed0BC
    {
        public bool Option55F063755603608CB8287224C5D426DE_c { get; set; }
    }

    public class Optionsetb9C2Fb0D304A86418C651492D37A5E72C
    {
        public bool Option90AB77B8232585150B951492D37AE403_c { get; set; }
    }
}
