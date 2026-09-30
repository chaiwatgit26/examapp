import { Routes } from '@angular/router';
import { Questions } from './questions/questions';
import { QuestionForm } from './question-form/question-form';

export const routes: Routes = [
  {
    path: '',
    component: Questions
  },
  {
    path: 'questions/new',
    component: QuestionForm
  }
];