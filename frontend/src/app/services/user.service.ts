import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginDetails, User } from '../interfaces/interfaces.all';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  base_url: string = "https://localhost:7195/api/User/";

  constructor(private http: HttpClient) { }

  public getUsers(): Observable<{ success: boolean, message?: string, error?: string, Users?: User[] }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, Users?: User[] }>(`${this.base_url}get-users`, )
  };

  public getSingleUserById(UserId: string): Observable<{ success: boolean, message?: string, error?: string, User: User }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, User: User }>(`${this.base_url + "get-single-user/" + UserId}`);
  }

  public getUsersByJobApplication(JobId: string): Observable<{ success: boolean, message?: string, error?: string, User: User }> {
    return this.http.get<{ success: boolean, message?: string, error?: string, User: User }>(`${this.base_url + "get-users-job-applications/" + JobId}`);
  }

  public deleteUser(UserId: string): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.delete<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "delete-user/" + UserId}`);
  }

  public createUser(user: User): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.post<{ success: boolean, message?: string, error?: string }>(`${this.base_url}register-user`, user);
  }

  public updateUser(UserId: string, user: User): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.put<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "update-user/" + UserId}`, user);
  }

  public loginUser(logins: LoginDetails): Observable<{ success: boolean, message?: string, error?: string }> {
    return this.http.post<{ success: boolean, message?: string, error?: string }>(`${this.base_url}login`, logins);
  }
}
