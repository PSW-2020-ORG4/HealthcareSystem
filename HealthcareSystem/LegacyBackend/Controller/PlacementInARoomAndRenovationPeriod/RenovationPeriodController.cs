using Model.Manager;
using Service.PlacementInARoomAndRenovationPeriod;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.PlacementInARoomAndRenovationPeriod
{
    public class RenovationPeriodController
    {
        private RenovationPeriodService renovationPeriodService = new RenovationPeriodService();

        public Model.Manager.RenovationPeriod ScheduleRenovation(Model.Manager.RenovationPeriod renovationPeriod)
        {
            return renovationPeriodService.ScheduleRenovation(renovationPeriod);
        }

        public bool CancelRenovation(int roomNumber)
        {
            return renovationPeriodService.CancelRenovation(roomNumber);
        }

        public List<RenovationPeriod> ViewRenovations()
        {
            return renovationPeriodService.ViewRenovations();
        }

        public Model.Manager.RenovationPeriod ViewRenovationByRoomNumber(int roomNumber)
        {
            return renovationPeriodService.ViewRenovationByRoomNumber(roomNumber);
        }

        public Model.Manager.RenovationPeriod EditRenovation(Model.Manager.RenovationPeriod renovationPeriod)
        {
            return renovationPeriodService.EditRenovation(renovationPeriod);
        }
    }
}
