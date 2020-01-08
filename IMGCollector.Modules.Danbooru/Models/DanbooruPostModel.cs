using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace IMGCollector.Modules.Danbooru.Models
{
    public partial class DanbooruPostModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("uploader_id")]
        public long UploaderId { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("source")]
        public Uri Source { get; set; }

        [JsonProperty("md5", NullValueHandling = NullValueHandling.Ignore)]
        public string Md5 { get; set; }

        [JsonProperty("last_comment_bumped_at")]
        public object LastCommentBumpedAt { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }

        [JsonProperty("image_width")]
        public long ImageWidth { get; set; }

        [JsonProperty("image_height")]
        public long ImageHeight { get; set; }

        [JsonProperty("tag_string")]
        public string TagString { get; set; }

        [JsonProperty("is_note_locked")]
        public bool IsNoteLocked { get; set; }

        [JsonProperty("fav_count")]
        public long FavCount { get; set; }

        [JsonProperty("file_ext", NullValueHandling = NullValueHandling.Ignore)]
        public FileExt? FileExt { get; set; }

        [JsonProperty("last_noted_at")]
        public object LastNotedAt { get; set; }

        [JsonProperty("is_rating_locked")]
        public bool IsRatingLocked { get; set; }

        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }

        [JsonProperty("has_children")]
        public bool HasChildren { get; set; }

        [JsonProperty("approver_id")]
        public long? ApproverId { get; set; }

        [JsonProperty("tag_count_general")]
        public long TagCountGeneral { get; set; }

        [JsonProperty("tag_count_artist")]
        public long TagCountArtist { get; set; }

        [JsonProperty("tag_count_character")]
        public long TagCountCharacter { get; set; }

        [JsonProperty("tag_count_copyright")]
        public long TagCountCopyright { get; set; }

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("is_status_locked")]
        public bool IsStatusLocked { get; set; }

        [JsonProperty("pool_string")]
        public PoolString PoolString { get; set; }

        [JsonProperty("up_score")]
        public long UpScore { get; set; }

        [JsonProperty("down_score")]
        public long DownScore { get; set; }

        [JsonProperty("is_pending")]
        public bool IsPending { get; set; }

        [JsonProperty("is_flagged")]
        public bool IsFlagged { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("tag_count")]
        public long TagCount { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("is_banned")]
        public bool IsBanned { get; set; }

        [JsonProperty("pixiv_id")]
        public long? PixivId { get; set; }

        [JsonProperty("last_commented_at")]
        public object LastCommentedAt { get; set; }

        [JsonProperty("has_active_children")]
        public bool HasActiveChildren { get; set; }

        [JsonProperty("bit_flags")]
        public long BitFlags { get; set; }

        [JsonProperty("tag_count_meta")]
        public long TagCountMeta { get; set; }

        [JsonProperty("uploader_name")]
        public string UploaderName { get; set; }

        [JsonProperty("has_large")]
        public bool HasLarge { get; set; }

        [JsonProperty("has_visible_children")]
        public bool HasVisibleChildren { get; set; }

        [JsonProperty("children_ids")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ChildrenIds { get; set; }

        [JsonProperty("is_favorited")]
        public bool IsFavorited { get; set; }

        [JsonProperty("tag_string_general")]
        public string TagStringGeneral { get; set; }

        [JsonProperty("tag_string_character")]
        public string TagStringCharacter { get; set; }

        [JsonProperty("tag_string_copyright")]
        public string TagStringCopyright { get; set; }

        [JsonProperty("tag_string_artist")]
        public string TagStringArtist { get; set; }

        [JsonProperty("tag_string_meta")]
        public string TagStringMeta { get; set; }

        [JsonProperty("file_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri FileUrl { get; set; }

        [JsonProperty("large_file_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri LargeFileUrl { get; set; }

        [JsonProperty("preview_file_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri PreviewFileUrl { get; set; }
    }

    public enum FileExt { Jpg, Png };

    public enum PoolString { Empty, Pool109 };

    public enum Rating { E, Q, S };

    public partial class DanbooruPostModel
    {
        public static DanbooruPostModel[] FromJson(string json) => JsonConvert.DeserializeObject<DanbooruPostModel[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DanbooruPostModel[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                FileExtConverter.Singleton,
                PoolStringConverter.Singleton,
                RatingConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            return l;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class FileExtConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FileExt) || t == typeof(FileExt?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "jpg":
                    return FileExt.Jpg;
                case "png":
                    return FileExt.Png;
            }
            return FileExt.Png;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FileExt)untypedValue;
            switch (value)
            {
                case FileExt.Jpg:
                    serializer.Serialize(writer, "jpg");
                    return;
                case FileExt.Png:
                    serializer.Serialize(writer, "png");
                    return;
            }
            serializer.Serialize(writer, "png");
        }

        public static readonly FileExtConverter Singleton = new FileExtConverter();
    }

    internal class PoolStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PoolString) || t == typeof(PoolString?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return PoolString.Empty;
                case "pool:109":
                    return PoolString.Pool109;
            }
            return PoolString.Empty;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PoolString)untypedValue;
            switch (value)
            {
                case PoolString.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case PoolString.Pool109:
                    serializer.Serialize(writer, "pool:109");
                    return;
            }
            serializer.Serialize(writer, "");
        }

        public static readonly PoolStringConverter Singleton = new PoolStringConverter();
    }

    internal class RatingConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Rating) || t == typeof(Rating?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "e":
                    return Rating.E;
                case "q":
                    return Rating.Q;
                case "s":
                    return Rating.S;
            }
            return Rating.E;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Rating)untypedValue;
            switch (value)
            {
                case Rating.E:
                    serializer.Serialize(writer, "e");
                    return;
                case Rating.Q:
                    serializer.Serialize(writer, "q");
                    return;
                case Rating.S:
                    serializer.Serialize(writer, "s");
                    return;
            }
            serializer.Serialize(writer, "e");
        }

        public static readonly RatingConverter Singleton = new RatingConverter();
    }
}
