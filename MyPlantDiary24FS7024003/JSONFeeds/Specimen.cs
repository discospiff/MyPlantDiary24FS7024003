﻿// namespace MyPlantDiary24FS7024003.JSONFeeds
// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using PlantPlacesSpeicmens;
//
//    var specimen = Specimen.FromJson(jsonString);

namespace MyPlantDiary24FS7024003.JSONFeeds.PlantPlacesSpeicmens
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Specimen
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("plant_id")]
        public long PlantId { get; set; }

        [JsonProperty("specimen_id")]
        public long SpecimenId { get; set; }

        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("genus")]
        public string Genus { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
    }

    public enum Address { Empty, The3400VineStreetCincinnatiOh45220, The5304SpringGroveAvenueCincinnatiOh };

    public partial class Specimen
    {
        public static List<Specimen> FromJson(string json) => JsonConvert.DeserializeObject<List<Specimen>>(json, PlantPlacesSpeicmens.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Specimen> self) => JsonConvert.SerializeObject(self, PlantPlacesSpeicmens.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                AddressConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AddressConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Address) || t == typeof(Address?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case " ":
                    return Address.Empty;
                case "3400 Vine Street Cincinnati OH 45220":
                    return Address.The3400VineStreetCincinnatiOh45220;
                case "5304 Spring Grove Avenue Cincinnati OH ":
                    return Address.The5304SpringGroveAvenueCincinnatiOh;
            }
            throw new Exception("Cannot unmarshal type Address");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Address)untypedValue;
            switch (value)
            {
                case Address.Empty:
                    serializer.Serialize(writer, " ");
                    return;
                case Address.The3400VineStreetCincinnatiOh45220:
                    serializer.Serialize(writer, "3400 Vine Street Cincinnati OH 45220");
                    return;
                case Address.The5304SpringGroveAvenueCincinnatiOh:
                    serializer.Serialize(writer, "5304 Spring Grove Avenue Cincinnati OH ");
                    return;
            }
            throw new Exception("Cannot marshal type Address");
        }

        public static readonly AddressConverter Singleton = new AddressConverter();
    }
}
