using Excalibur.Cross.ViewModels;
using MvvmCross.Core.Navigation;
using System;
using System.Collections.Generic;

namespace MyThaiStar.Core.ViewModels
{
    public class OrderMenuViewModel: BaseViewModel
    {
        #region private
        private readonly IMvxNavigationService _navigationService;
        private bool _acceptedTerms;
        private string _bookingId;
        private List<TermOfUse> _termList;
        #endregion

        #region public

        public OrderMenuViewModel()
        {
            SetUp();
        }


        public bool AcceptedTerms
        {
            get => _acceptedTerms;
            set => SetProperty(ref _acceptedTerms, value);
        }

        internal bool CheckAcceptedTerms()
        {
           return AcceptedTerms;
        }

        public string BookingId
        {
            get => _bookingId;
            set => SetProperty(ref _bookingId, value);
        }

        #endregion

        public OrderMenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public List<TermOfUse> TermList
        {
            get => _termList;
            set => SetProperty(ref _termList, value);
        }


        private void SetUp()
        {

            _termList = new List<TermOfUse>
            {
            new TermOfUse{Term="At our busiest times More adopts a 2 hour window for all tables." },
            new TermOfUse{Term="We will endeavour to give more time where possible." },
            new TermOfUse{Term="Any child whose height is below the 1.5 metre line eats for half price." },
            new TermOfUse{Term="Guests not wishing to dine must inform the management as we"},
            new TermOfUse{Term="may not be able to accommodate you at our busiest times."},
            new TermOfUse{Term="At More we encourage you to 'taste not waste'."},
            new TermOfUse{Term="More reserve the right to charge a minimum fee of £3"},
            new TermOfUse{Term="per plate for excessive wastage of food."},
            new TermOfUse{Term="All tables will be held for 20 minutes after the agreed reservation time,"},
            new TermOfUse{Term="after which the reservation will become void. We will, however,"},
            new TermOfUse{Term="try accommodate you as soon as an alternative table becomes available."},
            new TermOfUse{Term="Customers are not permitted to bring their own beer,"},
            new TermOfUse{Term="wine, spirits or soft drinks onto the premises."},
            new TermOfUse{Term="If customers require a cigarette during their visit to More"},
            new TermOfUse{Term="at least one member of the party must stay with the table."},
            new TermOfUse{Term="All meals are to be paid prior to dining."},
            new TermOfUse{Term="More reserve the right to amend the deposit,"},
            new TermOfUse{Term="refund, booking and pricing policy without prior to dining."}
            };
        }
    }

    public class TermOfUse
    {
        public string Term { get; set; }
    }
}
