// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using FakeCitySite.Server.Data;
using FakeCitySite.Shared;
using Microsoft.EntityFrameworkCore.Sqlite;
using Newtonsoft.Json.Linq;

CityContext db = new CityContext("Data Source=Cities.db");
Console.WriteLine("Hello, World!");
string[] lines = File.ReadAllLines("input.txt");

if (args.Contains("--update"))
{
    foreach (string line in lines)
    {
        if (line.StartsWith("$"))
        {
            if (!line.Contains('/'))
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            string[] parts = line.Split('/');
            if (db.Cities.Any(c => c.Name == parts[0]))
            {
                Console.WriteLine("City already existed...");
            }
            else
            {
                var newcity = new City
                {
                    Name = parts[0][1..],
                    IsReal = parts[1] == "real",
                    Region = parts[2].ToLower()
                };
                if (3 < parts.Length)
                {
                    newcity.Country = parts[3].ToLower();
                }

                if (4 < parts.Length)
                {
                    newcity.UsState = parts[4].ToLower();
                }

                db.Cities.Add(newcity);
                db.SaveChanges();
                Console.WriteLine("City added");
            }
        }
    }
}
else if (args.Contains("--getcountrynames"))
{
    var cities = db.Cities.Where(x => x.Country != null).ToList();
    cities.ForEach(c => c.Country = AbbreviationToCountryName(c.Country.ToUpper()));
    db.SaveChanges();
}

static string AbbreviationToCountryName(string abbreviation)
{
    //Turns a 2-letter country abbreviation into the full country name
    string countryName = abbreviation switch
    {
        "AF" => "Afghanistan",
        "AX" => "Åland Islands",
        "AL" => "Albania",
        "DZ" => "Algeria",
        "AS" => "American Samoa",
        "AD" => "Andorra",
        "AO" => "Angola",
        "AI" => "Anguilla",
        "AQ" => "Antarctica",
        "AG" => "Antigua and Barbuda",
        "AR" => "Argentina",
        "AM" => "Armenia",
        "AW" => "Aruba",
        "AU" => "Australia",
        "AT" => "Austria",
        "AZ" => "Azerbaijan",
        "BS" => "Bahamas",
        "BH" => "Bahrain",
        "BD" => "Bangladesh",
        "BB" => "Barbados",
        "BY" => "Belarus",
        "BE" => "Belgium",
        "BZ" => "Belize",
        "BJ" => "Benin",
        "BM" => "Bermuda",
        "BT" => "Bhutan",
        "BO" => "Bolivia",
        "BA" => "Bosnia / Herzegovina",
        "BW" => "Botswana",
        "BV" => "Bouvet Island",
        "BR" => "Brazil",
        "IO" => "British Indian Ocean Territory",
        "BN" => "Brunei Darussalam",
        "BG" => "Bulgaria",
        "BF" => "Burkina Faso",
        "BI" => "Burundi",
        "KH" => "Cambodia",
        "CM" => "Cameroon",
        "CA" => "Canada",
        "CV" => "Cape Verde",
        "KY" => "Cayman Islands",
        "CF" => "Central African Republic",
        "TD" => "Chad",
        "CL" => "Chile",
        "CN" => "China",
        "CX" => "Christmas Island",
        "CC" => "Cocos (Keeling) Islands",
        "CO" => "Colombia",
        "KM" => "Comoros",
        "CG" => "Congo",
        "CD" => "Congo, Democratic Republic",
        "CK" => "Cook Islands",
        "CR" => "Costa Rica",
        "CI" => "Cote D'Ivoire",
        "HR" => "Croatia",
        "CU" => "Cuba",
        "CY" => "Cyprus",
        "CZ" => "Czech Republic",
        "DK" => "Denmark",
        "DJ" => "Djibouti",
        "DM" => "Dominica",
        "DO" => "Dominican Republic",
        "EC" => "Ecuador",
        "EG" => "Egypt",
        "SV" => "El Salvador",
        "GQ" => "Equatorial Guinea",
        "ER" => "Eritrea",
        "EE" => "Estonia",
        "ET" => "Ethiopia",
        "FK" => "Falkland Islands (Malvinas)",
        "FO" => "Faroe Islands",
        "FJ" => "Fiji",
        "FI" => "Finland",
        "FR" => "France",
        "GF" => "French Guiana",
        "PF" => "French Polynesia",
        "TF" => "French Southern Territories",
        "GA" => "Gabon",
        "DE" => "Germany",
        "US" => "United States",
        "GM" => "Gambia",
        "UA" => "Ukraine",
        "RS" => "Serbia",
        "GE" => "Georgia",
        "JP" => "Japan",
        "GI" => "Gibraltar",
        "TR" => "Turkey",
        "GR" => "Greece",
        "MK" => "Macedonia",
        "IT" => "Italy",
        _ => "Error"
    };

    return countryName;
}