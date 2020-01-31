using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace MyThaiStar.Core.ViewModels
{
    public class InviteFriendsViewModel : MvxViewModel
    {
        #region private        
        private DateTime _startDate;
        private DateTime _minStartDate;
        private DateTime _bookingTime;        
        private List<string> _assistants;
        private ObservableCollection<TagItem> _items;
        #endregion

        public InviteFriendsViewModel()
        {
            ResetTags();
        }

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

        public List<string> Assistants
        {
            get => _assistants;
            set => SetProperty(ref _assistants, value);
        }

        public ObservableCollection<TagItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        #endregion

        public void RemoveTagCommand(TagItem tagItem)
        {
            RemoveTag(tagItem);
        }

        private void AddTag(TagItem tagItem)
        {
            Items.Remove(tagItem);
            Items.Add(tagItem);
        }
        private void RemoveTag(TagItem tagItem)
        {
            if (tagItem == null)
                return;

            Items.Remove(tagItem);
        }
        public TagItem ValidateAndReturn(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return null;

            var tagString = tag.StartsWith("#") ? tag : "#" + tag;

            if (Items.Any(v => v.Name.Equals(tagString, StringComparison.OrdinalIgnoreCase)))
                return null;

            var item = new TagItem()
            {
                Name = tagString.ToLower()
            };

            AddTag(item);

            return item;
        }

        public void ResetTags()
        {
            Items = new ObservableCollection<TagItem>();
        }
    }
    public class TagItem : MvxViewModel
    {
        string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
    }


}