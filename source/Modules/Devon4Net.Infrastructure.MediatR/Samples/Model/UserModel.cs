using System;
using Devon4Net.Infrastructure.MediatR.Model;

namespace Devon4Net.Infrastructure.MediatR.Samples.Model
{
    public class UserModel : ModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public UserModel(string name, string surName)
        {
            Id = Guid.NewGuid();
            Name = name;
            SurName = surName;
        }
    }
}
