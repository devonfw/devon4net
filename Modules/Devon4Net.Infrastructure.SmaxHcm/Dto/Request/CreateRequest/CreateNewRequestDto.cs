using System;
using System.Collections.Generic;
using Devon4Net.Infrastructure.SmaxHcm.Dto.Request.UserOptions;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Request.CreateRequest
{


    public class CreateNewRequestDto
    {
        public CreateRequestEntity entity { get; set; }
        public string operation { get; set; }
    }




    //public class CreateNewRequestDto
    //{
    //    public string ImpactScope { get; set; }
    //    public string Urgency { get; set; }
    //    public string RequestedByPerson { get; set; }
    //    public string RequestsOfferings { get; set; }
    //    public string DisplayLabel { get; set; }
    //    public string Description { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public string UserOptions { get; set; }
    //    //public UserOptionsDto UserOptions { get; set; }

    //}
}