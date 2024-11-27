import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Job } from '../interfaces/interfaces.all';

@Injectable({
  providedIn: 'root'
})
export class JobsService {
  base_url: string = "https://localhost:7195/api/Job/";

  constructor(private http: HttpClient) { }

  public getJobs(): Observable<{ success: boolean, message?: string, error?: string, jobs?: Job[] }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, jobs?: Job[] }>(`${this.base_url}all-jobs`);
  }

  public getActivatedJobs(): Observable<{ success: boolean, message?: string, error?: string, jobs?: Job[] }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, jobs?: Job[] }>(`${this.base_url}Activated-Jobs`);
  }

  public createJob(UserId: string, job: Job): Observable<{ success: boolean, message?: string, error?: string}> {
    return this.http.post<{ success: boolean, message?: string, error?: string}>(`${this.base_url}create-job/${UserId}`, job);
  }

  public updateJob(JobId: string, job: Job): Observable<{ success: boolean, message?: string, error?: string}> {
    return this.http.put<{ success: boolean, message?: string, error?: string}>(`${this.base_url + "update-job/" + JobId}`, job);
  }

  public deleteJob(JobId: string): Observable<{ success: boolean, message?: string, error?: string}> {
    return this.http.delete<{ success: boolean, message?: string, error?: string}>(`${this.base_url + "delete-job/" + JobId}`);
  }

  public getJobById(JobId: string): Observable<{ success: boolean, message?: string, error?: string, job?: Job[] }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, job?: Job[] }>(`${this.base_url + "get-single-job/" + JobId}`);
  }
}
