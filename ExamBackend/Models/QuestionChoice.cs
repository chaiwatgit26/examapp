namespace ExamBackend.Models;

public class QuestionChoice
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int ChoiceNo { get; set; }

    public string ChoiceText { get; set; } = string.Empty;

    public Question Question { get; set; } = null!;
}