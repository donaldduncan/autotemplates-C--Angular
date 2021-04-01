import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class TrellobotService {

  constructor(private http: HttpClient) { }

  getAllTrellobots(): Observable<Trellobot[]> {
    return this.http.get<Trellobot[]>('../api/trellobot/getall');
  }

  addTrellobot(formData: any): Observable<Trellobot> {
    return this.http.post<Trellobot>('../api/trellobot', formData);
  }

  updateTrellobot(formData: any): Observable<Trellobot> {
    return this.http.put<Trellobot>('../api/trellobot/' + formData.id, formData);
  }

  deleteTrellobot(id: number): Observable<Trellobot[]> {
    return this.http.delete<Trellobot[]>('../api/trellobot/' + id);
  }

  getMembers(): Observable<string> {
    return this.http.get<string>('../api/trellobot/getMembers/');
  }
  getMembers_TrelloApi(id: string): Observable<string> {
    console.log(id);
    return this.http.get<string>('https://api.trello.com/1/members/' + id);
  }

  handleError(error: any): Observable<Trellobot[]> {
    const errMsg = (error.message) ? error.message :
    error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg);
    return Observable.throw(errMsg);
  }

  extractData(data: any): any {
    const body = data.json();
    return body || {};
  }
}


export interface Trellobot {
  /*id: number;*/
  Name: string;
  /*appKey: string;*/
  Token: string;
  BoardId: string;
  TemplateId: string;
  MemberId: string;
}

