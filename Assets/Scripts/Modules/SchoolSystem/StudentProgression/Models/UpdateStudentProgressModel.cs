using System;

namespace Modules.SchoolSystem.StudentProgression.Models
{
    public class UpdateStudentProgressModel
    {
        public Guid StudentId { get; set; }
        public int GamesCompleted { get; set; }
        public float AverageScorePerGame { get; set; }
        public int WordsCountInVocabulary { get; set; }
        public DateTime LastVocabularyUpdate { get; set; }
    }
}