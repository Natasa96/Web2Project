using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;
using WebApp.Models.TrafficSystem;

namespace WebApp.Models
{
    public class DeparturesViewModel
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
    }

    public class EditLineInfoModel
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public string SelectedType { get; set; }
        public List<string> AllTypes { get; set; }
        public List<StationViewModel> SelectedStations { get; set; }
        public List<StationViewModel> AllStations { get; set; }
        public List<string> SelectedSchedule { get; set; }
        public List<string> AllSchedule { get; set; }
        public List<DeparturesViewModel> Departures { get; set; }
        
        public EditLineInfoModel()
        {
            SelectedSchedule = new List<string>();
            SelectedStations = new List<StationViewModel>();
            AllSchedule = new List<string>();
            AllStations = new List<StationViewModel>();
            Departures = new List<DeparturesViewModel>();
            AllTypes = new List<string>();
        }
    }


    public class TicketInfoViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
    public class ValidateUserInfoViewModel
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Document { get; set; }
        public string Type { get; set; }
    }

    public class CheckTicketViewModel
    {
        public int TicketID { get; set; }
    }
    public class GetPriceViewModel
    {
        public string Type { get; set; }
    }

    public class TicketDataViewModel
    {
        public int Price { get; set; }
        public string Type { get; set; }
    }

    public class PassengerInfoViewModel
    {
        public PassengerInfoViewModel() { Types = new List<string>(); }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool Validation { get; set; }
        public string Document { get; set; }
        public string Type { get; set; }
        public List<string> Types {get;set;}
    }

    public class TicketPriceInfoViewModel
    {
        public TicketType TicketType { get; set; }
    }
    

    public class NetworkLineViewModel
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public string Type { get; set; }
        public List<int> Stations { get; set; }
        public List<DateTime> Departures { get; set; }
        public List<string> ScheduleDays { get; set; }

        public NetworkLineViewModel()
        {
            Stations = new List<int>();
            Departures = new List<DateTime>();
            ScheduleDays = new List<string>();
        }
    }

    public class SchaduleType
    {
        public string Type { get; set; }
    }

    public class StationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<int> NLine { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public StationViewModel()
        {
            NLine = new List<int>();
        }
    }

    public class EditStationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<int> NLine { get; set; }
        public List<int> SelectedLines { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public EditStationViewModel()
        {
            NLine = new List<int>();
            SelectedLines = new List<int>();
        }
    }

    //model za red voznje
    public class ScheduleViewModel
    {
        public List<string> Days { get; set; }

        public ScheduleViewModel()
        {
            Days = new List<string>();
        }
    }

    public class ScheduleNLineViewModel
    {

        public int Id{ get; set; }
        public int LineNumber { get; set; }

        public ScheduleNLineViewModel(){}
    }

    public class ScheduleDeparturesViewModel
    {
        public List<string> Departures { get; set; }
        public int selectedNLine { get; set; }

        public ScheduleDeparturesViewModel()
        {
            Departures = new List<string>();
        }
    }
    public class PricelistViewModel
    {
        public Dictionary<string,int> TicketPrice { get; set; }
    }
}