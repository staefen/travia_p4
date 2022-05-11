using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; //Geo locater
using System.Threading.Tasks; //Geo locater
using Xamarin.Essentials; //Geo locater

namespace P4Travia
{
    class Location
    { //revisit public later
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }

    class LocationHandler
    {
        public async void UserLocation(Location lc)
        {
            //Bruger Geolocation til at finde device latitude og longitude, giver denne information til bruger obj
            try
            {
                var uLocation = await Geolocation.GetLastKnownLocationAsync();
                if (uLocation == null)
                {
                    uLocation = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.Low, //Bruger ikke kort eller noget
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }


                if (uLocation == null)
                {
                    //returner noget text om ingen GPS
                }
                else
                {
                    //Coordinate uC = new Coordinate(uLocation.Latitude, uLocation.Longitude);
                }
                lc.Latitude = uLocation.Latitude;
                lc.Longitude = uLocation.Longitude;


            }
            /* catch (FeatureNotSupportedException fnsEx)
            {
               // Handle not supported on device exception
               throw new ArgumentException("Feature not available");
            }
            catch (FeatureNotEnabledException fneEx)
            {
               // Handle not enabled on device exception
               throw new ArgumentException("Location not avaible on device");
           }
            catch (PermissionException pEx)
            {
               // Handle permission exception
               throw new ArgumentException("Please give permission for location");
           } */
            catch (Exception ex)
            {
                // Unable to get location
                throw new ArgumentException("Unable to get location");
            }
        }

        public async void ConvertCoordinate(Location lc)
        {
            //Tager latitude og longitude kordinater og oversaettter dem til et sted/en adresse, bruger geocoding
            try
            {
                var lat = lc.Latitude;
                var lon = lc.Longitude;

                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    string geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    lc.Address = geocodeAddress;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        } //virker ikke 100

        public async void ConvertAdress(string s, Location lc)
        {
            try
            {
                var address = s;
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    lc.Latitude = location.Latitude;
                    lc.Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        } //Ikke testet endnu
    }
}