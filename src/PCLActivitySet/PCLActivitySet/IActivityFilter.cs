using System;
using System.Collections.Generic;
using System.Linq;

namespace PCLActivitySet
{
    public interface IActivityFilter
    {
        Func<IEnumerable<Activity>, IEnumerable<Activity>> FilterImpl { get; }
    }

    public class ExcludeNonActiveFilter : IActivityFilter
    {
        public Func<IEnumerable<Activity>, IEnumerable<Activity>> FilterImpl => seq => seq.Where(activity => activity.IsActive);
    }

    public class ExcludeNonActiveWithDelayFilter : IActivityFilter
    {
        private readonly ActivityList _activityList;

        public ExcludeNonActiveWithDelayFilter(ActivityList activityList, TimeSpan delay)
        {
            this._activityList = activityList;
            this.Delay = delay;
        }

        public TimeSpan Delay { get; }
        public Func<IEnumerable<Activity>, IEnumerable<Activity>> FilterImpl
        {
            get
            {
                return seq => seq.Where(activity =>
                {
                    DateTime? lastCompletedTimeStamp = activity.CompletionHistory?.LastOrDefault()?.TimeStamp;
                    return activity.IsActive ||
                           (lastCompletedTimeStamp != null &&
                            lastCompletedTimeStamp.Value + this.Delay > this._activityList.FocusDateTime);
                });
            }
        }
    }

    public class FocusDateRadarFilter : IActivityFilter
    {
        private ActivityList _activityList;

        public FocusDateRadarFilter(ActivityList activityList)
        {
            this._activityList = activityList;
        }

        public Func<IEnumerable<Activity>, IEnumerable<Activity>> FilterImpl
        {
            get
            {
                DateTime focusDatePlus1 = this._activityList.FocusDate.AddDays(1).Date;

                return seq => seq.Where(activity =>
                {
                    if (activity.ActiveDueDate == null || activity.ActiveDueDate.Value < focusDatePlus1)
                        return true;
                    var leadTimeDate = activity.LeadTimeDate;
                    if (leadTimeDate != null && leadTimeDate < focusDatePlus1)
                        return true;

                    return false;
                });
            }
        }
    }
}