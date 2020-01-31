using System;
using MvvmCross.Core.ViewModels;

namespace MyThaiStar.Core.ViewModels
{
    public class BookTableViewModel : MvxViewModel
    {
        #region private        
        private DateTime _startDate;
        private DateTime _minStartDate;
        private DateTime _bookingTime;
        private int _assistants;
        #endregion

        #region public
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime BookingTime
        {
            get => _bookingTime;
            set => SetProperty(ref _bookingTime, value);
        }

        public DateTime MinStartDate
        {
            get => DateTime.Today;            
        }
        
        public int Assistants
        {
            get => _assistants;
            set => SetProperty(ref _assistants, value);
        }

        #endregion

    }


}