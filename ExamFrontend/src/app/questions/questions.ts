import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Question } from '../models/question';
import { QuestionService } from '../services/question';

@Component({
  selector: 'app-questions',
  imports: [RouterLink],
  templateUrl: './questions.html',
  styleUrl: './questions.css',
})
export class Questions implements OnInit {

  questions: Question[] = [];

  constructor(
    private questionService: QuestionService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.questionService.getQuestions().subscribe({
      next: (data) => {
        console.log('QUESTIONS FROM API:', data);

        this.questions = data;

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Failed to load questions', error);
      },
    });
  }

  deleteQuestion(id: number): void {
    this.questionService.deleteQuestion(id).subscribe({
      next: () => {
        this.questions = this.questions.filter(
          question => question.id !== id
        );

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Failed to delete question', error);
      }
    });
  }
}