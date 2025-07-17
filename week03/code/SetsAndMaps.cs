using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Create a HashSet for O(1) lookup
        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();
        var processed = new HashSet<string>();

        foreach (var word in words)
        {
            // Skip if already processed or if letters are the same
            if (processed.Contains(word) || word[0] == word[1])
                continue;

            // Create the reverse of the word
            var reverse = new string(new char[] { word[1], word[0] });

            // Check if the reverse exists in the set and hasn't been processed
            if (wordSet.Contains(reverse) && !processed.Contains(reverse))
            {
                pairs.Add($"{word} & {reverse}");
                processed.Add(word);
                processed.Add(reverse);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            
            // Check if we have at least 4 columns (index 3 is the 4th column)
            if (fields.Length > 3)
            {
                var degree = fields[3].Trim(); // Get the degree from column 4 (index 3)
                
                // Update the count in the dictionary
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize both words: convert to lowercase and remove spaces
        word1 = word1.ToLower().Replace(" ", "");
        word2 = word2.ToLower().Replace(" ", "");

        // If lengths are different, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Count characters in word1
        var charCount = new Dictionary<char, int>();
        for (int i = 0; i < word1.Length; i++)
        {
            char c = word1[i];
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // Subtract characters from word2
        for (int i = 0; i < word2.Length; i++)
        {
            char c = word2[i];
            if (!charCount.ContainsKey(c))
                return false; // Character not found in word1
            
            charCount[c]--;
            if (charCount[c] == 0)
                charCount.Remove(c);
        }

        // If all characters are accounted for, the dictionary should be empty
        return charCount.Count == 0;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<EarthquakeFeatureCollection>(json, options);

        // Create summary strings for each earthquake
        var summaries = new List<string>();
        
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                var place = feature.Properties?.Place ?? "Unknown location";
                var magnitude = feature.Properties?.Mag?.ToString("F1") ?? "Unknown";
                summaries.Add($"{place} - Mag {magnitude}");
            }
        }

        return summaries.ToArray();
    }
}

// Classes needed for JSON deserialization of USGS earthquake data
public class EarthquakeFeatureCollection
{
    public string Type { get; set; }
    public EarthquakeMetadata Metadata { get; set; }
    public List<EarthquakeFeature> Features { get; set; }
}

public class EarthquakeMetadata
{
    public long Generated { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Api { get; set; }
    public int Count { get; set; }
}

public class EarthquakeFeature
{
    public string Type { get; set; }
    public EarthquakeProperties Properties { get; set; }
    public EarthquakeGeometry Geometry { get; set; }
    public string Id { get; set; }
}

public class EarthquakeProperties
{
    public double? Mag { get; set; }
    public string Place { get; set; }
    public long? Time { get; set; }
    public long? Updated { get; set; }
    public int? Tz { get; set; }
    public string Url { get; set; }
    public string Detail { get; set; }
    public int? Felt { get; set; }
    public double? Cdi { get; set; }
    public double? Mmi { get; set; }
    public string Alert { get; set; }
    public string Status { get; set; }
    public int? Tsunami { get; set; }
    public int? Sig { get; set; }
    public string Net { get; set; }
    public string Code { get; set; }
    public string Ids { get; set; }
    public string Sources { get; set; }
    public string Types { get; set; }
    public int? Nst { get; set; }
    public double? Dmin { get; set; }
    public double? Rms { get; set; }
    public double? Gap { get; set; }
    public string MagType { get; set; }
    public string EventType { get; set; }
    public string Title { get; set; }
}

public class EarthquakeGeometry
{
    public string Type { get; set; }
    public List<double> Coordinates { get; set; }
}