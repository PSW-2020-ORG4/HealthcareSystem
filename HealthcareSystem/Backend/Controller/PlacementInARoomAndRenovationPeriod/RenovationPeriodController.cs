using Model.Manager;
using Service.PlacementInARoomAndRenovationPeriod;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.PlacementInARoomAndRenovationPeriod
{
     public class RenovationPeriodController
        {
            private RenovationPeriodService _renovationPeriodService;

            public void AddRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
            {
                _renovationPeriodService.AddRenovationPeriod(renovationPeriod);
            }

            public void DeleteRenovationPeriod(int roomNumber)
            {
                _renovationPeriodService.DeleteRenovationPeriod(roomNumber);
            }

            public List<RenovationPeriod> ViewRenovations()
            {
                return _renovationPeriodService.ViewRenovations();
            }

            public Model.Manager.RenovationPeriod ViewRenovationByRoomNumber(int roomNumber)
            {
                return _renovationPeriodService.ViewRenovationByRoomNumber(roomNumber);
            }

            public void UpdateRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
            {
                _renovationPeriodService.UpdateRenovationPeriod(renovationPeriod);
            }
     }
}

