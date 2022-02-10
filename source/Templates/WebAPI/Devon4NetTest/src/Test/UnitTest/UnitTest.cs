using AutoMapper;
using Devon4Net.Business.Common;
using Devon4Net.Infrastructure.Test;


namespace Devon4Net.Test.xUnit.Test.UnitTest
{
    public class UnitTest : BaseManagementTest
    {
        public override void ConfigureMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            Mapper = mockMapper.CreateMapper();
        }
    }
}
