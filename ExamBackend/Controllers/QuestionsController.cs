using ExamBackend.Data;
using ExamBackend.DTOs;
using ExamBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuestionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionResponse>>> GetQuestions()
    {
        var questions = await _context.Questions
            .Include(q => q.Choices)
            .OrderBy(q => q.QuestionNo)
            .ToListAsync();

        var result = questions.Select(q => new QuestionResponse
        {
            Id = q.Id,
            QuestionNo = q.QuestionNo,
            QuestionText = q.QuestionText,
            Choices = q.Choices
                .OrderBy(c => c.ChoiceNo)
                .Select(c => c.ChoiceText)
                .ToList()
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<QuestionResponse>> CreateQuestion(
    CreateQuestionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.QuestionText))
        {
            return BadRequest("Question text is required.");
        }

        if (request.Choices == null || request.Choices.Count != 4)
        {
            return BadRequest("Exactly 4 choices are required.");
        }

        if (request.Choices.Any(string.IsNullOrWhiteSpace))
        {
            return BadRequest("Choice text is required.");
        }

        var nextQuestionNo = await _context.Questions
            .Select(q => (int?)q.QuestionNo)
            .MaxAsync() ?? 0;

        nextQuestionNo++;

        var question = new Question
        {
            QuestionNo = nextQuestionNo,
            QuestionText = request.QuestionText.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        for (var i = 0; i < request.Choices.Count; i++)
        {
            question.Choices.Add(new QuestionChoice
            {
                ChoiceNo = i + 1,
                ChoiceText = request.Choices[i].Trim()
            });
        }

        _context.Questions.Add(question);

        await _context.SaveChangesAsync();

        var response = new QuestionResponse
        {
            Id = question.Id,
            QuestionNo = question.QuestionNo,
            QuestionText = question.QuestionText,
            Choices = question.Choices
                .OrderBy(c => c.ChoiceNo)
                .Select(c => c.ChoiceText)
                .ToList()
        };

        return CreatedAtAction(
            nameof(GetQuestions),
            new { id = question.Id },
            response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.Questions
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
        {
            return NotFound();
        }

        _context.Questions.Remove(question);

        await _context.SaveChangesAsync();

        var remainingQuestions = await _context.Questions
            .OrderBy(q => q.QuestionNo)
            .ToListAsync();

        for (var i = 0; i < remainingQuestions.Count; i++)
        {
            remainingQuestions[i].QuestionNo = i + 1;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<QuestionResponse>> GetQuestion(int id)
    {
        var question = await _context.Questions
            .Include(q => q.Choices)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
        {
            return NotFound();
        }

        var response = new QuestionResponse
        {
            Id = question.Id,
            QuestionNo = question.QuestionNo,
            QuestionText = question.QuestionText,
            Choices = question.Choices
                .OrderBy(c => c.ChoiceNo)
                .Select(c => c.ChoiceText)
                .ToList()
        };

        return Ok(response);
    }
}