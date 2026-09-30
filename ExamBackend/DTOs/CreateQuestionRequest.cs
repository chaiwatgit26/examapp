namespace ExamBackend.DTOs;

public class CreateQuestionRequest
{
    public string QuestionText { get; set; } = string.Empty;

    public List<string> Choices { get; set; } = new();
}