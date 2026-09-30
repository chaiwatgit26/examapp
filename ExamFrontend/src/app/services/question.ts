
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Question } from '../models/question';

export interface CreateQuestionRequest {
  questionText: string;
  choices: string[];
}

@Injectable({
  providedIn: 'root',
})
export class QuestionService {

  private readonly apiUrl = '/api/questions';

  constructor(private http: HttpClient) {}

  getQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(this.apiUrl);
  }

  createQuestion(
    request: CreateQuestionRequest
  ): Observable<Question> {
    return this.http.post<Question>(this.apiUrl, request);
  }

  deleteQuestion(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

