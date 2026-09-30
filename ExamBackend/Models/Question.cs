namespace ExamBackend.Models;

public class Question
{
    public int Id { get; set; }

    public int QuestionNo { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public ICollection<QuestionChoice> Choices { get; set; }
        = new List<QuestionChoice>();
}