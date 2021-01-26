namespace Backend.Model
{
    public class SurveyResult
    {
        public string RatedItem { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfGradesOne { get; set; }
        public int NumberOfGradesTwo { get; set; }
        public int NumberOfGradesThree { get; set; }
        public int NumberOfGradesFour { get; set; }
        public int NumberOfGradesFive { get; set; }

        public SurveyResult() { }

        public SurveyResult(string ratedItem, double averageRating, int numberOfGradesOne, int numberOfGradesTwo,
                            int numberOfGradesThree, int numberOfGradesFour, int numberOfGradesFive)
        {
            RatedItem = ratedItem;
            AverageRating = averageRating;
            NumberOfGradesOne = numberOfGradesOne;
            NumberOfGradesTwo = numberOfGradesTwo;
            NumberOfGradesThree = numberOfGradesThree;
            NumberOfGradesFour = numberOfGradesFour;
            NumberOfGradesFive = numberOfGradesFive;
        }
    }
}
