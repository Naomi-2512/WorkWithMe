import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Position } from '../interfaces/interfaces.all';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PositionsService {
  base_url: string = "https://localhost:7195/api/JobPosition/";

  constructor(private http: HttpClient) { }

  public createPosition(JobId: string, position: Position): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.post<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "create-position/" + JobId}`, position);
  }

  public updatePosition(PositionId: string, position: Position): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.put<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "update-position/" + PositionId}`, position);
  }

  public getPositionsByJobId(JobId: string): Observable<{ success: boolean, message?: string, error?: string, positions: Position }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, positions: Position }>(`${this.base_url + "get-position-by-JobId/" + JobId}`);
  }

  public deletePosition(PositionId: string): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.delete<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "delete-position/" + PositionId}`);
  }
}
