using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Entity
{
    public class Option
    {
        public static string BEGIN = "BEGIN";
        public static string DEATH = "DEATH";
        public static string VICTORY = "VICTORY";
        public static string ACTION_INCREMENT_DAY = "INCREMENT_DAY";
        public static string ACTION_ADVERTISEMENT = "ADV";
        public static string TYPE_DEFAULT = "DEFAULT";
        public static string TYPE_PROXY = "PROXY";
        public static string ACTION_DISABLE_ME = "DISABLE_ME";

        //private List<TrackImage> images;
        //private List<TrackAudio> audio;
        //private List<Item> items;
        //private List<Item> requiredItems;
        //private List<string> actions;
        //private string type;
        //private string text;
        //private int duration;

        //public Option()
        //{
        //    images = new List<>(2);
        //    audio = new List<>(2);
        //    items = new List<>(2);
        //    requiredItems = new List<>(1);
        //    actions = new List<>(1);
        //}

        ////    @Override
        ////public String toString()
        ////    {
        ////        return id + " " + getType();
        ////    }

        //public String getType()
        //{
        //    return type != null ? type : TYPE_DEFAULT;
        //}

        //public String getText()
        //{
        //    return text;
        //}

        //public List<TrackImage> getTrackImages()
        //{
        //    return images;
        //}

        //public List<TrackAudio> getTrackAudio()
        //{
        //    return audio;
        //}

        //public List<Item> getItems()
        //{
        //    return items;
        //}

        //public List<Item> getRequiredItems()
        //{
        //    return requiredItems;
        //}

        //public List<String> getActions()
        //{
        //    return actions;
        //}

        //public int getDuration()
        //{
        //    return duration;
        //}

        //public void setDuration(int duration)
        //{
        //    this.duration = duration;
        //}

        //public void reset()
        //{
        //    for (TrackAudio media : getTrackAudio())
        //    {
        //        media.setLocked(false);
        //        media.setCompleted(false);
        //    }

        //    for (TrackImage media : getTrackImages())
        //    {
        //        media.setLocked(false);

        //        Interaction interaction = media.getInteractionObject();
        //        if (interaction != null)
        //        {
        //            interaction.setLocked(false);
        //        }
        //    }
        //}

        //public boolean isProxy()
        //{
        //    return TYPE_PROXY.equals(type);
        //}
    }
}
