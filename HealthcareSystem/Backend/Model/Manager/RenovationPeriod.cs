/***********************************************************************
 * Module:  Renovation.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Manager.Renovation
 ***********************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class RenovationPeriod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public RenovationPeriod()
        {

        }

        public RenovationPeriod(DateTime beginDate, DateTime endDate)
        {
            BeginDate = beginDate;
            EndDate = endDate;
        }
    }
}
