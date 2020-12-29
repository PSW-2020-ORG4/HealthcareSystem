﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class EmergencyExaminationDTO
    {
        public List<ExaminationDTO> UnchangedExaminations { get; private set; }
        public List<ExaminationDTO> ShiftedExaminations { get; private set; }

        public EmergencyExaminationDTO() { }

        public EmergencyExaminationDTO(List<ExaminationDTO> unchangedExaminations, List<ExaminationDTO> shiftedExaminations)
        {
            UnchangedExaminations = unchangedExaminations;
            ShiftedExaminations = shiftedExaminations;
        }
    }
}