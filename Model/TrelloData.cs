using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;

namespace TrelloJson
{
    public partial class TrelloData
    {
        [J("model")]  public Model Model { get; set; }  
        [J("action")] public Action Action { get; set; }
    }

    public partial class Action
    {
        [J("id")]              public string Id { get; set; }                  
        [J("idMemberCreator")] public string IdMemberCreator { get; set; }     
        [J("data")]            public Data Data { get; set; }                  
        [J("type")]            public string Type { get; set; }                
        [J("date")]            public DateTimeOffset Date { get; set; }        
        [J("limits")]          public Limits Limits { get; set; }              
        [J("display")]         public Display Display { get; set; }            
        [J("memberCreator")]   public MemberCreator MemberCreator { get; set; }
    }

    public partial class Data
    {
        [J("listAfter")]  public List ListAfter { get; set; } 
        [J("listBefore")] public List ListBefore { get; set; }
        [J("board")]      public Board Board { get; set; }    
        [J("card")]       public DataCard Card { get; set; }  
        [J("old")]        public Old Old { get; set; }        
    }

    public partial class Board
    {
        [J("shortLink")] public string ShortLink { get; set; }
        [J("name")]      public string Name { get; set; }     
        [J("id")]        public string Id { get; set; }       
    }

    public partial class DataCard
    {
        [J("shortLink")] public string ShortLink { get; set; }
        [J("idShort")]   public long IdShort { get; set; }    
        [J("name")]      public string Name { get; set; }     
        [J("id")]        public string Id { get; set; }       
        [J("idList")]    public string IdList { get; set; }   
    }

    public partial class List
    {
        [J("name")] public string Name { get; set; }
        [J("id")]   public string Id { get; set; }  
    }

    public partial class Old
    {
        [J("idList")] public string IdList { get; set; }
    }

    public partial class Display
    {
        [J("translationKey")] public string TranslationKey { get; set; }
        [J("entities")]       public Entities Entities { get; set; }    
    }

    public partial class Entities
    {
        [J("card")]          public EntitiesCard Card { get; set; }      
        [J("listBefore")]    public ListAfter ListBefore { get; set; }   
        [J("listAfter")]     public ListAfter ListAfter { get; set; }    
        [J("memberCreator")] public ListAfter MemberCreator { get; set; }
    }

    public partial class EntitiesCard
    {
        [J("type")]      public string Type { get; set; }     
        [J("idList")]    public string IdList { get; set; }   
        [J("id")]        public string Id { get; set; }       
        [J("shortLink")] public string ShortLink { get; set; }
        [J("text")]      public string Text { get; set; }     
    }

    public partial class ListAfter
    {
        [J("type")]                                   public string Type { get; set; }    
        [J("id")]                                     public string Id { get; set; }      
        [J("text")]                                   public string Text { get; set; }    
        [J("username", NullValueHandling = N.Ignore)] public string Username { get; set; }
    }

    public partial class Limits
    {
    }

    public partial class MemberCreator
    {
        [J("id")]               public string Id { get; set; }              
        [J("avatarHash")]       public string AvatarHash { get; set; }      
        [J("avatarUrl")]        public Uri AvatarUrl { get; set; }          
        [J("fullName")]         public string FullName { get; set; }        
        [J("idMemberReferrer")] public object IdMemberReferrer { get; set; }
        [J("initials")]         public string Initials { get; set; }        
        [J("username")]         public string Username { get; set; }        
    }

    public partial class Model
    {
        [J("id")]             public string Id { get; set; }            
        [J("name")]           public string Name { get; set; }          
        [J("desc")]           public string Desc { get; set; }          
        [J("descData")]       public object DescData { get; set; }      
        [J("closed")]         public bool Closed { get; set; }          
        [J("idOrganization")] public object IdOrganization { get; set; }
        [J("pinned")]         public bool Pinned { get; set; }          
        [J("url")]            public Uri Url { get; set; }              
        [J("shortUrl")]       public Uri ShortUrl { get; set; }         
        [J("prefs")]          public Prefs Prefs { get; set; }          
        [J("labelNames")]     public LabelNames LabelNames { get; set; }
    }

    public partial class LabelNames
    {
        [J("green")]  public string Green { get; set; } 
        [J("yellow")] public string Yellow { get; set; }
        [J("orange")] public string Orange { get; set; }
        [J("red")]    public string Red { get; set; }   
        [J("purple")] public string Purple { get; set; }
        [J("blue")]   public string Blue { get; set; }  
        [J("sky")]    public string Sky { get; set; }   
        [J("lime")]   public string Lime { get; set; }  
        [J("pink")]   public string Pink { get; set; }  
        [J("black")]  public string Black { get; set; } 
    }

    public partial class Prefs
    {
        [J("permissionLevel")]       public string PermissionLevel { get; set; }                       
        [J("voting")]                public string Voting { get; set; }                                
        [J("comments")]              public string Comments { get; set; }                              
        [J("invitations")]           public string Invitations { get; set; }                           
        [J("selfJoin")]              public bool SelfJoin { get; set; }                                
        [J("cardCovers")]            public bool CardCovers { get; set; }                              
        [J("cardAging")]             public string CardAging { get; set; }                             
        [J("calendarFeedEnabled")]   public bool CalendarFeedEnabled { get; set; }                     
        [J("background")]            public string Background { get; set; }                            
        [J("backgroundImage")]       public Uri BackgroundImage { get; set; }                          
        [J("backgroundImageScaled")] public BackgroundImageScaled[] BackgroundImageScaled { get; set; }
        [J("backgroundTile")]        public bool BackgroundTile { get; set; }                          
        [J("backgroundBrightness")]  public string BackgroundBrightness { get; set; }                  
        [J("backgroundBottomColor")] public string BackgroundBottomColor { get; set; }                 
        [J("backgroundTopColor")]    public string BackgroundTopColor { get; set; }                    
        [J("canBePublic")]           public bool CanBePublic { get; set; }                             
        [J("canBeOrg")]              public bool CanBeOrg { get; set; }                                
        [J("canBePrivate")]          public bool CanBePrivate { get; set; }                            
        [J("canInvite")]             public bool CanInvite { get; set; }                               
    }

    public partial class BackgroundImageScaled
    {
        [J("width")]  public long Width { get; set; } 
        [J("height")] public long Height { get; set; }
        [J("url")]    public Uri Url { get; set; }    
    }

    public partial class TrelloData
    {
        public static TrelloData FromJson(string json) => JsonConvert.DeserializeObject<TrelloData>(json, TrelloJson.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TrelloData self) => JsonConvert.SerializeObject(self, TrelloJson.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
