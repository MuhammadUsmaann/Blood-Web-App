using System.Collections.Generic;

namespace MU.Location
{
    class LocationHandler
    {
        Dictionary<int, Country> _countries;
        Dictionary<int, State> _states;
        Dictionary<int, City> _cities;

        #region Country Region

        public Country GetCountryByID(int id)
        {
            foreach (var item in CountryList)
            {
                if (item.Value != null && item.Value.Id == id)
                {
                    return item.Value;
                }
            }
            return null;
        }
        public Country GetCountryByName(string name)
        {
            foreach (var item in CountryList)
            {
                if (item.Value != null && item.Value.Name == name)
                {
                    return item.Value;
                }
            }
            return null;
        }
        public List<Country> GetCountryByNameMatch(string name)
        {
            List<Country> SelectedValue = new List<Country>();
            foreach (var item in CountryList)
            {
                if (item.Value != null && item.Value.Name.Contains(name))
                {
                    SelectedValue.Add(item.Value);
                }
            }
            return SelectedValue.Count > 0 ? SelectedValue : null;
        }
        public Country GetCountryBySortName(string code)
        {
            foreach (var item in CountryList)
            {
                if (item.Value != null && item.Value.SortName == code)
                {
                    return item.Value;
                }
            }
            return null;
        }

        #endregion 

        #region State Region

        public State GetStateByID(int id)
        {
            foreach (var item in StateList)
            {
                if (item.Value != null && item.Value.Id == id)
                    return item.Value;
            }
            return null;
        }
        public State GetStateByName(string name)
        {
            foreach (var item in StateList)
            {
                if (item.Value != null && item.Value.Name == name)
                    return item.Value;
            }
            return null;
        }
        public List<State> GetStateByNameMatch(string name)
        {
            List<State> SelectedValues = new List<State>();
            foreach (var item in StateList)
            {
                if (item.Value != null && item.Value.Name.Contains(name))
                    SelectedValues.Add(item.Value);
            }
            return SelectedValues.Count > 0 ? SelectedValues : null;
        }
        public List<State> GetStateListByCountryID(int id)
        {
            List<State> SelectedValues = new List<State>();
            foreach (var item in StateList)
            {
                if (item.Value != null && item.Value.CountryID == id)
                    SelectedValues.Add(item.Value);
            }
            return SelectedValues.Count > 0 ? SelectedValues : null;
        }

        #endregion

        #region City Region

        public City GetCityByID(int id)
        {
            foreach (var item in CityList)
            {
                if (item.Value != null && item.Value.Id == id)
                    return item.Value;
            }
            return null;
        }
        public City GetCityByName(string name)
        {
            foreach (var item in CityList)
            {
                if (item.Value != null && item.Value.Name == name)
                    return item.Value;
            }
            return null;
        }
        public List<City> GetCityByNameMatch(string name)
        {
            List<City> SelectedValues = new List<City>();
            foreach (var item in CityList)
            {
                if (item.Value != null && item.Value.Name.Contains(name))
                    SelectedValues.Add(item.Value);
            }
            return SelectedValues.Count > 0 ? SelectedValues : null;
        }
        public List<City> GetCityListByStateID(int id)
        {
            List<City> SelectedValues = new List<City>();
            foreach (var item in CityList)
            {
                if (item.Value != null && item.Value.StateID == id)
                    SelectedValues.Add(item.Value);
            }
            return SelectedValues.Count > 0 ? SelectedValues : null;
        }
        public List<City> GetCityListByCountryID(int id)
        {
            List<City> SelectedValues = new List<City>();
            foreach (var item in CityList)
            {
                if (item.Value != null && item.Value.CountryID == id)
                    SelectedValues.Add(item.Value);
            }
            return SelectedValues.Count > 0 ? SelectedValues : null;
        }

        #endregion 


        public Dictionary<int, Country> CountryList
        {
            get
            {
                if (_countries == null)
                {
                    _countries = new Dictionary<int, Country>();
                    _countries.Add(1, new Country { Id = 1, Name = "Name" , SortName = "asd"});
                }
                return _countries;
            }
        }
        public Dictionary<int, State> StateList
        {
            get
            {
                if (_states == null)
                {
                    _states = new Dictionary<int, State>();
                    _states.Add(1, new State { Id = 1, Name = "Name", CountryID = 1 });
                }
                return _states;
            }
        }
        public Dictionary<int, City> CityList
        {
            get
            {
                if (_cities == null)
                {
                    _cities = new Dictionary<int, City>();
                    _cities.Add(1, new City { Id = 1, Name = "Name", StateID= 1, CountryID = 1 });
                }
                return _cities;
            }
        }

    }
}
