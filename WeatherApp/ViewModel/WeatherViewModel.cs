using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Commands;
using WeatherApp.Model;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel;

public class WeatherViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _query;

    public string Query
    {
        get { return _query; }
        set 
        {
            _query = value;
            OnPropertyChanged(nameof(Query));
        }
    }

    private CurrentConditions _currentConditions;

    public CurrentConditions CurrentConditions
    {
        get { return _currentConditions; }
        set 
        {
            _currentConditions = value;
            OnPropertyChanged(nameof(CurrentConditions));
        }
    }

    private City _selectedCity;

    public City SelectedCity
    {
        get { return _selectedCity; }
        set 
        { 
            if(_selectedCity is not null)
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
                GetWeather();
            }
        }
    }

    public SearchCommand SearchCommand { get; set; }

    public ObservableCollection<City> Cities { get; set; }

    public WeatherViewModel()
    {
        if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
        {
            SelectedCity = new City 
            {
                LocalizedName = "San Francisco"
            };
            CurrentConditions = new CurrentConditions 
            {
                WeatherText = "Partly cloudy",
                Temperature = new Temperature
                {
                    Metric = new Units
                    {
                        Value = "21"
                    }
                }
            };
        }

        SearchCommand = new SearchCommand(this);
        Cities = new ObservableCollection<City>();
    }

    public async void MakeQuery()
    {
        var cities = await AccuWeatherHelper.GetCities(Query);
        Cities.Clear();

        if(cities is not null)
            foreach (var city in cities)
                Cities.Add(city);
    }

    public async void GetWeather()
    {
        Query = string.Empty;
        CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key) ?? new CurrentConditions();
        Cities?.Clear();
    }

    private void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
