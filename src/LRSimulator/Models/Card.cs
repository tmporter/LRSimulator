using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSimulator.Models
{
    public class Card
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> names { get; set; }
        public string manaCost { get; set; }
        public int cmc { get; set; }
        public List<string> colors { get; set; }
        public string type { get; set; }
        public List<string> supertypes { get; set; }
        public List<string> types { get; set; }
        public List<string> subtypes { get; set; }
        public string rarity { get; set; }
        public string set { get; set; }
        public string setName { get; set; }
        public string text { get; set; }
        public string artist { get; set; }
        public string number { get; set; }
        public string power { get; set; }
        public string toughness { get; set; }
        public string layout { get; set; }
        public string imageUrl { get; set; }
        public int? multiverseid { get; set; }
    }

    public class CardComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
            }

            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;

            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;

            return string.Compare(s1, s2, true);
        }

        private bool IsNumeric(object value)
        {
            try
            {
                int i = Convert.ToInt32(value.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
