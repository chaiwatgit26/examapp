namespace ExamBackend.DTOs;

public class QuestionResponse
{
    public int Id { get; set; }

    public int QuestionNo { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public List<string> Choices { get; set; } = new();
}