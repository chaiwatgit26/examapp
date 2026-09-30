
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import {
  CreateQuestionRequest,
  QuestionService
} from '../services/question';

@Component({
  selector: 'app-question-form',
  imports: [FormsModule, RouterLink],
  templateUrl: './question-form.html',
  styleUrl: './question-form.css'
})
export class QuestionForm {

  questionText = '';
  choices: string[] = ['', '', '', ''];

  constructor(
    private questionService: QuestionService,
    private router: Router
  ) {}

  save(): void {

    const request: CreateQuestionRequest = {
      questionText: this.questionText,
      choices: this.choices
    };

    this.questionService.createQuestion(request).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Failed to create question', error);
      }
    });
  }
}

