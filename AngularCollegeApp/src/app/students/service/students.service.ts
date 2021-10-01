import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { State, City, Student } from 'src/app/models/student.model';
import { Country } from 'src/app/models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  url = 'https://localhost:44365/Api/Student';
  constructor(private http: HttpClient) { }
  getAllStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.url + '/GetAllStudent');
  }

  getStudentsById(StudentId: string): Observable<Student> {
    return this.http.get<Student>(this.url + '/' + StudentId);
  }
  createStudent(student: Student): Observable<Student> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Student>(this.url + '/SaveStudent/',
    student, httpOptions);
  }
  updateStudent(student: Student): Observable<Student> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Student>(this.url + '/SaveStudent/',
    student, httpOptions);
  }
  deleteStudentById(studentid: string): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.url + '/DeleteStudent/' + studentid,
      httpOptions);
  }

  getCountry(): Observable<Country[]> {
    return this.http.get<Country[]>(this.url + '/GetAllCountry');
  } 

  getState(CountryId: string): Observable<State[]> {
    return this.http.get<State[]>(this.url + '/Country/' + CountryId + '/State');
  }

  getCity(StateId: string): Observable<City[]> {
    return this.http.get<City[]>(this.url + '/State/' + StateId + '/City');
  }

  deleteData(user: Student[]): Observable<string> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<string>(this.url + '/DeleteStudent/', user, httpOptions);
  }  
}
