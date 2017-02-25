using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using PCLActivitySet.Recurrence;

namespace PCLActivitySet
{
    [DebuggerDisplay("{Name} : {GetType().Name}")]
    public class Activity : AbstractEntity<Activity>
    {
        private DateTime? _activeDueDate;
        private List<ActivityHistoryItem> _completionHistory;

        public Activity()
        {
            this.CompletionHistory = new List<ActivityHistoryItem>();
        }

        public string Name { get; set; }

        public Guid? ActivityListGuid { get; set; }

        public DateTime? ActiveDueDate
        {
            get { return this._activeDueDate; }
            set
            {
                if (value != null)
                    this.HandleRewind(value.Value);
                this._activeDueDate = value;
            }
        }

        public DateProjection LeadTime { get; set; }


        public DateTime? LeadTimeDate =>
            this.LeadTime == null || this.ActiveDueDate == null
                ? (DateTime?) null
                : this.LeadTime.GetPrevious(this.ActiveDueDate.Value);

        public DateRecurrence Recurrence  { get; set; }


        public List<ActivityHistoryItem> CompletionHistory
        {
            get { return this._completionHistory; }
            set { this._completionHistory = value ?? new List<ActivityHistoryItem>(); }
        }

        public FluentlyModifyActivity Fluently => new FluentlyModifyActivity(this);
        
        public static FluentlyModifyActivity FluentNew(string name, DateTime? activeDueDate = null)
        {
            return new FluentlyModifyActivity(new Activity() {Name = name, ActiveDueDate = activeDueDate});
        }
    }
}