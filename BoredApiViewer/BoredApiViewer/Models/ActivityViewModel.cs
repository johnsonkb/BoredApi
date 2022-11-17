using System.Collections.Generic;

namespace BoredApiViewer.Models
{
    public class ActivityViewModel
    {
        public ActivityViewModel()
        {
        }

        public List<BoredActivity> activities { get; set; }
        public string selectedKeyValue { get; set; }
    }
}