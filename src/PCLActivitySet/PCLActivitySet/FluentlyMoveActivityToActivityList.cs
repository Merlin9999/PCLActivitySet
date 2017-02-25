using System;
using System.Linq;

namespace PCLActivitySet
{
    public class FluentlyMoveActivityToActivityList
    {
        private readonly ActivityBoard _board;
        private readonly Activity _activityToMove;

        public FluentlyMoveActivityToActivityList(ActivityBoard board, Activity activityToMove)
        {
            if (!board.ContainsActivity(activityToMove))
                throw new ArgumentException($"Cannot move an {nameof(Activity)} that is not owned by the referenced {nameof(ActivityBoard)}.");
            this._board = board;
            this._activityToMove = activityToMove;
        }

        public void ToList(ActivityList activityList)
        {
            if (activityList == this._board.InBox)
                this._activityToMove.ActivityListGuid = null;
            else
            {
                if (!this._board.ActivityLists.Contains(activityList))
                    throw new ArgumentException($"Cannot move the {nameof(Activity)} to an {nameof(ActivityList)} that is not owned by the referenced {nameof(ActivityBoard)}.");
                else
                    this._activityToMove.ActivityListGuid = activityList.Guid;
            }
        }
    }
}