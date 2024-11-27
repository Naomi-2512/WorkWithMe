import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Payment } from '../interfaces/interfaces.all';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  base_url: string = "https://localhost:7195/api/Payment/";

  constructor(private http: HttpClient) { }

  public makePayment(UserId: string, payment: Payment): Observable<{ success: boolean, message?: string, error?: string }>{
    return this.http.post<{ success: boolean, message?: string, error?: string }>(`${this.base_url + "make-payment/" + UserId}`, payment);
  }

  public getPaymentsByUserId(UserId: string): Observable<{ success: boolean, message?: string, error?: string, payments?: Payment[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, payments?: Payment[] }>(`${this.base_url + "get-payments-by-manager/" + UserId}`);
  }

  public getPayments(): Observable<{ success: boolean, message?: string, error?: string, payments?: Payment[] }>{
    return this.http.get<{ success: boolean, message?: string, error?: string, payments?: Payment[] }>(`${this.base_url}get-payments-history`);
  }
}
