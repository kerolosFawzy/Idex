using IDEX.Model;
using IDEX.Views;
using ReactiveUI;
using System;
using Xamarin.Forms;

namespace IDEX.ViewModel
{
    public class RoomDetailsScreenViewModel : BaseViewModel
    {
        public Level SelectedLevel { get; set; } = OverviewScreenViewModel.SelectedRoom;

        //public ReactiveCommand InstaCommand { get; set; }
        //public ReactiveCommand ChangePageCommand { get; set; }
        //public ReactiveCommand HygieneCommand { get; set; }
        //public ReactiveCommand AdditionalCommand { get; set; }

        bool flag = true;

        #region
        string GraySvg = "resource://IDEX.SvgImages.check_detials_room_gray.svg";
        string BlackSvg = "resource://IDEX.SvgImages.check_detials_room_black.svg";

        private string _instaSvgPath;
        public string InstaSvgPath
        {
            get => _instaSvgPath;
            set => this.RaiseAndSetIfChanged(ref _instaSvgPath, value);
        }

        private string _hygSvgPath;
        public string HygSvgPath
        {
            get => _hygSvgPath;
            set => this.RaiseAndSetIfChanged(ref _hygSvgPath, value);
        }

        private string _useExtra = "Use Extra Level";
        public string UseExtra
        {
            get => _useExtra;
            set => this.RaiseAndSetIfChanged(ref _useExtra, value);
        }

        private string _additionalSvgPath;
        public string AdditionalSvgPath
        {
            get => _additionalSvgPath;
            set => this.RaiseAndSetIfChanged(ref _additionalSvgPath, value);
        }


        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private string _id;

        public string ID
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private string _doorNo;

        public string DoorNo
        {
            get => _doorNo;
            set => this.RaiseAndSetIfChanged(ref _doorNo, value);
        }
        private string _name;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        private string _area;

        public string Area
        {
            get => _area;
            set => this.RaiseAndSetIfChanged(ref _area, value);
        }
        private string _hygLevel;

        public string HygLevel
        {
            get => _hygLevel;
            set => this.RaiseAndSetIfChanged(ref _hygLevel, value);
        }
        private string _hygType;

        public string HygType
        {
            get => _hygType;
            set => this.RaiseAndSetIfChanged(ref _hygType, value);
        }
        #endregion

        public RoomDetailsScreenViewModel(IScreen hostScreen = null) : base(hostScreen)
        {
            SetRoomData();
            var InstaCommand = ReactiveCommand.Create(InstaCommandHendler);
            var HygieneCommand = ReactiveCommand.Create(HygieneCommandHendler);
            var AdditionalCommand = ReactiveCommand.Create(AdditionalCommandHendler);
            var ChangePageCommand = ReactiveCommand.Create(ChangePageCommandHendler);
        }

        private void ChangePageCommandHendler()
        {
            if (flag)
                UseExtra = "Use original level";
            else
                UseExtra = "Use Extra level";
            flag = !flag;
        }

        public override void OnSoftBackButtonPressed()
        {
            Navigation.GoBack();
        }

        #region Commandes 
        private void AdditionalCommandHendler()
        {
            AdditionalSvgPath = BlackSvg;
            PageNavigate(nameof(AdditionalRequirementsPage));
        }
        private void HygieneCommandHendler()
        {
            HygSvgPath = BlackSvg;
            PageNavigate(nameof(HygieneScreen));
        }
        private void InstaCommandHendler()
        {
            InstaSvgPath = BlackSvg;
            PageNavigate(nameof(InstaPage));
        }
        #endregion

        private void PageNavigate(string PageName)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.NavigateAsync(PageName);

            });
        }
        private void SetRoomData()
        {
            Title = SelectedLevel.DoorNumber + ", " + SelectedLevel.Name;
            ID = SelectedLevel.ID.ToString();
            DoorNo = SelectedLevel.DoorNumber;
            Name = SelectedLevel.Name;
            Area = SelectedLevel.Area.ToString() + " m2";
            HygLevel = "2";
            HygType = "Bath/Toilet";
            InstaSvgPath = GraySvg;
            HygSvgPath = GraySvg;
            AdditionalSvgPath = GraySvg;
        }

    }
}
