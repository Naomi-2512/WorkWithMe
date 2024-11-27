import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Approval } from '../interfaces/interfaces.all';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApprovalsService {
  base_url: string = "https://localhost:7195/api/Approval/";

  constructor(private http: HttpClient) { }

  public createApproval(approval: Approval): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.post<{ success: boolean, message?: string, error?: string }>(`${this.base_url}create-approval`, approval);
  }

  public updateApproval(ApprovalId: string, approval: Approval): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.put<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "update-approval/" + ApprovalId}`, approval);
  }

  public deleteApproval(ApprovalId: string): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.delete<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "delete-approval/" + ApprovalId}`);
  }

  public getApprovals(): Observable<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>(`${this.base_url}get-all-approvals`);
  }

  public getApproval(ApprovalId: string): Observable<{ success: boolean, message?: string, error?: string, approval?: Approval[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, approval?: Approval[] }>(`${this.base_url + "get-single-approval/" + ApprovalId}`);
  }

  public getApprovalByUserId(UserId: string): Observable<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>(`${this.base_url + "get-approval-by-user-Id/" + UserId}`);
  }

  public getDeclinedApprovalsByUserId(UserId: string): Observable<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>(`${this.base_url + "get-declined-approval-by-user-Id/" + UserId}`);
  }

  public getDeclinedApprovalsByJobId(JobId: string): Observable<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, approvals?: Approval[] }>(`${this.base_url + "get-approval-by-job-Id/" + JobId}`);
  }
}
